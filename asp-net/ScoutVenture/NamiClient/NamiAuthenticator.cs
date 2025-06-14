using System.Net;
using System.Security.Authentication;
using System.Text.Json;
using NamiClient.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace NamiClient
{
    public class NamiAuthenticator(string memberId, string password, CookieContainer cookieContainer) : IAuthenticator
    {
        private bool _authenticated;
        private readonly object _lock = new();

        public ValueTask Authenticate(IRestClient client, RestRequest request)
        {
            lock (_lock)
            {
                if (_authenticated) return ValueTask.CompletedTask;
                
                // use a separate client to send authentication request 
                var loginClientOptions = new RestClientOptions(client.Options.BaseUrl!)
                {
                    CookieContainer = cookieContainer
                };
                
                using var loginClient = new RestClient(loginClientOptions);
                
                var loginRequest = new RestRequest(NamiApiEndpoints.Login, Method.Post);
                loginRequest.AddHeader("Content-Type", ContentType.FormUrlEncoded);
                
                loginRequest.AddParameter("username", memberId);
                loginRequest.AddParameter("password", password);
                loginRequest.AddParameter("Login", "API");
                
                var loginResponse = loginClient.ExecuteAsync(loginRequest).Result;

                var activateLoginRequest = new RestRequest(loginResponse.GetHeaderValue("Location"));
                var activateLoginResponse = loginClient.ExecuteAsync(activateLoginRequest).Result;
                
                VerifyLogin(activateLoginResponse);

                _authenticated = true;
            }

            return ValueTask.CompletedTask;
        }

        private void VerifyLogin(RestResponse response)
        {
            var statusCode = response.StatusCode;

            switch (response.StatusCode)
            {
                case HttpStatusCode.NoContent:
                    throw new NamiException("Session activation error");
                case HttpStatusCode.OK:
                    break;
                default:
                    throw new NamiException("Unexpected response status code: " + response.StatusCode);
            }

            if (cookieContainer.GetAllCookies().All(c => c.Name != "JSESSIONID"))
            {
                throw new NamiException("No session cookie received");
            }

            NamiLoginResponse? namiLoginResponse = JsonSerializer.Deserialize<NamiLoginResponse>(response.Content!);
            
            if (namiLoginResponse == null)
            {
                throw new NamiException("Empty body");
            }
        }
    }
}