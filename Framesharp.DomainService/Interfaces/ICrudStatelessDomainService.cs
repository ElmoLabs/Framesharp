using Framesharp.Domain;
using Framesharp.Persistence.Interfaces;
using Framesharp.Repository;

namespace Framesharp.DomainService.Interfaces
{
    public interface ICrudStatelessDomainService<T> : IStatelessDomainService, IPersistenceService<T> where T : class, IDomainObject
    {
    }

    public interface ICrudStatelessDomainService<T, TRepository> : ICrudStatelessDomainService<T>
        where T : class, IDomainObject
        where TRepository : IRepository<T>
    {
    }
}
