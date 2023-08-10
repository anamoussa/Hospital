using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;

public class Department: AuditEntity
{
    public int DepartmentId { get; set; }
    public string Name { get; set; }
    public string Head { get; set; }
    public int HospitalId { get; set; }
    public Hospital Hospital { get; set; }
}
