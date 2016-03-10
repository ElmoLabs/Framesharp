using Framesharp.Configuration;
using Framesharp.Repository;

namespace Framesharp.DependencyInjection.Registry
{
    public class RepositoryRegistry : StructureMap.Registry
    {
        public RepositoryRegistry()
        {
            For(typeof(IRepository<>)).Use(typeof(GenericRepository<>));

            For(typeof(IStatelessRepository<>)).Use(typeof(StatelessGenericRepository<>));
        }
    }
}
