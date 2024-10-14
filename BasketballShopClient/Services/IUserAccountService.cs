using BasketballShopSharedLibrary.DTOs;
using BasketballShopSharedLibrary.Response;

namespace BasketballShopClient.Services
{
    public interface IUserAccountService
    {
        Task<ServiceResponse> Register(UserDTO model);
        Task<LoginResponse> Login(LoginDTO model);
        Task<ServiceResponse> Logout();
    }
}
