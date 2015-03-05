using Framesharp.Configuration;
using Framesharp.Core;
using Framesharp.Core.Interfaces;
using Framesharp.Persistence.Interfaces;
using StructureMap.Web;

namespace Framesharp.DependencyInjection.Registry
{
    public class CoreRegistry : StructureMap.Configuration.DSL.Registry
    {
        public CoreRegistry()
        {
            For<IOperationCallContext>().HybridHttpOrThreadLocalScoped().Use<OperationCallContext>().Setter<ISessionContainer>().Is(x => x.GetInstance<ISessionContainer>());

            For<IStatelessOperationCallContext>().HybridHttpOrThreadLocalScoped().Use<StatelessOperationCallContext>().Setter<IStatelessSessionContainer>().Is(x => x.GetInstance<IStatelessSessionContainer>());
        }
    }
}