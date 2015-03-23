using NHibernate;
using Framesharp.Data;
using Framesharp.Data.Interfaces;

namespace Framesharp.DependencyInjection.Registry
{
    public class NHibernateRegistry : StructureMap.Configuration.DSL.Registry
    {
        public NHibernateRegistry(ISessionFactory sessionFactory)
        {
            For<ISessionFactory>().Singleton().Use(sessionFactory);

            For<ISession>().Use(x => x.GetInstance<ISessionFactory>().OpenSession());

            For<IStatelessSession>().Use(x => x.GetInstance<ISessionFactory>().OpenStatelessSession());

            For<ISessionContainer>().Use<SessionContainer>().Ctor<ISession>("session").Is(x => x.GetInstance<ISession>());

            For<IStatelessSessionContainer>().Use<StatelessSessionContainer>().Ctor<IStatelessSession>("session").Is(x => x.GetInstance<IStatelessSession>());
        }
    }
}