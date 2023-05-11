using ClientForWeb.Models;

namespace ClientForWeb.Abstractions
{
    public interface IUserService
    {
        Task<UserViewModel> GetUser();
    }
}
