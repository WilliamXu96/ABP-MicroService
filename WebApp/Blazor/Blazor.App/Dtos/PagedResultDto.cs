using System.Collections.Generic;

namespace Blazor.App.Dtos
{
    public class PagedResultDto<T> where T : class
    {
        public int TotalCount { get; set; }

        public List<T> Items { get; set; }
    }
}
