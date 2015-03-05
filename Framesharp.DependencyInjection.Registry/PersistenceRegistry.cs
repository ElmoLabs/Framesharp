using Framesharp.Persistence;
using Framesharp.Persistence.Interfaces;
using Framesharp.Test.Persistence;

namespace Framesharp.DependencyInjection.Registry
{
    public class PersistenceRegistry : StructureMap.Configuration.DSL.Registry
    {
        public PersistenceRegistry()
        {
            ForConcreteType<EquivalencePartitioning>().Configure.Ctor<IStatelessSessionContainer>("sessionContainer").Is
                (x => x.GetInstance<IStatelessSessionContainer>());
        }
    }
}
