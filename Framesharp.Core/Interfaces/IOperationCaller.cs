using Framesharp.Core;

namespace Framesharp.Core.Interfaces
{
    public interface IOperationCaller
    {
        /// <summary>
        /// NHibernate session instance
        /// </summary>
        IOperationCallContext OperationCallContext { get; }
    }
}