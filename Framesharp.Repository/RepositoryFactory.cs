using Framesharp.Core.Interfaces;
using Framesharp.DependencyInjection;

namespace Framesharp.Repository
{
    public static class RepositoryFactory
    {
        public static T Get<T>(IOperationCallContext operationCallContext) where T : IRepository
        {
            return DependencyResolver.GetInstance<T>("operationCallContext", operationCallContext);
        }

        public static T GetStateless<T>() where T : IRepository
        {
            IStatelessOperationCallContext operationCallContext =
                    DependencyResolver.GetInstance<IStatelessOperationCallContext>();

            return DependencyResolver.GetInstance<T>("operationCallContext", operationCallContext);
        }
    }
}