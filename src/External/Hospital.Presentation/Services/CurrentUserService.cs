using System.Security.Claims;
using Hospital.Application.Common.Interfaces;

namespace Hospital.UI.Services;

public class CurrentUserService : ICurrentUserService
{
    public CurrentUserService(IHttpContextAccessor context)
    {
        this.UserId = context.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier)! ?? "AlaaDin";
    }
    public string UserId { get; }
}