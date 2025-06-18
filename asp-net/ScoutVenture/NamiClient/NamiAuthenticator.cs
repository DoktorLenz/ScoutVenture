using System.Net;
using System.Text.Json;
using NamiClient.Exceptions;
using RestSharp;
using RestSharp.Authenticators;

namespace NamiClient
{
    public class NamiAuthenticator(string memberId, string password, CookieContainer cookieContainer) : IAuthenticator
    {
        private readonly object _lock = new();
        private bool _authenticated;

        public ValueTask Authenticate(IRestClient client, RestRequest request)
        {
            lock (_lock)
            {
                if (_authenticated)
                {
                    return ValueTask.CompletedTask;
                }

                // use a separate client to send authentication request 
                RestClientOptions loginClientOptions = new(client.Options.BaseUrl!)
                {
                    CookieContainer = cookieContainer
                };

                using RestClient loginClient = new(loginClientOptions);

                RestRequest loginRequest = new(NamiApiEndpoints.Login, Method.Post);
                loginRequest.AddHeader("Content-Type", ContentType.FormUrlEncoded);

                loginRequest.AddParameter("username", memberId);
                loginRequest.AddParameter("password", password);
                loginRequest.AddParameter("Login", "API");

                RestResponse loginResponse = loginClient.ExecuteAsync(loginRequest).Result;

                if (loginResponse.StatusCode == HttpStatusCode.OK)
                {
                    NamiErrorWrapper? responseContent =
                        JsonSerializer.Deserialize<NamiErrorWrapper>(loginResponse.Content!);
                    if (responseContent != null)
                    {
                        throw new NamiAuthenticationException(
                            $"Failed to authenticate. StatusCode \"{responseContent.StatusCode}\": {responseContent.StatusMessage}");
                    }

                    throw new NamiException("Unknown error: HttpStatusCode 200 OK");
                }

                if (loginResponse.StatusCode != HttpStatusCode.Found ||
                    loginResponse.GetHeaderValue("Location") == null)
                {
                    throw new NamiException($"Unknown error: HttpStatusCode {loginResponse.StatusCode}");
                }

                RestRequest activateLoginRequest = new(loginResponse.GetHeaderValue("Location"));
                RestResponse activateLoginResponse = loginClient.ExecuteAsync(activateLoginRequest).Result;

                VerifyLogin(activateLoginResponse);

                _authenticated = true;
            }

            return ValueTask.CompletedTask;
        }

        private void VerifyLogin(RestResponse response)
        {
            HttpStatusCode statusCode = response.StatusCode;

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