using Framesharp.Domain;
using Framesharp.Presentation.Web.Mvc.Models;

namespace Framesharp.Presentation.Web.Type
{
    public interface IModelDomainTranscriptor<TModel, TDomain> where TModel : IModel, new() where TDomain : IDomainObject, new()
    {
        TModel ConvertTo(TModel model, TDomain domain);
        TDomain ConvertFrom(TModel model, TDomain domain);
    }
}