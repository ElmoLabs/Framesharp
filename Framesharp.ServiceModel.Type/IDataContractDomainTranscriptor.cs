using Framesharp.Domain;
using Framesharp.ServiceModel.Interfaces;

namespace Framesharp.ServiceModel.Type
{
    public interface IDataContractDomainTranscriptor<TDomainObject, TDataContract> 
        where TDomainObject : IDomainObject, new() 
        where TDataContract : IDataContract, new()
    {
        TDomainObject ConvertTo(TDomainObject domain, TDataContract dataContract);
        TDataContract ConvertFrom(TDomainObject domain, TDataContract dataContract);
    }
}