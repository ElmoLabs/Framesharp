using Framesharp.Core.Interfaces;
using Framesharp.DependencyInjection;

namespace Framesharp.Repository
{
    public static class RepositoryFactory
    {
        public static T Get<T>(IOperationCallContext context) where T : IRepository
        {
            return DependencyResolver.GetInstance<T>("context", context);
        }

        public static T GetStateless<T>() where T : IRepository
        {
            IStatelessOperationCallContext context =
                    DependencyResolver.GetInstance<IStatelessOperationCallContext>();

            return DependencyResolver.GetInstance<T>("context", context);
        }
    }
}