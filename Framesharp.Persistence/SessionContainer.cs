using System;
using System.Data;
using Framesharp.Persistence.Interfaces;
using NHibernate;

namespace Framesharp.Persistence
{
    public class SessionContainer : SessionContainerBase, ISessionContainer, ISessionProvider
    {
        public SessionContainer(ISession session)
        {
            Session = session;

            Session.FlushMode = FlushMode.Always;
        }

        public ISession GetSession()
        {
            return Session;
        }

        public override bool HasActiveTransaction
        {
            get { return Transaction != null && Transaction.IsActive; }
        }

        public override void BeginTransaction()
        {
            Transaction = Session.BeginTransaction();
        }

        public override void BeginTransaction(IsolationLevel isolationLevel)
        {
            Transaction = Session.BeginTransaction(isolationLevel);
        }

        public override void CommitTransaction()
        {
            Transaction.Commit();
        }

        public override void RollbackTransaction()
        {
            Transaction.Rollback();
        }

        public override void Refresh(object obj)
        {
            Session.Refresh(obj);
        }

        public override void Flush()
        {
            Session.Flush();
        }

        public override void Dispose()
        {
            if (Transaction != null && Transaction.IsActive)
            {
                Transaction.Rollback();
                Transaction.Dispose();
            }

            if (Session != null && Session.IsOpen)
            {
                Session.Close();
                Session.Dispose();
            }
        }
    }
}
