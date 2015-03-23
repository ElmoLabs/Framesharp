using System;
using Framesharp.Core.Interfaces;
using Framesharp.DependencyInjection;
using Framesharp.Data.Interfaces;

namespace Framesharp.Core
{
    public class OperationCallContext : IOperationCallContext
    {
        public Guid InstanceId { get; private set; }

        /// <summary>
        /// NHibernate session instance
        /// </summary>
        public ISessionContainer SessionContainer { get; set; }
        
        public OperationCallContext()
        {
            InstanceId = Guid.NewGuid();
        }

        public void ResetSessionContainer(bool commitTransactions)
        {
            if (commitTransactions) SessionContainer.CommitTransaction(); else SessionContainer.RollbackTransaction();

            SessionContainer = DependencyResolver.GetInstance<ISessionContainer>();
        }

        public void Dispose()
        {
            SessionContainer.Dispose();
        }

        public static IOperationCallContext Current
        {
            get 
            {
                return DependencyResolver.GetInstance<IOperationCallContext>();
            }
        }
    }
}
