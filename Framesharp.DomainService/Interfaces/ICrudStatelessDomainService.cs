using Framesharp.Domain;
using Framesharp.Data.Interfaces;
using Framesharp.Repository;

namespace Framesharp.DomainService.Interfaces
{
    public interface ICrudStatelessDomainService<T> : IStatelessDomainService, IPersistenceManager<T> where T : class, IDomainObject
    {
    }
}
