using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace API.Helpers
{
    public class PagedList<T> : List<T>
    {
        public PagedList(IEnumerable<T> items, int count, int pageNumber, int pageLength)
        {
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double) pageLength);
            PageLength = pageLength;
            TotalCount = count;
            AddRange(items);
        }

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageLength { get; set; }
        public int TotalCount { get; set; }

        public static async Task<PagedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageLength)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageNumber - 1) * pageLength).Take(pageLength).ToListAsync();
            return new PagedList<T>(items, count, pageNumber, pageLength);
        }
    }
}