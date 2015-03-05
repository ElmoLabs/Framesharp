using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framesharp.Common.TextFileImport
{
    public abstract class PropertyMap<TDomain> : IPropertyMap<TDomain> where TDomain : class
    {
        public Expression Expression { get; set; }

        public IValueFormatter ValueFormatter { get; protected set; }
    }
}
