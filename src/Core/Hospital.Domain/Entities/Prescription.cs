using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;

public class Prescription : IAuditEntity
{
    public int PrescriptionId { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; } = new();
    public string Medication { get; set; } = null!;
    public string Dosage { get; set; } = null!;
    public string? CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public string? ModifiedBy { get; set; }
    public DateTime ModifiedOn { get; set; }
}
