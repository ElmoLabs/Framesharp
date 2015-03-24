using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Framesharp.Collection;
using Framesharp.Data.Interfaces;
using Framesharp.Domain;

namespace Framesharp.Repository
{
    public interface IRepository
    {
    }

    public interface IRepository<T> : IRepository , IPersistenceManager<T> where T : class, IDomainObject
    {
    }
}
