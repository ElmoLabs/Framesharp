using System;
using Framesharp.Configuration;
using Framesharp.Core.Interfaces;
using Framesharp.DependencyInjection;
using Framesharp.DependencyInjection.Registry;
using Framesharp.Test.Persistence;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framesharp.Test
{
    [TestClass]
    public abstract class TestClassBase : IOperationCaller, IDisposable
    {
        public IOperationCallContext OperationCallContext { get; private set; }

        protected ApplicationSettings ApplicationSettings { get; private set; }

        protected EquivalencePartitioning EquivalencePartition { get; private set; }

        protected TestClassBase(ApplicationSettings applicationSettings)
        {
            ApplicationSettings = applicationSettings;

            DependencyResolver.ConfigureApplication(applicationSettings);

            OperationCallContext = DependencyResolver.GetInstance<IOperationCallContext>();

            EquivalencePartition = DependencyResolver.GetInstance<EquivalencePartitioning>();
        }

        public void Dispose()
        {
            OperationCallContext.Dispose();
        }
    }
}
