using System.Web.Mvc;
using Framesharp.Presentation.Web.Mvc;
using Framesharp.ServiceModel.Interfaces;

namespace Framesharp.ServiceModel.Web.Mvc
{
    public class CrudController<TService> : BaseController where TService : IService
    {
        protected TService Service { get; private set; }

        public CrudController()
        {
            Service = ServiceProxyFactory.Get<TService>();
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (OperationCallContext != null)
                OperationCallContext.Dispose();

            base.OnResultExecuted(filterContext);
        }
    }
}