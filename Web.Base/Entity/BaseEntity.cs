namespace WebBase.Entity;


public abstract class BaseEntity
{
    public DateTime InsertDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool IsActive { get; set; }
}