using Hospital.Domain.Entities.Common.Entity;
using Microsoft.AspNetCore.Identity;

namespace Hospital.Domain.Entities;


public class Patient: AuditEntity
{
    public int PatientId { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public string Address { get; set; }
}
