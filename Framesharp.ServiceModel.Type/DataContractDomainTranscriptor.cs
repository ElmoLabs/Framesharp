using Framesharp.Domain;
using Framesharp.ServiceModel.Interfaces;

namespace Framesharp.ServiceModel.Type
{
    public class DataContractDomainTranscriptor<TDomainObject, TDataContract> : IDataContractDomainTranscriptor<TDomainObject, TDataContract>
        where TDomainObject : IDomainObject, new()
        where TDataContract : IDataContract, new()
    {
        public virtual TDomainObject ConvertTo(TDomainObject model, TDataContract domain)
        {
            return default(TDomainObject);
        }

        public virtual TDataContract ConvertFrom(TDomainObject model, TDataContract domain)
        {
            return default(TDataContract);
        }
    }
}
