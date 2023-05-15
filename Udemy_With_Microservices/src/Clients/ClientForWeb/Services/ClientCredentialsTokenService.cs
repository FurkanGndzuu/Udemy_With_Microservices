using ClientForWeb.Abstractions;
using ClientForWeb.Configurations;
using IdentityModel.AspNetCore.AccessTokenManagement;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace ClientForWeb.Services
{
    public class ClientCredentialsTokenService : IClientCredentialsTokenService
    {
        readonly ServiceApiSettings _serviceApiSettings;
        readonly ClientSettings _clientSettings;
        readonly IClientAccessTokenCache _clientAccessTokenCache;
        readonly HttpClient _httpClient;

        public ClientCredentialsTokenService(IOptions<ServiceApiSettings> serviceApiSettings, IOptions<ClientSettings> clientSettings, IClientAccessTokenCache clientAccessTokenCache, HttpClient httpClient)
        {
            _serviceApiSettings = serviceApiSettings.Value;
            _clientSettings = clientSettings.Value;
            _clientAccessTokenCache = clientAccessTokenCache;
            _httpClient = httpClient;
        }

        public async Task<string> GetTokenAsync()
        {
            var currentToken = await _clientAccessTokenCache.GetAsync("WebClientToken",new ClientAccessTokenParameters());

            if (currentToken is not null)
                return currentToken.AccessToken;


            var disco = await _httpClient.GetDiscoveryDocumentAsync(new DiscoveryDocumentRequest()
            {
                Address = _serviceApiSettings.BaseUrl,
                Policy = new DiscoveryPolicy { RequireHttps = false }
            });

            if (disco.IsError)
                throw disco.Exception;

            var clientCreadentialTokenRequest = new ClientCredentialsTokenRequest()
            {
                ClientId = _clientSettings.WebClient.Client_Id,
                ClientSecret = _clientSettings.WebClient.Client_Secret,
                Address = disco.TokenEndpoint
            };

            var newToken = await _httpClient.RequestClientCredentialsTokenAsync(clientCreadentialTokenRequest);

            if (newToken.IsError)
                throw newToken.Exception;

            await _clientAccessTokenCache.SetAsync("WebClientToken", newToken.AccessToken, newToken.ExpiresIn,new ClientAccessTokenParameters());

            return newToken.AccessToken;
        }
    }
}
