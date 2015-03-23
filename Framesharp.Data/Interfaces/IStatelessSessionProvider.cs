using NHibernate;

namespace Framesharp.Data.Interfaces
{
    public interface IStatelessSessionProvider
    {
        IStatelessSession GetSession();
    }
}
