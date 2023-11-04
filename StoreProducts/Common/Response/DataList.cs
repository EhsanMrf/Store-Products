namespace Common.Response
{ public class DataList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Page { get; set; }
        public int TotalPage { get; set; }
        public int TotalCount { get; set; }

        public DataList(IEnumerable<T> items, int totalCount, int page, int pageSize)
        {
            Page = page;
            TotalPage = (int)Math.Ceiling(totalCount / (double)pageSize);
            Items = items;
            TotalCount=totalCount;
        }
    }
}
