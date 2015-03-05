using System;
using System.Collections;
using System.Collections.Generic;

namespace Framesharp.Collection
{
    public abstract class PagedListBase<T> : IPagedList<T>
    {
        protected IList<T> InnerList;

        public int PageNumber { get; protected set; }
        public int PageSize { get; protected set; }
        public int PageCount { get { return (int)Math.Ceiling((double)Count / PageSize); } }

        public abstract int Count { get; }

        public virtual int IndexOf(T item)
        {
            return InnerList.IndexOf(item);
        }

        public virtual T this[int index]
        {
            get { return InnerList[index]; }
            set { InnerList[index] = value; }
        }

        protected PagedListBase(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.</returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/> that can be used to iterate through the collection.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        /// Checks for an item in the current pages item collection
        /// </summary>
        /// <param name="item">Item to be searched in the current pages item collection</param>
        /// <returns></returns>
        public virtual bool Contains(T item)
        {
            return InnerList.Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
        }

        public abstract void ChangePage(int pageNumber);

        public abstract void Insert(int index, T item);

        public abstract void RemoveAt(int index);

        public abstract void Add(T item);

        public abstract void Clear();

        public abstract bool IsReadOnly { get; }

        public abstract bool Remove(T item);
    }
}
