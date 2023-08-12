using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;

public class Doctor : ApplicationUser, IAuditEntity
{
    public string Specialization { get; set; } = null!;
    public int DepartmentId { get; set; }
    public Department Department { get; set; } = new();
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
}
