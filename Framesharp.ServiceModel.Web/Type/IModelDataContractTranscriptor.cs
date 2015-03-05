using Framesharp.Presentation.Web.Mvc.Models;
using Framesharp.ServiceModel.Interfaces;

namespace Framesharp.ServiceModel.Web.Type
{
    public interface IModelDataContractTranscriptor<TModel, TDataContract> 
        where TModel : IModel, new() 
        where TDataContract : IDataContract, new()
    {
        TModel ConvertTo(TModel model, TDataContract domain);
        TDataContract ConvertFrom(TModel model, TDataContract domain);
    }
}