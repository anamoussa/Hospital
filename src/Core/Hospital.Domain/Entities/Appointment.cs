using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;

public class Appointment:AuditEntity
{
    public int AppointmentId { get; set; }
    public int DoctorId { get; set; }
    public Doctor Doctor { get; set; }
    public int PatientId { get; set; }
    public Patient Patient { get; set; }
    public DateTime AppointmentDate { get; set; }
    public string Status { get; set; }
}
