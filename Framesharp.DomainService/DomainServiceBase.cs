using Framesharp.Core.Interfaces;
using Framesharp.DomainService.Interfaces;

namespace Framesharp.DomainService
{
    public abstract class DomainServiceBase : IDomainService
    {
        public IOperationCallContext OperationCallContext { get; private set; }

        protected DomainServiceBase(IOperationCallContext operationCallContext)
        {
            OperationCallContext = operationCallContext;
        }

        public virtual void Dispose()
        {
        }
    }
}
