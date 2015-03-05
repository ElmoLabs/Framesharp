using Framesharp.Configuration;
using Framesharp.DomainService;
using Framesharp.DomainService.Interfaces;
using DSL = StructureMap.Configuration.DSL;

namespace Framesharp.DependencyInjection.Registry
{
    public class DomainServiceRegistry : DSL.Registry
    {
        public DomainServiceRegistry()
        {
            For(typeof(ICrudDomainService<>)).Use(typeof(CrudDomainService<>));

            For(typeof(ICrudStatelessDomainService<>)).Use(typeof(CrudStatelessDomainService<>));
        }
    }
}
