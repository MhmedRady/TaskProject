using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace NewProject.WebAPI.Extensions
{
    public static class APIPagerControllerExtensions
    {
        public static async Task<IActionResult> GetPagedData<T>(this ControllerBase controller, int currentPage, IQueryable<T> list, int pageSize = 20, int? listCount = null, dynamic info = null)
        {
            pageSize = pageSize > 100 ? 100 : pageSize;
            int previousPage = currentPage - 1;
            int startIndex = previousPage * pageSize;
            startIndex = startIndex > 0 ? startIndex : 0;

            try
            {
                var pagedData = await list
                    .Skip(startIndex)
                    .Take(pageSize)
                    .ToListAsync();

                if (currentPage <= 1)
                {
                    var pagesCount = Math.Ceiling(Convert.ToDecimal(list.Count()) / Convert.ToDecimal(pageSize));
                    return controller.Ok(new
                    {
                        Success = true,
                        PagesCount = pagesCount,
                        Data = pagedData,
                        CurrentPage = currentPage,
                        PageSize = pageSize,
                        ListCount = listCount,
                        Message = "Operation Completed Successfully",
                        info
                    });
                }
                return controller.Ok(new
                {
                    Message = "Operation Completed Successfully",
                    Success = true,
                    Data = pagedData,
                    CurrentPage = currentPage,
                    PageSize = pageSize,
                    PagesCount = pagedData.Count(),
                    ListCount = listCount,
                });
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
