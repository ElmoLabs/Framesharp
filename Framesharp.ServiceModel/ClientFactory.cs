using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Channels;
using Framesharp.DependencyInjection;

namespace Framesharp.ServiceModel
{
    public class ClientFactory<T> : IDisposable
    {
        private readonly IChannelFactory<T> _channelFactory;

        public ClientFactory() : this(null)
        {
        }

        public ClientFactory(string endpointConfigurationName)
        {
            endpointConfigurationName = endpointConfigurationName ?? typeof (T).Name.ToLower();

            _channelFactory = DependencyResolver.GetInstance<IChannelFactory<T>>(new Dictionary<string, object>{ {"endpointConfigurationName", endpointConfigurationName} }); 
            // ObjectFactory.With("endpointConfigurationName").EqualTo(endpointConfigurationName).GetInstance<<T>>();
        }

        public ClientFactory(Binding binding, EndpointAddress remoteAddress)
        {
            _channelFactory = DependencyResolver.GetInstance<IChannelFactory<T>>(new Dictionary<string, object> { { "binding", binding }, { "remoteAddress", remoteAddress } }); 
            // _channelFactory = ObjectFactory.With("binding").EqualTo(binding).With("remoteAddress").EqualTo(remoteAddress).GetInstance<IChannelFactory<T>>();
        }

        public T GetProxy()
        {
            Open();

            if (!(_channelFactory is ChannelFactory<T>)) throw new Exception("Channel factory initialization failed."); 

            return (_channelFactory as ChannelFactory<T>).CreateChannel();
        }

        internal void Open()
        {
            if (_channelFactory.State != CommunicationState.Opened)
            {
                _channelFactory.Open();
            }
        }

        public void Dispose()
        {
            //_channelFactory.Close();
        }
    }
}