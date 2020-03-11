using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace API {
    class AuthV2Token {
        private static string accessToken;
        public static async Task<string> GetTokenGetUsers (string[] args) {
            Console.WriteLine (accessToken);
            await ClientCredentialsFlow ();
            await GetUsers ();
            return(accessToken);
            // await CreateUser();
        }

        public static async Task<string> ClientCredentialsFlow () {

            var body = new Model {
                grant_type = "client_credentials",
                client_id = "SqOAAsbjZ0MnmINs9zuDFZywJT42KkBk",
                client_secret = "9wSBrEo_ht7_uZqmmXZPUWVdNqJv_4cWx7VhCke8xQxwhIpKp6XdwZnJTeyef76Y",
                audience = "https://dev-59tm9cah.auth0.com/api/v2/"
            };

            using (var client = new HttpClient ()) {
                var content = JsonConvert.SerializeObject (body);
                var stringContent = new StringContent (content, Encoding.UTF8, "application/json");
                var res = await client.PostAsync ("https://dev-59tm9cah.auth0.com/oauth/token", stringContent);
                var responseBody = await res.Content.ReadAsStringAsync ();
                var deserializeBody = JsonConvert.DeserializeObject<AuthResponseModel> (responseBody);
                accessToken = deserializeBody.access_token;
                System.Console.WriteLine (accessToken);
                return(accessToken);
            }

        }
        public static async Task GetUsers () {
            using (var client = new HttpClient ()) {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue ("Bearer", accessToken);
                var response = await client.GetAsync ("https://dev-59tm9cah.auth0.com/api/v2/users");
                var responseBody = await response.Content.ReadAsStringAsync ();
                Console.WriteLine ("==============================");
                Console.WriteLine (responseBody);

            }
        }

        internal class Model {

            public string grant_type { get; set; }
            public string client_id { get; set; }
            public string client_secret { get; set; }
            public string audience { get; set; }
        }

        internal class AuthResponseModel {
            public string access_token { get; set; }
            public string scopes { get; set; }
            public string expires_in { get; set; }
            public string token_type { get; set; }
        }

        internal class User {
            public string email { get; set; }
            public bool email_verified { get; set; }
            public string connection { get; set; }
            public string username { get; set; }
            public string password { get; set; }

        }

    }
}