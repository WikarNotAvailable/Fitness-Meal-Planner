using WebAPI.Filters;
using WebAPI.Wrappers;

namespace WebAPI.Helpers
{
    public class PaginationHelper
    {
        public static PagedResponse<IEnumerable<T>> CreatePagedResponse<T>(IEnumerable<T> pagedData, PaginationFilter validPaginationFilter, int totalRecords)
        {
            var response = new PagedResponse<IEnumerable<T>>(pagedData, validPaginationFilter.pageNumber, validPaginationFilter.pageSize);
            var totalPages = ((double)totalRecords / (double)validPaginationFilter.pageSize);
            var roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            int currentPage = validPaginationFilter.pageNumber;

            response.totalPages = roundedTotalPages;
            response.totalRecords = totalRecords;
            response.previousPage = currentPage > 1 ? true : false;
            response.nextPage = currentPage < roundedTotalPages ? true : false;

            return response;
        }
    }
}
