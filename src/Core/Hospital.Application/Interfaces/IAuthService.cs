using Hospital.Application.Common.Models;

namespace Hospital.Application.Common.Interfaces;

public interface IAuthService
{
    Task<AuthModel> RegisterAsync(RegisterModel model);
    Task<AuthModel> LoginAsync(LoginModel model);
    Task<string> AddToRoleAsync(RoleModel model);
    Task<AuthModel> RefreshTokenAsync(string token);
    Task<bool> RevokeTokenAsync(string token);
}