using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;

public class Doctor: AuditEntity
{
    public int DoctorId { get; set; }
    public string Name { get; set; }
    public string Specialization { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
}
