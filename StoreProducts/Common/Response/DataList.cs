using Common.Input;

namespace Common.Response
{
    public class DateList<T>
    {
        public List<T> Items { get; set; }
        public int? TotalCount { get; set; }
        public int? PageCount { get; set; }

        public DateList(List<T> items, int totalCount, BaseListInput input = null)
        {
            input ??= new BaseListInput { Page = 1, PageSize = int.MaxValue };
            Items = items;
            TotalCount = totalCount;
            //PageCount => ?
        }

        public DateList(List<T> items) : this(items, items.Count) { }

    }
}
