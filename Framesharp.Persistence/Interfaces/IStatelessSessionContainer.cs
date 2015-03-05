using System;

namespace Framesharp.Persistence.Interfaces
{
    public interface IStatelessSessionContainer : ITransactionManager, IDisposable
    {
    }
}
