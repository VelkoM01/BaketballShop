using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketballShopSharedLibrary.Response
{
    public record class ServiceResponse(bool Flag, string Message = null!);
    public record class LoginResponse(bool Flag, string? Message, string Token = null!, string RefreshToken = null!);
}
