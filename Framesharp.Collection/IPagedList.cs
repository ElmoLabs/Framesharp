using System.Collections.Generic;

namespace Framesharp.Collection
{
    public interface IPagedList<T> : IList<T>
    {
        int PageCount { get; }
        
        int PageNumber { get; }

        int PageSize { get; }

        void ChangePage(int pageNumber);
    }
}