namespace Common.Entity;

public class BaseEntity<T> : EntityDef
{
    public T Id { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public bool IsDeleted { get; set; }
}