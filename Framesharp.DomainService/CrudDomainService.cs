using System.Collections;
using System.Collections.Generic;
using Framesharp.Collection;
using Framesharp.Core.Interfaces;
using Framesharp.Data.Transaction;
using Framesharp.DependencyInjection;
using Framesharp.Domain;
using Framesharp.DomainService.Interfaces;
using Framesharp.Repository;

namespace Framesharp.DomainService
{
    public class CrudDomainService<T> : DomainServiceBase, ICrudDomainService<T> where T : class, IDomainObject
    {
        protected IRepository<T> Repository { get; private set; }

        public CrudDomainService(IOperationCallContext operationCallContext)
            : base(operationCallContext)
        {
            Repository = DependencyResolver.GetInstance<IRepository<T>>("operationCallContext", operationCallContext);
        }
        
        [TransactionScope]
        public virtual void Save(T entity)
        {
            Repository.Save(entity);
        }

        [TransactionScope]
        public virtual void Update(T entity)
        {
            Repository.Update(entity);
        }

        [TransactionScope]
        public virtual void Delete(T entity)
        {
            Repository.Delete(entity);
        }

        [TransactionScope]
        public virtual void SaveOrUpdate(T entity)
        {
            Repository.SaveOrUpdate(entity);
        }

        public virtual void Refresh(T entity)
        {
            Repository.Refresh(entity);
        }

        public virtual void Evict(T entity)
        {
            Repository.Evict(entity);
        }

        public virtual T Get(object id)
        {
            return Get(id, false);
        }

        public virtual T Get(string columnName, object columnValue)
        {
            return Repository.Get(columnName, columnValue);
        }

        public virtual T GetByCriteria(IDictionary filterParameters)
        {
            return Repository.GetByCriteria(filterParameters);
        }

        public virtual T Get(object id, bool updateLock)
        {
            return Repository.Get(id, updateLock);
        }

        #region List methods

        public virtual IList<T> ListAllAscending(IDictionary criteriaCollection)
        {
            return ListAllAscending(criteriaCollection, null);
        }

        public virtual IList<T> ListAllAscending(IDictionary criteriaCollection, string orderByPropertyName)
        {
            return Repository.ListAllAscending(criteriaCollection, orderByPropertyName);
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
            return Repository.ListAllDescending(criteriaCollection, orderByPropertyName);
        }

        public virtual IList<T> ListAllDescending(string orderByPropertyName)
        {
            return ListAllDescending(null, orderByPropertyName);
        }

        public virtual IList<T> ListAll()
        {
            return ListAll(null);
        }

        public virtual IList<T> ListAll(string columnName, object columnValue)
        {
            return Repository.ListAll(columnName, columnValue);
        }

        public virtual IList<T> ListAll(IDictionary criteriaCollection)
        {
            return Repository.ListAll(criteriaCollection);
        }

        #endregion

        #region PagedList methods

        public virtual IPagedList<T> ListAllAscending(IDictionary criteriaCollection, int pageNumber, int pageSize)
        {
            return ListAllAscending(criteriaCollection, null, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAllAscending(IDictionary criteriaCollection, string orderByPropertyName, int pageNumber, int pageSize)
        {
            return Repository.ListAllAscending(criteriaCollection, orderByPropertyName, pageNumber, pageSize);
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
            return Repository.ListAllDescending(criteriaCollection, orderByPropertyName, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAllDescending(string orderByPropertyName, int pageNumber, int pageSize)
        {
            return ListAllDescending(null, orderByPropertyName, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAll(int pageNumber, int pageSize)
        {
            return ListAll(null, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAll(string columnName, object columnValue, int pageNumber, int pageSize)
        {
            return Repository.ListAll(columnName, columnValue, pageNumber, pageSize);
        }

        public virtual IPagedList<T> ListAll(IDictionary criteriaCollection, int pageNumber, int pageSize)
        {
            return Repository.ListAll(criteriaCollection, pageNumber, pageSize);
        }

        #endregion
    }

    public class CrudDomainService<T, TRepository> : CrudDomainService<T> where T : class, IDomainObject
    {
        protected new TRepository Repository { get; private set; }

        public CrudDomainService(IOperationCallContext operationCallContext)
            : base(operationCallContext)
        {
            Repository = DependencyResolver.GetInstance<TRepository>("operationCallContext", operationCallContext);
        }

        public override void Dispose()
        {
            
        }
    }
}
