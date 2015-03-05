using Framesharp.Core.Interfaces;
using Framesharp.ServiceModel.Interfaces;

namespace Framesharp.ServiceModel
{
    public class ServiceBase : IService, IOperationCaller
    {
        public IOperationCallContext OperationCallContext
        {
            get { return Core.OperationCallContext.Current; }
        }
    }
}
