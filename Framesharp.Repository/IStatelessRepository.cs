using Framesharp.Core.Interfaces;
using Framesharp.Data.Interfaces;
using Framesharp.Domain;

namespace Framesharp.Repository
{
    public interface IStatelessRepository<T> : IRepository<T> where T : class, IDomainObject
    {
        IStatelessOperationCallContext StatelessOperationCallContext { get; } 
    }
}
