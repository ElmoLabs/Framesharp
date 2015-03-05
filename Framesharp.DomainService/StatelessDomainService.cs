using Framesharp.Core.Interfaces;
using Framesharp.DomainService.Interfaces;

namespace Framesharp.DomainService
{
    public abstract class StatelessDomainServiceBase : IStatelessDomainService
    {
        public IStatelessOperationCallContext StatelessOperationCallContext { get; private set; }

        protected StatelessDomainServiceBase(IStatelessOperationCallContext context)
        {
            StatelessOperationCallContext = context;
        }

        public void Dispose()
        {
        }
    }
}
