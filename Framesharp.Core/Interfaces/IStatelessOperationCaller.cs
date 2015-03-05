namespace Framesharp.Core.Interfaces
{
    public interface IStatelessOperationCaller
    {
        /// <summary>
        /// NHibernate stateless session instance
        /// </summary>
        IStatelessOperationCallContext StatelessOperationCallContext { get; }
    }
}