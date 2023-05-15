using ClientForWeb.Abstractions;
using ClientForWeb.Exceptions;
using System.Net.Http.Headers;

namespace ClientForWeb.Handlers
{
    public class ClientCreadentialTokenHandler : DelegatingHandler
    {
        private readonly IClientCredentialsTokenService _clientCredentialTokenService;

        public ClientCreadentialTokenHandler(IClientCredentialsTokenService clientCredentialTokenService)
        {
            _clientCredentialTokenService = clientCredentialTokenService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await _clientCredentialTokenService.GetTokenAsync());


            var response = await base.SendAsync(request, cancellationToken);

            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                throw new UnAuthorizeException();
            }

            return response;
        }
    }
}
