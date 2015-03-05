using System.Data;

namespace Framesharp.Persistence.Interfaces
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