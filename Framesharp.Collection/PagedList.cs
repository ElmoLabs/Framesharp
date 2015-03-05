using System.Collections.Generic;
using System.Linq;

namespace Framesharp.Collection
{
    public sealed class PagedList<T> : PagedListBase<T>
    {
        private readonly IList<T> _fullList;

        public override int Count { get { return _fullList.Count; } }
        
        public PagedList(IList<T> sourceList, int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
            _fullList = sourceList;
        }

        public override void ChangePage(int pageNumber)
        {
            InnerList = _fullList.Skip((pageNumber - 1) * PageSize).Take(PageSize).ToList<T>();
        }

        public override void Insert(int index, T item)
        {
            InnerList.RemoveAt(index);
        }

        public override void RemoveAt(int index)
        {
            InnerList.RemoveAt(index);
        }

        public override void Add(T item)
        {
            InnerList.Add(item);
        }

        public override void Clear()
        {
            InnerList.Clear();
        }

        public override bool IsReadOnly
        {
            get { return InnerList.IsReadOnly; }
        }

        public override bool Remove(T item)
        {
            return InnerList.Remove(item);
        }
    }
}
