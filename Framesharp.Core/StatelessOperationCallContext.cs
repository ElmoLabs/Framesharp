using Framesharp.Core.Interfaces;
using Framesharp.DependencyInjection;
using Framesharp.Persistence.Interfaces;

namespace Framesharp.Core
{
    public class StatelessOperationCallContext : IStatelessOperationCallContext
    {
        /// <summary>
        /// NHibernate stateless session instance
        /// </summary>
        public IStatelessSessionContainer StatelessSessionContainer { get; set; }

        public void ResetSessionContainer(bool commitTransactions)
        {
            if (commitTransactions) StatelessSessionContainer.CommitTransaction(); else StatelessSessionContainer.RollbackTransaction();

            StatelessSessionContainer = DependencyResolver.GetInstance<IStatelessSessionContainer>();
        }

        public void Dispose()
        {
            StatelessSessionContainer.Dispose();
        }
    }
}
