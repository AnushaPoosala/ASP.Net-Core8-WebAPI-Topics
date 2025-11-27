using AESServerApp.Models.Services;
using System.Text;

public class AesEncryptionMiddleware
{

    // Delegate to represent the next middleware in the pipeline
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;

    // Constructor that takes the next middleware and configuration
    public AesEncryptionMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
    }

    // Asynchronous method to handle the request
    public async Task Invoke(HttpContext context)
    {

        //Check if the app is enabled for Encryption

        var isEncryptionEnabled = _configuration.GetValue<bool>("Encryption:Enabled");

        if (!isEncryptionEnabled)
        {

            // If encryption is not enabled, just call the next middleware
            await _next(context);
            return;
        }

        //Now proceed with the encryption process
        //Retrive the 'ClientId' from the request header or use the 'DefaultClient' if not found
        var clientId = context.Request.Headers["ClientId"].FirstOrDefault() ?? "DefaultClient";

        //Resolve the AESEncryptionService from the service provider
        var aesEncryptionService = context.RequestServices.GetService<AESEncryptionService>();
        if (aesEncryptionService == null)
        {
            throw new Exception("AESEncryptionService not found.");
        }

        //Check if the request is Post or put
        if (context.Request.Method == HttpMethods.Post || context.Request.Method == HttpMethods.Put)
        {
            // Read the request body
            context.Request.EnableBuffering();

            // Here we are created a StreamReader for Read the request body as a string
            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, leaveOpen: true))
            {
                var encryptedBody = await reader.ReadToEndAsync();

                //Reset the request body stream position to 0 to allow the next middleware to read it
                context.Request.Body.Position = 0;

                // Check if the request body is empty
                if (string.IsNullOrEmpty(encryptedBody))
                {
                    // If the request body is empty, just call the next middleware
                    await _next(context);
                    return;
                }

                try
                {

                    // Encrypt the request body
                    var decryptedBody = await aesEncryptionService.DecryptStringAsync(clientId, encryptedBody);

                    //Convert the decrypted body to a byte array
                    var decryptedBodyBytes = Encoding.UTF8.GetBytes(decryptedBody);

                    // Create a new MemoryStream with the decrypted body bytes
                    var decryptedBodyStream = new MemoryStream(decryptedBodyBytes);
                    // Replace the request body with the decrypted body stream
                    context.Request.Body = decryptedBodyStream;


                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log it, return an error response, etc.)
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync("Error decrypting request body: " + ex.Message);
                    return;
                }
                var originalResponseBodyStream= context.Request.Body;
                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    try
                    {
                        await _next(context);
                        context.Response.Body.Seek(0,SeekOrigin.Begin);

                        var responseText=await new StreamReader(context.Response.Body).ReadLineAsync();

                        if(!string.IsNullOrEmpty(responseText) && context.Response.StatusCode>=200 && context.Response.StatusCode<300)
                        {

                        }

                    }
                    catch(Exception ex)
                    {

                    }
                }

            }
        }

    }


}