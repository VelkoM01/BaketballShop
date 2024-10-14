using BasketballShopSharedLibrary.DTOs;
using BasketballShopSharedLibrary.Models;
using BasketballShopSharedLibrary.Response;

namespace BasketballShopServer.Repositories
{
    public interface IUserAccount
    {
        Task<ServiceResponse> Register(UserDTO model);
        Task<LoginResponse> Login(LoginDTO model);
        Task<UserSession> GetUserByToken(string token);
        Task<LoginResponse> GetRefreshToken(PostRefreshTokenDTO model);
        Task<ServiceResponse> Logout(string accessToken);
    }
}
