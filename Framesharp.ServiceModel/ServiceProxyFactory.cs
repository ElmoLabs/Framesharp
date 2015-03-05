namespace Framesharp.ServiceModel
{
    public class ServiceProxyFactory
    {
        public static T Get<T>()
        {
            return Get<T>(null);
        }

        public static T Get<T>(string endpointConfigurationName)
        {
            return new ClientFactory<T>(endpointConfigurationName).GetProxy();
        }
    }
}