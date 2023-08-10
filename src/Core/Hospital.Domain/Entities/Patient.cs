using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;


public class Patient : User, IAuditEntity
{
    public int PatientId { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; } = null!;
    public string? Address { get; set; }
    public List<Appointment> Appointments { get; set; } = new();
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
}
