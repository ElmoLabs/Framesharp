using System.Collections;
using System.Collections.Generic;

namespace Framesharp.Configuration
{
    public class ApplicationSettings
    {
        public virtual IList<StructureMap.Configuration.DSL.Registry> DependencyRegistries { get; set; }
    }
}
