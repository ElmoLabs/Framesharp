using System;
using System.Collections.Generic;
using System.Configuration;
using Framesharp.Configuration;
using StructureMap;
using StructureMap.Pipeline;
using StructureMap.Web.Pipeline;

namespace Framesharp.DependencyInjection
{
    public static class DependencyResolver
    {
        private static Container _container;

        private static Container Container {
            get
            {
                if (_container == null)
                    throw new ConfigurationErrorsException("The structure map container is not set. Check if DependencyResolver.ConfigureApplication is being called in your application startup.");
                
                return _container;
            }
            set
            {
                _container = value;
            }
        }

        public static void ConfigureApplication(ApplicationSettings applicationSettings)
        {
            Container = new Container(c =>
            {
                foreach (var registry in applicationSettings.DependencyRegistries)
                {
                    c.AddRegistry(registry);
                }
            });
        }

        public static T GetInstance<T>()
        {
            return Container.GetInstance<T>();
        }

        public static T GetInstance<T>(string key, object value)
        {
            return GetInstance<T>(new Dictionary<string, object>() {{key, value}});
        }

        public static T GetInstance<T>(IDictionary<string, object> parameterValueCollection)
        {
            ExplicitArguments args = new ExplicitArguments(parameterValueCollection);

            return Container.GetInstance<T>(args);
        }

        public static void ReleaseAndDisposeAllHttpScopedObjects()
        {
            HttpContextLifecycle.DisposeAndClearAll();
        }
    }
}
