using System;
using System.ServiceModel.Channels;

namespace Framesharp.ServiceModel.MessageHandling
{
    public  static class EndpointMessageResolver
    {
        /// <summary>
        /// Extracts the remote endpoint information message property
        /// </summary>
        /// <param name="message">WCF Service message</param>
        /// <returns></returns>
        public static RemoteEndpointMessageProperty ExtractRemoteEndpointMessageObject(Message message)
        {
            return ExtractRemoteEndpointMessagePropertyFromMessageProperties(message.Properties);
        }

        /// <summary>
        /// Extracts the remote endpoint information message property
        /// </summary>
        /// <param name="extended">WCF Service message properties</param>
        /// <returns></returns>
        private static RemoteEndpointMessageProperty ExtractRemoteEndpointMessagePropertyFromMessageProperties(MessageProperties extended)
        {
            object propertyObject;

            // Tries to retrieve the httpmessage object from the rough wcf message object.
            if (!extended.TryGetValue(RemoteEndpointMessageProperty.Name, out propertyObject))
                throw new Exception("Invalid request. Remote endpoint information not found.");

            return propertyObject as RemoteEndpointMessageProperty;
        }

        /// <summary>
        /// Extracts the remote endpoint information message property
        /// </summary>
        /// <param name="extended">WCF Service message properties</param>
        /// <returns></returns>
        public static RemoteEndpointMessageProperty ExtractRemoteEndpointMessageProperty(this MessageProperties extended)
        {
            return ExtractRemoteEndpointMessagePropertyFromMessageProperties(extended);
        }
    }
}