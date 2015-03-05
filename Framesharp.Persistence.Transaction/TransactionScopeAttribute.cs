using System;
using System.Data;
using System.Xml.Serialization;
using Framesharp.Core.Interfaces;
using Framesharp.Persistence.Interfaces;
using PostSharp.Aspects;

namespace Framesharp.Persistence.Transaction
{
    [Serializable]
    public class TransactionScopeAttribute : OnMethodBoundaryAspect
    {
        private bool _skipTransationHandling;

        [XmlIgnore]
        public bool DefaultRollback { get; set; }

        [XmlIgnore]
        public IsolationLevel IsolationLevel { get; set; }

        public TransactionScopeAttribute()
        {
            _skipTransationHandling = false;

            DefaultRollback = false;

            IsolationLevel = IsolationLevel.ReadCommitted;
        }
        
        /// <summary>
        /// Method executed upon entry of the method to which this aspect is applied.
        /// </summary>
        /// <param name="args">Information about the method being executed.</param>
        public override void OnEntry(MethodExecutionArgs args)
        {
            _skipTransationHandling = GetTransactionManagerFromOperationCaller(args).HasActiveTransaction;

            if (_skipTransationHandling) return;

            Console.WriteLine("Transaction begun on method: {0} {1}", args.Method.Name, DefaultRollback ? "with default rollback set." : string.Empty);

            GetTransactionManagerFromOperationCaller(args).BeginTransaction(isolationLevel: IsolationLevel);
        }

        /// <summary>
        /// Method executed upon failure of the method to which this aspect is applied.
        /// </summary>
        /// <param name="args">Information about the method being executed.</param>
        public override void OnException(MethodExecutionArgs args)
        {
            if (_skipTransationHandling) return;

            Console.WriteLine("Transaction failed");

            GetTransactionManagerFromOperationCaller(args).RollbackTransaction();

            Exception exception = args.Exception;

            while (exception != null)
            {
                Console.WriteLine("Exception message: " + exception.Message);

                exception = exception.InnerException;
            }
        }

        /// <summary>
        /// Method executed upon complete succesful execution of the method to which this aspect is applied.
        /// </summary>
        /// <param name="args">Information about the method being executed.</param>
        public override void OnSuccess(MethodExecutionArgs args)
        {
            if (_skipTransationHandling) return;

            if (DefaultRollback)
            {
                Console.WriteLine("Transaction executed successfuly but rolledback by default");

                GetTransactionManagerFromOperationCaller(args).RollbackTransaction();
            }
            else
            {
                Console.WriteLine("Transaction executed successfuly");

                GetTransactionManagerFromOperationCaller(args).CommitTransaction();
            }
        }

        /// <summary>
        /// Retrieves the transaction manager of the instance to which this method belongs to.
        /// </summary>
        /// <param name="args">Information about the method being executed.</param>
        private ITransactionManager GetTransactionManagerFromOperationCaller(MethodExecutionArgs args)
        {
            IOperationCaller operationCaller = (args.Instance as IOperationCaller);

            if (operationCaller == null) 
                throw new InvalidCastException(string.Format("Classes that use TransactionScope aspect should implement {0} interface.", typeof(IOperationCaller).FullName));

            return operationCaller.OperationCallContext.SessionContainer;
        }
    }
}
