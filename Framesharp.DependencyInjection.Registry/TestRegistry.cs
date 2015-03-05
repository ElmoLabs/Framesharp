using Framesharp.Persistence.Interfaces;
using Framesharp.Test.Persistence;

namespace Framesharp.DependencyInjection.Registry
{
    public class TestRegistry : StructureMap.Configuration.DSL.Registry
    {
        public TestRegistry()
        {
            ForConcreteType<EquivalencePartitioning>().Configure.Ctor<IStatelessSessionContainer>("sessionContainer").Is
                (x => x.GetInstance<IStatelessSessionContainer>());
        }
    }
}
