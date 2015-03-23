using Framesharp.Domain;
using Framesharp.Data.Interfaces;
using Framesharp.Repository;

namespace Framesharp.DomainService.Interfaces
{
    public interface ICrudStatelessDomainService<T> : IStatelessDomainService, IDataService<T> where T : class, IDomainObject
    {
    }

    public interface ICrudStatelessDomainService<T, TRepository> : ICrudStatelessDomainService<T>
        where T : class, IDomainObject
        where TRepository : IRepository<T>
    {
    }
}
