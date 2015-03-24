using Framesharp.Core.Interfaces;
using Framesharp.DependencyInjection;
using Framesharp.DomainService.Interfaces;
using Framesharp.Data.Interfaces;

namespace Framesharp.DomainService
{
    public static class DomainServiceFactory
    {
        public static T Get<T>(IOperationCallContext context) where T : IPersistenceManager
        {
            return DependencyResolver.GetInstance<T>("operationCallContext", context);
        }

        public static T GetStateless<T>() where T : IStatelessDomainService
        {
            IStatelessOperationCallContext context =
                    DependencyResolver.GetInstance<IStatelessOperationCallContext>();

            return DependencyResolver.GetInstance<T>("operationCallContext", context);
        }
    }
}