namespace WebAPI.Filters
{
    //filter used for pagination
    public class PaginationFilter
    {
        private const int maxPageSize = 50;
        public int pageNumber { get; set; }
         /*   get
            {
                return pageNumber;
            }
            set
            {
                pageNumber = value < 1 ? 1 : value;
            } 
        } */
        public int pageSize { get; set; }
      /*  {
            get
            {
                return pageSize;
            }
            set
            {
                pageSize = value > maxPageSize ? maxPageSize : value;
            }
        } */
        public PaginationFilter()
        {
            pageNumber = 1;
            pageSize = maxPageSize;
        }
        public PaginationFilter(int _pageNumber, int _pageSize)
        {
            pageNumber = _pageNumber < 1 ? 1 : _pageNumber;
            pageSize = _pageSize > maxPageSize ? maxPageSize : _pageSize;
        }
    }
}
