using System.Collections.Generic;
using Framesharp.Domain;
using Framesharp.DomainService;
using Framesharp.DependencyInjection;
using Framesharp.DomainService.Interfaces;

namespace Framesharp.ServiceModel.Web.Mvc.Models
{
    public abstract class DataReferenceModel<T> : ModelBase where T : class, IDomainObject
    {
        public static IList<T> GetList()
        {
            IList<T> result = null;

            using (ICrudStatelessDomainService<T> domainService = DependencyResolver.GetInstance<ICrudStatelessDomainService<T>>())
            {
                result = domainService.ListAll();
            }

            return result;
        }
    }
}
