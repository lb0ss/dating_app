using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace dating_app.api.Helpers
{
    // used to expand the GetUsers() method in DatingRepository to
    // make it return the properties to the client
    public class PageList<T> : List<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PageList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            this.AddRange(items);
        }

        public static async Task<PageList<T>> CreateAsync(IQueryable<T> source, 
        int pageNumber, int pageSize)
        {
            var count = await source.CountAsync();  // calculate the total of source items
            //  determine where to start when a page is requested by reducing
            // the pageSize (count) of the previous page and start from the next pageSize (count)
            var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PageList<T>(items, count, pageNumber, pageSize);
        }
    }
}