using DemoServerAppForHMAC.Models.Services;
using Microsoft.Extensions.Caching.Memory;
using System.Security.Cryptography;
using System.Text;

namespace DemoServerAppForHMAC.Middleware
{
    public class HMACAuthMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMemoryCache _memoryCache;
        private readonly IConfiguration _configuration;

        //This is the time span for the cache to expire
        private static readonly TimeSpan NonceExpireTime = TimeSpan.FromMinutes(2);

        //Here we are injecting the IMemoryCache and IConfiguration , RequestDelegate to the constructor
        public HMACAuthMiddleware(RequestDelegate next, IMemoryCache memoryCache, IConfiguration configuration)
        {
            _next = next;
            _memoryCache = memoryCache;
            _configuration = configuration;
        }

        //This method is used to invoke the middleware
        public async Task Invoke(HttpContext context)
        {

            var isHMACAuthRequired = _configuration.GetValue<bool>("HMACAuth:Enabled");

            //If the HMAC authentication is not required then it will call the next middleware
            if (!isHMACAuthRequired)
            {
                //Skip the HMAC validation and It will Call the next middleware in the pipeline
                await _next(context);
                return;
            }

            //Procced with the HMAC validation
            if (!context.Request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Authorization header is missing");
                return;
            }

            if (!authHeader.ToString().StartsWith("HMAC", StringComparison.OrdinalIgnoreCase))
            {

                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid Authorization header");
                return;
            }


            var tokenDetails = authHeader.ToString().Substring("HMAC".Length).Split("|");


            if (tokenDetails.Length != 4)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid HMAC format");
                return;
            }

            var clientId = tokenDetails[0].Trim();
            var token = tokenDetails[1].Trim();
            var nonce = tokenDetails[2].Trim();
            var timeStamp = tokenDetails[3].Trim();

            //Resolve the scoped service ClientService from the current request service provider
            var clientService = context.RequestServices.GetRequiredService<ClientService>();

            var secrectkey = await clientService.GetClientSecrectKeyInfoAsync(clientId);
            if (secrectkey == null)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid ClientId");
                return;
            }

            if (!long.TryParse(timeStamp, out var timestampSeconds))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid timestamp format");
                return;

            }

            var requestTime = DateTimeOffset.FromUnixTimeSeconds(timestampSeconds).UtcDateTime;
            var currentTime = DateTimeOffset.UtcNow;

            //Check the timestamp is within the allowed time(NonceExpireTime , i.e 2 mints)
            if (Math.Abs((currentTime - requestTime).TotalMinutes) > 2)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Timestamp is outside of the allowed time range");
                return;
            }

            //Check the nonce is already used or not
            var nonceKey = $"{clientId}:{nonce}";
            if (_memoryCache.TryGetValue(nonceKey, out _))
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Nonce is already used");
                return;
            }

            // Add the client specific nonce to the cache with an expiration time
            _memoryCache.Set(nonceKey, true, NonceExpireTime);

            var requestBody = string.Empty;
            //Read the request body Post or Put request
            if (context.Request.Method == HttpMethod.Post.Method || context.Request.Method == HttpMethod.Put.Method)
            {
                context.Request.EnableBuffering();

                using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
                {
                    requestBody = await reader.ReadToEndAsync();
                    //Once you read it , it will be empty so we need to set the position to 0
                    context.Request.Body.Position = 0;
                }

                var isValid = ValidateToken(token, nonce, timeStamp, context.Request, requestBody, secrectkey);
                //it is fals
                if (!isValid)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Invalid Token");
                    return;
                }

                //Call the next middleware in the pipeline
                await _next(context);
            }
        }

        //This method is used to generate the HMAC hash
        private bool ValidateToken(string token, string nonce, string timestamp, HttpRequest httpRequest, string requestBody, string secrectkey)
        {
            var path = Convert.ToString(httpRequest.Path);
            path = path.StartsWith("/") ? path : "/" + path;
            var requestContent = new StringBuilder()
                .Append(httpRequest.Method.ToUpper())
                .Append(path.ToUpper())
                .Append(nonce)
                .Append(timestamp);

            if (httpRequest.Method == HttpMethod.Post.Method || httpRequest.Method == HttpMethod.Put.Method)
            {
                requestContent.Append(requestBody);
            }

            var secrectbytes = Encoding.UTF8.GetBytes(secrectkey);
            var requestContentBytes = Encoding.UTF8.GetBytes(requestContent.ToString());

            //Compute the hash of the request content using the ComputeHash method of HMACSHA512

            using var hmac = new HMACSHA256(secrectbytes);
            var computedHash = hmac.ComputeHash(requestContentBytes);
            var computedToken = Convert.ToBase64String(computedHash);

            return token == computedToken;
        }

    }

}
