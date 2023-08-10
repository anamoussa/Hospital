using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;

public class Appointment : IAuditEntity
{
    public int AppointmentId { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; } = new();
    public int PatientId { get; set; }
    public Patient Patient { get; set; } = new();
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; } = null!;
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
}
