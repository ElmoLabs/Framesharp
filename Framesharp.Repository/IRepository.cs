using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framesharp.Collection;
using Framesharp.Domain;

namespace Framesharp.Repository
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository where T : class, IDomainObject
    {
        T Get(object id);

        T Get(object id, bool updateLock);

        T Get(string columnName, object columnValue);

        T GetByCriteria(IDictionary criteriaCollection);

        void Save(T entity);

        void Update(T entity);

        void Delete(T entity);

        void SaveOrUpdate(T entity);

        void Refresh(T entity);

        void Evict(T entity);

        bool VerifyId(object id);

        IList<T> ListAllAscending(IDictionary criteriaCollection);

        IList<T> ListAllAscending(IDictionary criteriaCollection, string orderByPropertyName);

        IList<T> ListAllAscending(string orderByPropertyName);

        IList<T> ListAllDescending(IDictionary criteriaCollection);

        IList<T> ListAllDescending(IDictionary criteriaCollection, string orderByPropertyName);

        IList<T> ListAllDescending(string orderByPropertyName);

        IList<T> ListAll();

        IList<T> ListAll(IDictionary criteriaCollection);

        IPagedList<T> ListAllAscending(IDictionary criteriaCollection, int pageNumber, int pageSize);

        IPagedList<T> ListAllAscending(IDictionary criteriaCollection, string orderByPropertyName, int pageNumber, int pageSize);

        IPagedList<T> ListAllAscending(string orderByPropertyName, int pageNumber, int pageSize);

        IPagedList<T> ListAllDescending(IDictionary criteriaCollection, int pageNumber, int pageSize);

        IPagedList<T> ListAllDescending(IDictionary criteriaCollection, string orderByPropertyName, int pageNumber, int pageSize);

        IPagedList<T> ListAllDescending(string orderByPropertyName, int pageNumber, int pageSize);

        IPagedList<T> ListAll(int pageNumber, int pageSize);

        IPagedList<T> ListAll(IDictionary criteriaCollection, int pageNumber, int pageSize);
    }
}
