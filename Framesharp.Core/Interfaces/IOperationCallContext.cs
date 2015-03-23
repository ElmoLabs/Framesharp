using System;
using Framesharp.Data.Interfaces;

namespace Framesharp.Core.Interfaces
{
    public interface IOperationCallContext : IDisposable
    {
        Guid InstanceId { get; }

        /// <summary>
        /// NHibernate session instance
        /// </summary>
        ISessionContainer SessionContainer { get; }

        void ResetSessionContainer(bool commitTransactions);
    }
}