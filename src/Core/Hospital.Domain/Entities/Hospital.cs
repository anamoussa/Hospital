using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;

public class Hospital : IAuditEntity
{
    public int HospitalId { get; set; }
    public string Name { get; set; } = null!;
    public string Address { get; set; } = null!;
    public List<Department> Departments { get; set; } = new();
    public string? Phone { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
}
