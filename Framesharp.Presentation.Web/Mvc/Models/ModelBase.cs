using System;

namespace Framesharp.Presentation.Web.Mvc.Models
{
    public abstract class ModelBase : IModel
    {
        protected ModelBase()
        {
            ModelIdentity = Guid.NewGuid();
        }

        public Guid ModelIdentity { get; set; }
    }
}
