namespace Hospital.Domain.Entities;

public class MedicalRecord
{
    public int MedicalRecordID { get; set; }
    public int PatientID { get; set; }
    public DateTime DateOfVisit { get; set; }
    public string Diagnosis { get; set; } = null!;
    public int PrescriptionID { get; set; }
    public Patient Patient { get; set; } = new();
    public Prescription Prescription { get; set; } = new();

}