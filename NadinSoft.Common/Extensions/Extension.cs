using Microsoft.EntityFrameworkCore;
using NadinSoft.Common.DTOs;

namespace NadinSoft.Common.Extensions
{
    public static class Extension
    {
        public static PaginationDto<T> GetPaged<T>(this IQueryable<T> source, int page, int limit) where T :class
        {
            var skip = (page - 1) * limit;

            var count = source.Count();

            var listedSource = source.ToList();

            var pagedDto = new PaginationDto<T>()
            {
                PageNumber = page,
                PageSize = limit,
                Source = listedSource,
                TotalCount = count
            };

            return pagedDto;
        }
    }
}
