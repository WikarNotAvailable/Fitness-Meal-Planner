namespace WebAPI.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
        public int totalPages { get; set; }
        public int totalRecords { get; set; }
        public bool nextPage { get; set; }
        public bool previousPage { get; set; }

        public PagedResponse (T _data, int _pageNumber, int _pageSize)
        {
            pageNumber = _pageNumber;
            data = _data;
            pageSize = _pageSize;
            succeeded = true;
        }
    }
}
