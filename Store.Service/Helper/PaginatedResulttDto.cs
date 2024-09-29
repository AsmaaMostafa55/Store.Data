namespace Store.Service.Helper
{
    internal class PaginatedResulttDto<T>
    {
        public PaginatedResulttDto( int pageSize, int pageIndex, int totalCount, IReadOnlyList<T> data)
        {
            PageIndex = pageIndex;
            TotalCount = totalCount;
            PageSize = pageSize;
            Data = data;
        }

        public int PageIndex { get; set; }
        public int TotalCount {  get; set; }
        public int PageSize { get; set; }
        public IReadOnlyList<T> Data { get; set; }
    }
}
