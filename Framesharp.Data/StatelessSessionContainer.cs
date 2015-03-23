using System.Data;
using Framesharp.Data.Interfaces;
using NHibernate;

namespace Framesharp.Data
{
    public class StatelessSessionContainer : SessionContainerBase, IStatelessSessionContainer, IStatelessSessionProvider
    {
        public StatelessSessionContainer(IStatelessSession statelessSession)
        {
            StatelessSession = statelessSession;
        }

        public IStatelessSession GetSession()
        {
            return StatelessSession;
        }

        public override bool HasActiveTransaction
        {
            get { return Transaction.IsActive; }
        }

        public override void BeginTransaction()
        {
            Transaction = StatelessSession.BeginTransaction();
        }

        public override void BeginTransaction(IsolationLevel isolationLevel)
        {
            Transaction = StatelessSession.BeginTransaction(isolationLevel);
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

            if (StatelessSession != null && StatelessSession.IsOpen)
            {
                StatelessSession.Close();
                StatelessSession.Dispose();
            }
        }
    }
}
