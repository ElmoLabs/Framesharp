using System;
using System.ServiceModel.Channels;

namespace Framesharp.ServiceModel.MessageHandling
{
    public static class HttpMessageResolver
    {
        /// <summary>
        /// Extracts the http request object out of the service message
        /// </summary>
        /// <param name="message">WCF Service message</param>
        /// <returns></returns>
        public static HttpRequestMessageProperty ExtractHttpRequestObject(Message message)
        {
            return ExtractHttpRequestMessagePropertyFromMessageProperties(message.Properties);
        }

        /// <summary>
        /// Extracts the http request object out of the service message
        /// </summary>
        /// <param name="extended">WCF Service message properties</param>
        /// <returns></returns>
        private static HttpRequestMessageProperty ExtractHttpRequestMessagePropertyFromMessageProperties(MessageProperties extended)
        {
            object httpMessageObject;

            // Tries to retrieve the httpmessage object from the rough wcf message object.
            if (!extended.TryGetValue(HttpRequestMessageProperty.Name, out httpMessageObject))
                throw new Exception("Invalid request. Http headers not found.");

            return httpMessageObject as HttpRequestMessageProperty;
        }

        /// <summary>
        /// Extracts the http request object out of the service message
        /// </summary>
        /// <param name="extended">WCF Service message properties</param>
        /// <returns></returns>
        public static HttpRequestMessageProperty ExtractHttpRequestMessageProperty(this MessageProperties extended)
        {
            return ExtractHttpRequestMessagePropertyFromMessageProperties(extended);
        }
    }
}