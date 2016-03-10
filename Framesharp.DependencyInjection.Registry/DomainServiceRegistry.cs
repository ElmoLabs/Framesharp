using Framesharp.Configuration;
using Framesharp.DomainService;
using Framesharp.DomainService.Interfaces;

namespace Framesharp.DependencyInjection.Registry
{
    public class DomainServiceRegistry : StructureMap.Registry
    {
        public DomainServiceRegistry()
        {
            For(typeof(ICrudDomainService<>)).Use(typeof(CrudDomainService<>));

            For(typeof(ICrudStatelessDomainService<>)).Use(typeof(CrudStatelessDomainService<>));
        }
    }
}
