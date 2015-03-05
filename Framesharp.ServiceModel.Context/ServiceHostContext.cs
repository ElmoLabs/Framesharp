using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;
using Framesharp.Configuration;
using Framesharp.DependencyInjection;
using Framesharp.DependencyInjection.Registry;
using Framesharp.ServiceModel.Interfaces;

namespace Framesharp.ServiceModel.Context
{
    public class ServiceHostContext
    {
        public static ServiceHostContext Current { get; private set; }
        private static Queue<Action> _initializationProcess;

        private readonly IList<ServiceHost> _serviceHosts;

        private ServiceHostContext()
        {
            _serviceHosts = new List<ServiceHost>();
        }

        public static void QueueInitializationAction(Action action)
        {
            _initializationProcess = _initializationProcess ?? new Queue<Action>();

            _initializationProcess.Enqueue(action);
        }

        public static void Initialize(ApplicationSettings applicationSettings)
        {
            DependencyResolver.ConfigureApplication(applicationSettings);

            if (_initializationProcess != null)
                while (_initializationProcess.Count > 0)
                {
                    Action action = _initializationProcess.Dequeue();
                    
                    action.Invoke();
                }

            Current = new ServiceHostContext();
        }

        public ServiceHost Open<T>() where T : class, IService
        {
            var serviceHost = new ServiceHost(typeof (T));

            _serviceHosts.Add(serviceHost);

            string serviceTypeName = typeof (T).Name;

            Console.WriteLine("Opening host for service {0}...", serviceTypeName);
            serviceHost.Open();
            Console.WriteLine("{0} host open.", serviceTypeName);

            return serviceHost;
        }
    }
}