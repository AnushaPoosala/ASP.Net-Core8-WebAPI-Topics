using Newtonsoft.Json;
using System.Text;

namespace ClientAppForHMAC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var clientId = "WebApp";
            var secretkey = "XYZCompany123";
            var baseUrl = "https://localhost:7202";// actual Server URL to call the endpoints

            var httpClient = new HttpClient
            {
                //Defalut timeout for HttpClient in .NET is 100 seconds, you can override it by setting Timeout property
                Timeout = TimeSpan.FromMinutes(5),
            };

            try
            {

                //Step 1 : Create User object

                var userInfo = new
                {
                    Username = "Shiva",
                    UserRole = "Lead",
                    Salary = 400000
                };

                var response = SendRequestAsync(httpClient, HttpMethod.Post, "api/User", clientId, secretkey, baseUrl, userInfo).Result;

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(responseContent);
                    Console.WriteLine("User created successfully");
                    Console.ReadLine();
                }
                else
                {
                    Console.WriteLine($"Failed to create User. Status code : {response.StatusCode}");
                    Console.ReadLine();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadLine();
            }

        }

        private static async Task<HttpResponseMessage> SendRequestAsync(
            HttpClient httpClient,
            HttpMethod httpMethod,
            string endPoint,
            string clientId,
            string secretkey,
            string baseurl,
            object data = null)
        {
            var requestbody = data == null ? string.Empty : JsonConvert.SerializeObject(data);

            //Generate HMAC token
            var hmacToken = HMACHelper.GenerateHmacToken(httpMethod.Method, endPoint, clientId, secretkey, requestbody);

            //Create HttpRequestMessage

            var requestMessage = new HttpRequestMessage(httpMethod, $"{baseurl}/{endPoint}")
            {
                Content = !string.IsNullOrEmpty(requestbody) ? new StringContent(requestbody, Encoding.UTF8, "application/json") : null
            };

            //Add Authorization header with HMAC token
            requestMessage.Headers.Add("Authorization", $"HMAC {hmacToken}");



            //Send the request
            return await httpClient.SendAsync(requestMessage);


        }
    }
}