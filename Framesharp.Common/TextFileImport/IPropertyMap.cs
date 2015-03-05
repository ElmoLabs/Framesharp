using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framesharp.Common.TextFileImport
{
    public interface IPropertyMap
    {
        Expression Expression { get; set; }

        IValueFormatter ValueFormatter { get; }
    }

    public interface IPropertyMap<TDomain> : IPropertyMap where TDomain : class
    {
    }
}
