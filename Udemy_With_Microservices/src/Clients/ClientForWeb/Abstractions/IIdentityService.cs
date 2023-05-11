using ClientForWeb.Models;
using IdentityModel.Client;
using ServicesShared;

namespace ClientForWeb.Abstractions
{
    public interface IIdentityService
    {
        Task<Response<bool>> SignIn(SigninInput input);
        Task<TokenResponse> GetAccessTokenByRefreshToken();
        Task RevokeRefreshToken();

    }
   
}
