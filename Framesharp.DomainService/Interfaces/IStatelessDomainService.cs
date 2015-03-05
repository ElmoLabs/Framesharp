using System;
using Framesharp.Core.Interfaces;

namespace Framesharp.DomainService.Interfaces
{
    public interface IStatelessDomainService : IStatelessOperationCaller, IDisposable
    {
    }
}
