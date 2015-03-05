using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framesharp.Common.TextFileImport
{
    public interface IListPropertyMap : IPropertyMap
    {
        IPropertyMap[] Parameters { get; set; }
    }
}
