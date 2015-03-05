using Framesharp.Configuration;
using Framesharp.Core.Interfaces;
using Framesharp.Presentation.Web.Mvc;
using DI = Framesharp.DependencyInjection;

namespace Framesharp.Test.Web
{
    public abstract class CrudControllerTestClass<T> : TestClassBase where T : BaseController, IOperationCaller
    {
        protected T Controller { get; private set; }

        protected CrudControllerTestClass(ApplicationSettings applicationSettings) : base(applicationSettings)
        {
            Controller = DI.DependencyResolver.GetInstance<T>();
        }
    }
}
