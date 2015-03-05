using System;
using Framesharp.Persistence.Interfaces;

namespace Framesharp.Core.Interfaces
{
    public interface IStatelessOperationCallContext : IDisposable
    {
        /// <summary>
        /// NHibernate stateless session instance
        /// </summary>
        IStatelessSessionContainer StatelessSessionContainer { get; }

        void ResetSessionContainer(bool commitTransactions);
    }
}