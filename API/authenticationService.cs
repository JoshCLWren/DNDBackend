using RestSharp;

namespace authenticationService
{
    class authenticator
    {
        public string tokenGenerator()
        {
            var client = new RestClient("https://dev-59tm9cah.auth0.com/oauth/token");
            var request = new RestRequest(Method.POST);
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", "grant_type=client_credentials&client_id=%24%7Baccount.clientId%7D&client_secret=YOUR_CLIENT_SECRET&audience=https%3A%2F%2F%24%7Baccount.namespace%7D%2Fapi%2Fv2%2F", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var token = response.Content;
            
            return token;
        }
    }
}