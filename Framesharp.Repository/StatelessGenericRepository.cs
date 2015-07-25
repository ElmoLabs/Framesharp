using Framesharp.Core.Interfaces;
using Framesharp.Domain;

namespace Framesharp.Repository
{
    public class StatelessGenericRepository<T> : StatelessRepositoryBase<T> where T : class, IDomainObject
    {
        public StatelessGenericRepository(IStatelessOperationCallContext operationCallContext)
            : base(operationCallContext)
        {
        }
    }
}
