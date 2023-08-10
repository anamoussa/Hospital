using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;

public class Department : IAuditEntity
{
    public int DepartmentId { get; set; }
    public string Name { get; set; } = null!;
    public string Head { get; set; } = null!;
    public int HospitalId { get; set; }
    public Hospital Hospital { get; set; } = new();
    public List<Doctor> Doctors { get; set; } = new();
    public List<Staff> Staffs { get; set; } = new();
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
}
