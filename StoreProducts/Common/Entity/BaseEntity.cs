using System.Security.Cryptography;

namespace Common.Entity;

public class BaseEntity<TId> : EntityDef where TId : struct, IComparable
{
    public TId Id { get; set; }
    public DateTime CreateDateTime { get; set; }
    public DateTime? UpdateDateTime { get; set; }
    public bool IsDeleted { get; set; }
}