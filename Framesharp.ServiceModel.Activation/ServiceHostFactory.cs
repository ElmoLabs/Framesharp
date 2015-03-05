using System;
using System.ServiceModel.Description;
using Framesharp.DependencyInjection.Registry;

namespace Framesharp.ServiceModel.Activation
{
    public class ServiceHostFactory : System.ServiceModel.Activation.ServiceHostFactory
    {
        protected override System.ServiceModel.ServiceHost CreateServiceHost(Type serviceType, Uri[] baseAddresses)
        {
            //All the custom factory does is return a new instance
            //of our custom host class. The bulk of the custom logic should
            //live in the custom host (as opposed to the factory) 
            //for maximum reuse value outside of the IIS/WAS hosting environment.
            var serviceHost = new ServiceHost(serviceType, baseAddresses);

            serviceHost.Open();

            return serviceHost;
        }
    }
}