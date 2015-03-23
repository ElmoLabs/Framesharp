using System;
using Framesharp.Core.Interfaces;
using Framesharp.Data.Interfaces;

namespace Framesharp.DomainService.Interfaces
{
    public interface IDomainService : IOperationCaller, IDataService, IDisposable
    {
    }
}
