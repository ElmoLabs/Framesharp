using System;
using Framesharp.Core.Interfaces;
using Framesharp.Persistence.Interfaces;

namespace Framesharp.DomainService.Interfaces
{
    public interface IDomainService : IOperationCaller, IPersistenceService, IDisposable
    {
    }
}
