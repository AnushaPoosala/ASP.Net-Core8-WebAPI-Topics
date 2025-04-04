using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ClientAppForHMAC
{
    public class HMACHelper
    {

        public static string GenerateHmacToken(string httpMethodName, string path, string clientId, string secretkey, string requestBody = "")
        {

            //Generate a nonce (a random value, for example a GUID)
            var nonce = Guid.NewGuid().ToString();

            //Generate a timestamp (a UNIX timestamp will do)
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();

            //Build the request content by concatenating the HTTP method, the path, the timestamp, the nonce and the request body
            path = path.StartsWith("/") ? path : "/" + path;
            Console.WriteLine("path:" + path);
            var requestContent = new StringBuilder()
               .Append(httpMethodName.ToUpper())
               .Append(path.ToUpper())
               .Append(nonce)
               .Append(timestamp);


            if (httpMethodName == HttpMethod.Post.Method || httpMethodName == HttpMethod.Put.Method)
            {
                requestContent.Append(requestBody);
            }

            Console.WriteLine("nonce :" + nonce);
            Console.WriteLine(requestContent.ToString());

            var secrectbytes = Encoding.UTF8.GetBytes(secretkey);
            var requestContentBytes = Encoding.UTF8.GetBytes(requestContent.ToString());

            //Compute the hash of the request content using the ComputeHash method of HMACSHA512

            using var hmac = new HMACSHA256(secrectbytes);
            var computedHash = hmac.ComputeHash(requestContentBytes);
            var computedToken = Convert.ToBase64String(computedHash);

            return $"{clientId}|{computedToken}|{nonce}|{timestamp}";



        }

    }
}