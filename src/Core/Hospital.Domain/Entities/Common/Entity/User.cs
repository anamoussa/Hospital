using Microsoft.AspNetCore.Identity;

namespace Hospital.Domain.Entities.Common.Entity;

public class User:IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
}
