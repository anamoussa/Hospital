using Hospital.Domain.Entities.Common.Entity;

namespace Hospital.Domain.Entities;

public class Prescription: AuditEntity
{
    public int PrescriptionId { get; set; }
    public int AppointmentId { get; set; }
    public Appointment Appointment { get; set; }
    public string Medication { get; set; }
    public string Dosage { get; set; }
}
