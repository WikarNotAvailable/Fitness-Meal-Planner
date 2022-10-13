using WebAPI.Filters;
using WebAPI.Wrappers;

namespace WebAPI.Helpers
{
    //helper creating proper paged response
    public class PaginationHelper
    {
        public static PagedResponse<IEnumerable<T>> CreatePagedResponse<T>(IEnumerable<T> pagedData, PaginationFilter validPaginationFilter, int totalRecords)
        {
            var response = new PagedResponse<IEnumerable<T>>(pagedData, validPaginationFilter.PageNumber, validPaginationFilter.PageSize);
            var totalPages = ((double)totalRecords / (double)validPaginationFilter.PageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            int currentPage = validPaginationFilter.PageNumber;

            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            response.PreviousPage = currentPage > 1 ? true : false;
            response.NextPage = currentPage < roundedTotalPages ? true : false;

            return response;
        }
    }
}
