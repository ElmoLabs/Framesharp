namespace Framesharp.Presentation.Web.Type
{
    public class ModelDomainTranscriptor<TModel, TDomain> : IModelDomainTranscriptor<TModel, TDomain> 
        where TModel : class, Framesharp.Presentation.Web.Mvc.Models.IModel, new()
        where TDomain : Framesharp.Domain.IDomainObject, new()
    {
        public virtual TModel ConvertTo(TModel model, TDomain domain)
        {
            return default(TModel);
        }

        public virtual TDomain ConvertFrom(TModel model, TDomain domain)
        {
            return default(TDomain);
        }
    }
}
