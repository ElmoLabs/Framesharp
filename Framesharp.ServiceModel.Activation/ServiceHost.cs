using System;
using System.ServiceModel.Description;
using Framesharp.DependencyInjection.Registry;

namespace Framesharp.ServiceModel.Activation
{
    public class ServiceHost : System.ServiceModel.ServiceHost
    {
        public ServiceHost(Type serviceType, params Uri[] baseAddresses) :
            base(serviceType, baseAddresses)
        {
        }

        protected override void ApplyConfiguration()
        {
            base.ApplyConfiguration();

            ServiceMetadataBehavior mexBehavior = this.Description.Behaviors.Find<ServiceMetadataBehavior>();

            if (mexBehavior != null) return;

            //Metadata behavior has already been configured, 
            //so we do not have any work to do.
            mexBehavior = new ServiceMetadataBehavior();

            Description.Behaviors.Add(mexBehavior);

            //Add a metadata endpoint at each base address
            //using the "/mex" addressing convention
            foreach (Uri baseAddress in this.BaseAddresses)
            {
                if (baseAddress.Scheme == Uri.UriSchemeHttp)
                {
                    mexBehavior.HttpGetEnabled = true;
                    this.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                            MetadataExchangeBindings.CreateMexHttpBinding(),
                                            "mex");
                }
                else if (baseAddress.Scheme == Uri.UriSchemeHttps)
                {
                    mexBehavior.HttpsGetEnabled = true;
                    this.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                            MetadataExchangeBindings.CreateMexHttpsBinding(),
                                            "mex");
                }
                else if (baseAddress.Scheme == Uri.UriSchemeNetPipe)
                {
                    this.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                            MetadataExchangeBindings.CreateMexNamedPipeBinding(),
                                            "mex");
                }
                else if (baseAddress.Scheme == Uri.UriSchemeNetTcp)
                {
                    this.AddServiceEndpoint(ServiceMetadataBehavior.MexContractName,
                                            MetadataExchangeBindings.CreateMexTcpBinding(),
                                            "mex");
                }
            }
        }
    }
}