using System;

namespace Framesharp.Data.Interfaces
{
    public interface IStatelessSessionContainer : ITransactionManager, IDisposable
    {
    }
}
