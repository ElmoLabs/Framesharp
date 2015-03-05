namespace Framesharp.ServiceModel.Web.Type
{
    public class ModelDataContractTranscriptor<TModel, TDataContract> : IModelDataContractTranscriptor<TModel, TDataContract> 
        where TModel : class, Framesharp.Presentation.Web.Mvc.Models.IModel, new()
        where TDataContract : Framesharp.ServiceModel.Interfaces.IDataContract, new()
    {
        public virtual TModel ConvertTo(TModel model, TDataContract domain)
        {
            return default(TModel);
        }

        public virtual TDataContract ConvertFrom(TModel model, TDataContract domain)
        {
            return default(TDataContract);
        }
    }
}
