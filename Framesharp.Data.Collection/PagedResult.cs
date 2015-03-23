using System;
using Framesharp.Collection;
using NHibernate;
using NHibernate.Criterion;

namespace Framesharp.Data.Collection
{
    public sealed class PagedResult<T> : PagedListBase<T>
    {
        private readonly ICriteria _criteria;

        private readonly int _count;

        public override int Count { get { return _count; } }

        public override T this[int index]
        {
            get { return InnerList[index]; }
            set { throw new InvalidOperationException("This collection is readonly"); }
        }

        public PagedResult(ICriteria criteria, int pageNumber, int pageSize) : base(pageNumber, pageSize)
        {
            _criteria = criteria;

            _count = GetCount();

            ChangePage(pageNumber);
        }

        public override void ChangePage(int pageNumber)
        {
            InnerList = _criteria.SetMaxResults(PageSize).SetFirstResult((pageNumber - 1) * PageSize).List<T>();
        }

        public override void Insert(int index, T item)
        {
            throw new InvalidOperationException("This collection is readonly.");
        }

        public override void RemoveAt(int index)
        {
            throw new InvalidOperationException("This collection is readonly.");
        }

        public override void Add(T item)
        {
            throw new InvalidOperationException("This collection is readonly.");
        }

        public override void Clear()
        {
            throw new InvalidOperationException("This collection is readonly.");
        }

        public override bool IsReadOnly
        {
            get { return true; }
        }

        public override bool Remove(T item)
        {
            throw new InvalidOperationException("This collection is readonly.");
        }

        private int GetCount()
        {
            ICriteria criteria = _criteria.Clone() as ICriteria;

            if (criteria == null)
                throw new InvalidOperationException("Could not retrieve resulting row count.");

            criteria.ClearOrders();
            criteria.SetProjection(Projections.Count(Projections.Id()));

            return criteria.UniqueResult<int>();
        }
    }
}
