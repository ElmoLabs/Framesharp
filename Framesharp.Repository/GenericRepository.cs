using Framesharp.Core.Interfaces;
using Framesharp.Domain;

namespace Framesharp.Repository
{
    public class GenericRepository<T> : RepositoryBase<T> where T : class, IDomainObject
    {
        public GenericRepository(IOperationCallContext context): base(context)
        {
        }
    }
}
