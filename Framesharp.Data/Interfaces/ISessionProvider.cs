using NHibernate;

namespace Framesharp.Data.Interfaces
{
    public interface ISessionProvider
    {
        ISession GetSession();
    }
}
