namespace Hospital.Domain.Entities.Common.Entity;

public interface IAuditEntity
{
    string? CreatedBy { get; set; }
    DateTime CreatedOn { get; set; }
    string? ModifiedBy { get; set; }
    DateTime ModifiedOn { get; set; }
}
