using Framesharp.Core.Interfaces;
using Framesharp.DependencyInjection;
using Framesharp.DomainService.Interfaces;
using Framesharp.Data.Interfaces;

namespace Framesharp.DomainService
{
    public static class DomainServiceFactory
    {
        public static T Get<T>(IOperationCallContext operationCallContext) where T : IPersistenceManager
        {
            return DependencyResolver.GetInstance<T>("operationCallContext", operationCallContext);
        }

        public static T GetStateless<T>() where T : IStatelessDomainService
        {
            IStatelessOperationCallContext operationCallContext =
                    DependencyResolver.GetInstance<IStatelessOperationCallContext>();

            return DependencyResolver.GetInstance<T>("operationCallContext", operationCallContext);
        }
    }
}