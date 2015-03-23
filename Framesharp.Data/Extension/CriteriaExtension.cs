using System;
using Framesharp.Data.Collection;
using Framesharp.Collection;
using NHibernate.Criterion;

namespace Framesharp.Data.Extension
{
    public static class CriteriaExtension
    {
        public static IPagedList<T> List<T>(this NHibernate.ICriteria extended, int startPageNumber, int pageSize)
        {
            return new PagedResult<T>(extended, startPageNumber, pageSize);
        }

        public static IPagedList<T> List<T>(this NHibernate.ICriteria extended, int pageSize)
        {
            return new PagedResult<T>(extended, 1, pageSize);
        }

        public static NHibernate.ICriteria AddOrder(this NHibernate.ICriteria extended, string orderByExpression, string orderByDirection)
        {
            if (string.IsNullOrEmpty(orderByExpression))
                throw new ArgumentNullException("orderByExpression", "OrderBy expression is empty");

            if (string.IsNullOrEmpty(orderByDirection))
                throw new ArgumentNullException("orderByDirection", "OrderBy direction is empty");

            switch (orderByDirection)
            {
                case "Asc":
                case "asc":
                case "ASC":
                case "Ascending":
                case "ascending":
                case "ASCENDING":

                    extended.AddOrder(Order.Asc(orderByExpression));

                    break;
                case "Desc":
                case "desc":
                case "DESC":
                case "Descending":
                case "descending":
                case "DESCENDING":

                    extended.AddOrder(Order.Desc(orderByExpression));

                    break;
                default:
                    throw new ArgumentException("OrderBy direction is invalid", "orderByDirection");
            }

            return extended;
        }
    }
}