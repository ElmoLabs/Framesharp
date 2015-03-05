using System.ServiceModel.Channels;
using Framesharp.Configuration;
using Framesharp.ServiceModel;
using System.ServiceModel;

namespace Framesharp.DependencyInjection.Registry
{
    public class ServiceModelRegistry : StructureMap.Configuration.DSL.Registry
    {
        public ServiceModelRegistry()
        {
            For(typeof(IChannelFactory<>)).Singleton().Use(typeof(ChannelFactoryFactory<>));
        }
    }
}
