using NHibernate;

namespace Framesharp.Persistence.Interfaces
{
    public interface IStatelessSessionProvider
    {
        IStatelessSession GetSession();
    }
}
