using System;

namespace Framesharp.Data.Interfaces
{
    public interface ISessionContainer : ITransactionManager, IDisposable
    {
        void Refresh(object obj);

        void Flush();
    }
}
