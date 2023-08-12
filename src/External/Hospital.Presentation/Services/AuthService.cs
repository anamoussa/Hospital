using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Hospital.Application.Common.Interfaces;
using Hospital.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;
using Hospital.UI.Helpers;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;
using Hospital.Domain.Entities.Common.Entity;
using Hospital.Domain.Entities;

namespace Hospital.UI.Services;

public class AuthService : IAuthService
{
    private readonly UserManager<ApplicationUser> manager;
    private readonly RoleManager<IdentityRole> roleManager;
    private readonly SignInManager<ApplicationUser> signInManager;
    private readonly JwtSettings jwtSettings;
    public AuthService(
        UserManager<ApplicationUser> _manager,
        RoleManager<IdentityRole> _roleManager,
        SignInManager<ApplicationUser> _signInManager,
        IOptions<JwtSettings> _jwtSettings)
    {
        manager = _manager;
        roleManager = _roleManager;
        signInManager = _signInManager;
        jwtSettings = _jwtSettings.Value;
    }
    public async Task<string> AddToRoleAsync(RoleModel model)
    {
        var user = await manager.FindByIdAsync(model.UserId);
        var role = await roleManager.FindByNameAsync(model.RoleName);
        if (user is null || role is null)
            return "invaild user id or role name!";
        if (await manager.IsInRoleAsync(user, model.RoleName))
            return "user is already asigned to this role";
        var result = await manager.AddToRoleAsync(user, model.RoleName);
        return (!result.Succeeded) ? result.Errors.ToString()! : "";
    }

    [Obsolete]
    public async Task<AuthModel> LoginAsync(LoginModel model)
    {
        var authModel = new AuthModel();
        var user = await manager.FindByNameAsync(model.UserName);
        if (user is null || !await manager.CheckPasswordAsync(user!, model.Password))
        {
            authModel.Message = "user not found";
            return authModel;
        }
        var result = await signInManager.PasswordSignInAsync(user, model.Password, false, true);
        if (!result.Succeeded)
        {
            authModel.Message = "something went wrong!";
            return authModel;
        }
        if (result.IsLockedOut)
        {
            authModel.Message = "user is locked out please try again later!";
            return authModel;
        }
        await CheckToRefreshToken(user, authModel);
        var roles = await manager.GetRolesAsync(user);
        var jwtSecurityToken = await CreateJwtTokenAsync(user);
        var jwtToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
        authModel.IsAuthenticated = true;
        authModel.UserName = user.UserName;
        authModel.Roles = roles.ToList();
        authModel.Token = jwtToken;
        authModel.ExpiresOn = jwtSecurityToken.ValidTo;
        return authModel;
    }

    public async Task<AuthModel> RegisterAsync(RegisterModel model)
    {
        var email = await manager.FindByEmailAsync(model.Email);
        var username = await manager.FindByNameAsync(model.UserName);
        if (email is not null || username is not null)
            return new AuthModel { Message = "email or user name is already registered!" };
        var user = new ApplicationUser
        {
            Email = model.Email,
            UserName = model.UserName
        };
        var result = await manager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            var errors = "";
            foreach (var error in result.Errors)
                errors += $"{error.Description} ,";
            return new AuthModel { Message = errors };
        }
        await manager.AddToRoleAsync(user, "User");
        // TODO: Confirm User's Email
        return new AuthModel
        {
            IsAuthenticated = true,
            UserName = user.UserName
        };
    }

    [Obsolete]
    public async Task<AuthModel> RefreshTokenAsync(string token)
    {
        var authModel = new AuthModel();
        var user = await manager.Users.SingleOrDefaultAsync(rft => rft.RefreshTokens!.Any(rf => rf.Token == token));
        if (user is null)
        {
            authModel.Message = "invalid token";
            return authModel;
        }
        var refreshToken = user.RefreshTokens?.Single(t => t.Token == token);
        if (!refreshToken!.IsActive)
        {
            authModel.Message = "inactive token!";
            return authModel;
        }
        refreshToken.RevokeOn = DateTime.UtcNow;
        var newRefreshToken = GenerateRefreshToken();
        user.RefreshTokens?.Add(newRefreshToken);
        await manager.UpdateAsync(user);
        var jwtToken = await CreateJwtTokenAsync(user);
        var roles = await manager.GetRolesAsync(user);
        authModel.IsAuthenticated = true;
        authModel.RefreshToken = newRefreshToken.Token;
        authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
        authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        authModel.ExpiresOn = jwtToken.ValidTo;
        authModel.UserName = user.UserName;
        authModel.Roles = roles.ToList();
        return authModel;
    }
    public async Task<bool> RevokeTokenAsync(string token)
    {
        var user = await manager.Users.SingleOrDefaultAsync(u => u.RefreshTokens!.Any(t => t.Token == token));
        if (user is null)
            return false;
        var refreshToken = user.RefreshTokens?.Single(t => t.Token == token);
        if (!refreshToken!.IsActive)
            return false;

        refreshToken.RevokeOn = DateTime.UtcNow;
        user.RefreshTokens!.Add(refreshToken);
        await manager.UpdateAsync(user);
        return true;
    }
    private async Task<JwtSecurityToken> CreateJwtTokenAsync(ApplicationUser user)
    {
        var userClaims = await manager.GetClaimsAsync(user);
        var roles = await manager.GetRolesAsync(user);
        var rolesClaims = new List<Claim>();
        foreach (var role in roles)
            rolesClaims.Add(new Claim("role", role));
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email,user.Email!),
            new Claim("uid",user.Id!),
        }
        .Union(userClaims)
        .Union(rolesClaims);
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        return new JwtSecurityToken(
            claims: claims,
            issuer: jwtSettings.Issure,
            audience: jwtSettings.Audience,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.Duration)
            );
    }
    [Obsolete]
    private RefreshToken GenerateRefreshToken()
    {
        var arr = new byte[32];
        var generator = new RNGCryptoServiceProvider();
        generator.GetBytes(arr);
        return new RefreshToken
        {
            Token = Convert.ToBase64String(arr),
            ExpiresOn = DateTime.UtcNow.AddDays(10),
            CreatedOn = DateTime.UtcNow
        };
    }

    [Obsolete]
    private async Task CheckToRefreshToken(ApplicationUser user, AuthModel model)
    {
        if (user.RefreshTokens!.Any(rft => rft.IsActive))
        {
            var refreshToken = user.RefreshTokens?.FirstOrDefault(rf => rf.IsActive);
            model.RefreshToken = refreshToken?.Token;
            model.RefreshTokenExpiration = refreshToken!.ExpiresOn;
        }
        else
        {
            var newRefreshToken = GenerateRefreshToken();
            user.RefreshTokens?.Add(newRefreshToken);
            await manager.UpdateAsync(user);
            model.RefreshToken = newRefreshToken.Token;
            model.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
        }
    }
}
