using System.ComponentModel.DataAnnotations;
using static System.Int32;
namespace Common.Input
{
    public class BaseListInput
    {
        [Range(1, MaxValue)] public virtual int? Page { get; set; } = 1;
        [Range(1, MaxValue)] public virtual int PageSize { get; set; } = 9999;
    }
}
