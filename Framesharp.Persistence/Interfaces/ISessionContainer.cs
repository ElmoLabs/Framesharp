using System;

namespace Framesharp.Persistence.Interfaces
{
    public interface ISessionContainer : ITransactionManager, IDisposable
    {
        void Refresh(object obj);

        void Flush();
    }
}
