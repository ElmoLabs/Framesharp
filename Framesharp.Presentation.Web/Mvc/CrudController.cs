using System.Web.Mvc;
using Framesharp.DomainService;
using Framesharp.DomainService.Interfaces;
using Framesharp.Persistence.Interfaces;

namespace Framesharp.Presentation.Web.Mvc
{
    public class CrudController<TDomainService> : BaseController where TDomainService : IDomainService
    {
        protected TDomainService DomainService { get; private set; }

        public CrudController()
        {
            DomainService = DomainServiceFactory.Get<TDomainService>(OperationCallContext);
        }

        protected override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            if (OperationCallContext != null)
                OperationCallContext.Dispose();

            base.OnResultExecuted(filterContext);
        }
    }
}