using System.Data;
using Framesharp.Persistence.Interfaces;
using NHibernate;
using System;

namespace Framesharp.Persistence
{
    public abstract class SessionContainerBase : ITransactionManager
    {
        protected ISession Session { get; set; }
        protected IStatelessSession StatelessSession { get; set; }

        protected ITransaction Transaction;

        public abstract bool HasActiveTransaction { get; }

        public abstract void BeginTransaction();

        public abstract void BeginTransaction(IsolationLevel isolationLevel);

        public abstract void CommitTransaction();

        public abstract void RollbackTransaction();

        public abstract void Refresh(object obj);

        public abstract void Flush();

        public abstract void Dispose();
    }
}
