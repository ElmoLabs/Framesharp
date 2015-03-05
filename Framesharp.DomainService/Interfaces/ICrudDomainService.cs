using System.Collections;
using System.Collections.Generic;
using Framesharp.Domain;
using Framesharp.Persistence.Interfaces;
using Framesharp.Repository;

namespace Framesharp.DomainService.Interfaces
{
    public interface ICrudDomainService<T> : IDomainService, IPersistenceService<T> where T : class, IDomainObject
    {
    }

    public interface ICrudDomainService<T, TRepository> : ICrudDomainService<T>
        where T : class, IDomainObject
        where TRepository : IRepository<T>
    {
    }
}
