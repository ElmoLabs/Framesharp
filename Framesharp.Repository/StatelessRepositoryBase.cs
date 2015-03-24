using System;
using System.Collections;
using System.Collections.Generic;
using Framesharp.Collection;
using Framesharp.Data;
using Framesharp.Core.Interfaces;
using Framesharp.Domain;
using Framesharp.Data.Extension;
using Framesharp.Data.Interfaces;
using NHibernate;
using NHibernate.Criterion;

namespace Framesharp.Repository
{
    public class StatelessRepositoryBase<T> : IStatelessRepository<T> where T : class, IDomainObject
    {
        public IStatelessOperationCallContext StatelessOperationCallContext { get; private set; }

        protected IStatelessSession Session { get { return ((IStatelessSessionProvider)StatelessOperationCallContext.StatelessSessionContainer).GetSession(); } }

        public StatelessRepositoryBase(IStatelessOperationCallContext context)
        {
            StatelessOperationCallContext = context;
        }

        public virtual void Save(T entity)
        {
            Session.Insert(entity);
        }

        public virtual void Update(T entity)
        {
            Session.Update(entity);
        }

        public virtual void Delete(T entity)
        {
            Session.Delete(entity);
        }
        
        public virtual void SaveOrUpdate(T entity)
        {
            throw new NotImplementedException("Stateless sessions do not support SaveOrUpdate operation. Use Insert or Update instead.");
        }

        public virtual void Refresh(T entity)
        {
            Session.Refresh(entity);
        }

        public virtual void Evict(T entity)
        {
            throw new NotImplementedException("Stateless sessions do not support Evict operation because there is no first-cache activated.");
        }

        public virtual T Get(object id)
        {
            return Session.Get<T>(id);
        }

        public virtual T Get(string columnName, object columnValue)
        {
            return Session.CreateCriteria<T>().Add(Restrictions.Eq(columnName, columnValue)).UniqueResult<T>();
        }

        public virtual T GetByCriteria(IDictionary criteriaCollection)
        {
            var criteria = Session.CreateCriteria<T>();

            criteria.Add(Restrictions.AllEq(criteriaCollection));

            return criteria.UniqueResult<T>();
        }

        public virtual T Get(object id, bool updateLock)
        {
            if (updateLock)
            {
                return Session.Get<T>(id, LockMode.Upgrade);
            }

            return Session.Get<T>(id);
        }

        public virtual bool VerifyId(object id)
        {
            T entity = Get(id);

            return entity != null;
        }

        public virtual IList<T> ListAllAscending(IDictionary criteriaCollection)
        {
            return ListAllAscending(criteriaCollection, null);
        }

        public virtual IList<T> ListAllAscending(IDictionary criteriaCollection, string orderByPropertyName)
        {
            Order order = string.IsNullOrEmpty(orderByPropertyName) ? null : Order.Asc(orderByPropertyName);

            return ListAll(criteriaCollection, order);
        }

        public virtual IList<T> ListAllAscending(string orderByPropertyName)
        {
            return ListAllAscending(null, orderByPropertyName);
        }

        public virtual IList<T> ListAllDescending(IDictionary criteriaCollection)
        {
            return ListAllDescending(criteriaCollection, null);
        }

        public virtual IList<T> ListAllDescending(IDictionary criteriaCollection, string orderByPropertyName)
        {
            Order order = string.IsNullOrEmpty(orderByPropertyName) ? null : Order.Desc(orderByPropertyName);

            return ListAll(criteriaCollection, order);
        }

        public virtual IList<T> ListAllDescending(string orderByPropertyName)
        {
            return ListAllDescending(null, orderByPropertyName);
        }

        public virtual IList<T> ListAll()
        {
            return ListAll(null, null);
        }

        public virtual IList<T> ListAll(string columnName, object columnValue)
        {
            IDictionary criteriaCollection = new Dictionary<string, object>() { { columnName, columnValue } };

            return ListAll(criteriaCollection);
        }

        public virtual IList<T> ListAll(IDictionary criteriaCollection)
        {
            return ListAll(criteriaCollection, null);
        }

        private IList<T> ListAll(IDictionary criteriaCollection, Order order)
        {
            return CreateCriteria(criteriaCollection, order).List<T>();
        }

        public virtual IPagedList<T> ListAllAscending(IDictionary criteriaCollection, int pageNumber, int pageSize)
        {
            return ListAllAscending(criteriaCollection, null, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAllAscending(IDictionary criteriaCollection, string orderByPropertyName, int pageNumber, int pageSize)
        {
            Order order = string.IsNullOrEmpty(orderByPropertyName) ? null : Order.Asc(orderByPropertyName);

            return ListAll(criteriaCollection, order, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAllAscending(string orderByPropertyName, int pageNumber, int pageSize)
        {
            return ListAllAscending(null, orderByPropertyName, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAllDescending(IDictionary criteriaCollection, int pageNumber, int pageSize)
        {
            return ListAllDescending(criteriaCollection, null, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAllDescending(IDictionary criteriaCollection, string orderByPropertyName, int pageNumber, int pageSize)
        {
            Order order = string.IsNullOrEmpty(orderByPropertyName) ? null : Order.Desc(orderByPropertyName);

            return ListAll(criteriaCollection, order, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAllDescending(string orderByPropertyName, int pageNumber, int pageSize)
        {
            return ListAllDescending(null, orderByPropertyName, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAll(int pageNumber, int pageSize)
        {
            return ListAll(null, null, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAll(string columnName, object columnValue, int pageNumber, int pageSize)
        {
            IDictionary criteriaCollection = new Dictionary<string, object>() { { columnName, columnValue } };

            return ListAll(criteriaCollection, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAll(IDictionary criteriaCollection, int pageNumber, int pageSize)
        {
            return ListAll(criteriaCollection, null, pageNumber, pageSize);
        }

        private IPagedList<T> ListAll(IDictionary criteriaCollection, Order order, int pageNumber, int pageSize)
        {
            return CreateCriteria(criteriaCollection, order).List<T>(pageNumber, pageSize);
        }

        private ICriteria CreateCriteria(IDictionary criteriaCollection, Order order)
        {
            ICriteria criteria = Session.CreateCriteria<T>();

            if (criteriaCollection != null && criteriaCollection.Count > 0)
                criteria.Add(Restrictions.AllEq(criteriaCollection));

            if (order != null)
                criteria.AddOrder(order);

            return criteria;
        }
    }
}
