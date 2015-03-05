using Framesharp.Core.Interfaces;
using Framesharp.DomainService.Interfaces;

namespace Framesharp.DomainService
{
    public abstract class DomainServiceBase : IDomainService
    {
        public IOperationCallContext OperationCallContext { get; private set; }

        protected DomainServiceBase(IOperationCallContext context)
        {
            OperationCallContext = context;
        }

        public virtual void Dispose()
        {
        }
    }
}
