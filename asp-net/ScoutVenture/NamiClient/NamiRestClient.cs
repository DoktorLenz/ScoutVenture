using System.Net;
using System.Text.Json;
using NamiClient.Exceptions;
using RestSharp;

namespace NamiClient
{
    public sealed class NamiRestClient : IDisposable
    {
        private readonly RestClient _restClient;

        public NamiRestClient(string memberId, string password)
        {
            CookieContainer cookieContainer = new();
            RestClientOptions options = new("https://nami.dpsg.de")
            {
                CookieContainer = cookieContainer,
                FollowRedirects = false,
                Authenticator = new NamiAuthenticator(memberId, password, cookieContainer)
            };
            _restClient = new RestClient(options);
        }

        public void Dispose()
        {
            _restClient.Dispose();
        }

        public async Task<NamiDataWrapper> GetAllNamiMembersOfGrouping(string groupingId)
        {
            RestRequest request = new(NamiApiEndpoints.AllMembersOfGrouping(groupingId));

            RestResponse response = await _restClient.ExecuteAsync(request);

            if (!response.IsSuccessful)
            {
                throw new Exception(response.ErrorMessage);
            }

            NamiDataWrapper? result = response.Content == null
                ? null
                : JsonSerializer.Deserialize<NamiDataWrapper>(response.Content);

            if (result != null)
            {
                return result.ResponseType switch
                {
                    "OK" => result,
                    "ERROR" => throw new NamiSessionExpiredException(),
                    "EXCEPTION" => throw new NamiAccessViolationException(
                        "Benutzer hat keine Berechtigungen für die Gruppierung."),
                    _ => throw new NamiException(
                        $"Unhandled responseType: {result.ResponseType}. Message: {response.ErrorMessage}")
                };
            }

            throw new NamiException("Content of request was null.");
        }
    }
}