using System.Collections.Generic;

namespace Framesharp.Configuration
{
    public class ApplicationSettings
    {
        public virtual IList<StructureMap.Registry> DependencyRegistries { get; set; }
    }
}
