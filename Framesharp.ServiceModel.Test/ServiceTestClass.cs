using System;
using System.Collections.Generic;
using Framesharp.Configuration;
using Framesharp.DependencyInjection;
using Framesharp.DependencyInjection.Registry;

namespace Framesharp.ServiceModel.Test
{
    public abstract class ServiceTestClass : IDisposable
    {
        protected ServiceTestClass(ApplicationSettings applicationSettings) : this(null, applicationSettings)
        {
        }

        protected ServiceTestClass(Action testInitialization, ApplicationSettings applicationSettings)
        {
            DependencyResolver.ConfigureApplication(applicationSettings);

            if (testInitialization != null) testInitialization.Invoke();
        }

        public abstract void Dispose();
    }
}
