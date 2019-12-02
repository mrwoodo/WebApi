using IdentityModel.Client;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiTest
{
    public class HttpClientHelper
    {
        private HttpClient _httpClient;
        private Settings _settings;

        public string ApiClientId
        {
            set => _settings.APIClientId = value;
        }

        public string ApiClientSecret
        {
            set => _settings.APIClientSecret = value;
        }

        public string ApiScope
        {
            set => _settings.APIScope = value;
        }

        public HttpClientHelper()
        {
            _settings = new Settings();
            _httpClient = new HttpClient();
        }

        public async Task<DiscoveryResponse> GetDiscoveryResponse()
        {
            var discoveryResponse = await _httpClient.GetDiscoveryDocumentAsync(_settings.APIServerBaseUrl);

            return discoveryResponse;
        }

        public async Task<TokenResponse> GetAccessToken()
        {
            return await GetAccessToken(
                _settings.APIClientId,
                _settings.APIClientSecret,
                _settings.APIScope);
        }

        private async Task<TokenResponse> GetAccessToken(string clientId, string clientSecret, string scope)
        {
            var discoveryResponse = await GetDiscoveryResponse();
            var tokenResponse = await _httpClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryResponse.TokenEndpoint,
                ClientId = clientId,
                ClientSecret = clientSecret,
                Scope = scope
            });

            return tokenResponse;
        }

        public async Task SetBearer()
        {
            var accessToken = await GetAccessToken();

            _httpClient.SetBearerToken(accessToken.AccessToken);
        }

        public async Task<HttpResponseMessage> GetAsync(string query)
        {
            var response = await _httpClient.GetAsync($"{_settings.APIServerBaseUrl}{query}");

            return response;
        }
    }
}
