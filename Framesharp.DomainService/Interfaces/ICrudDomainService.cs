using System.Collections;
using System.Collections.Generic;
using Framesharp.Domain;
using Framesharp.Data.Interfaces;
using Framesharp.Repository;

namespace Framesharp.DomainService.Interfaces
{
    public interface ICrudDomainService<T> : IDomainService, IPersistenceManager<T> where T : class, IDomainObject
    {
    }
}
