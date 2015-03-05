using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Framesharp.ServiceModel
{
    public class ChannelFactoryFactory<T> : ChannelFactory<T>
    {
        public ChannelFactoryFactory()
            : base(typeof(T).ToString().ToLower())
        {
        }

        public ChannelFactoryFactory(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }
    }
}
