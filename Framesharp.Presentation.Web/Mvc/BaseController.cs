using System.Web;
using System.Web.Mvc;
using Framesharp.Core.Interfaces;

namespace Framesharp.Presentation.Web.Mvc
{
    public class BaseController : Controller, IOperationCaller
    {
        public IOperationCallContext OperationCallContext
        {
            get { return Framesharp.Core.OperationCallContext.Current; }
        }

        public void SetOperationResult(OperationStatus status, string message)
        {
            Response.Headers["OperationResultMessage"] = HttpUtility.HtmlEncode(message);
            Response.Headers["OperationResultStatus"] = status.ToString();
        }
    }
}