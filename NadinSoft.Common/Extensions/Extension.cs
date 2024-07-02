using Microsoft.EntityFrameworkCore;
using NadinSoft.Common.DTOs;

namespace NadinSoft.Common.Extensions
{
    public static class Extension
    {
        public static async Task<PaginationDto<T>> GetPaged<T>(this IQueryable<T> source, int page, int limit) where T :class
        {
            var skip = (page - 1) * limit;

            var count = source.Count();

            var listedSource = await source.Skip(skip).Take(limit).ToListAsync();

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
