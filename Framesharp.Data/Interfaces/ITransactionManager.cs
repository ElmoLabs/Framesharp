using System.Data;

namespace Framesharp.Data.Interfaces
{
    public interface ITransactionManager
    {
        bool HasActiveTransaction { get; }

        void BeginTransaction();

        void BeginTransaction(IsolationLevel isolationLevel);

        void CommitTransaction();

        void RollbackTransaction();
    }
}