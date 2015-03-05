using NHibernate;

namespace Framesharp.Persistence.Interfaces
{
    public interface ISessionProvider
    {
        ISession GetSession();
    }
}
