using Microsoft.AspNetCore.Identity;

namespace Hospital.Domain.Entities.Common.Entity;

public class ApplicationUser:IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public IList<RefreshToken>? RefreshTokens { get; set; }

}
