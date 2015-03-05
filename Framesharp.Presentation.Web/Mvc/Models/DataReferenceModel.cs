using System.Collections.Generic;
using Framesharp.DependencyInjection;
using Framesharp.Domain;
using Framesharp.DomainService.Interfaces;

namespace Framesharp.Presentation.Web.Mvc.Models
{
    public abstract class DataReferenceModel<T> : ModelBase where T : class, IDomainObject
    {
        public static IList<T> GetList()
        {
            IList<T> result;

            using (ICrudStatelessDomainService<T> domainService = DependencyResolver.GetInstance<ICrudStatelessDomainService<T>>())
            {
                result = domainService.ListAll();
            }

            return result;
        }
    }
}
