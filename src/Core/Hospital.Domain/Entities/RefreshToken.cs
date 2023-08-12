using Microsoft.EntityFrameworkCore;

namespace Hospital.Domain.Entities;

[Owned]
public class RefreshToken
{
    public string Token { get; set; } = null!;
    public DateTime ExpiresOn { get; set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;
    public DateTime CreatedOn { get; set; }
    public DateTime? RevokeOn { get; set; }
    public bool IsActive => RevokeOn is null && !IsExpired;
}