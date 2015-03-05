using System;
using System.Linq.Expressions;

namespace Framesharp.Common.TextFileImport
{
    public class PositionalListPropertyMap<TDomain> : ListPropertyMap<TDomain>
        where TDomain : class
    {
        public PositionalListPropertyMap(Expression<Func<TDomain, object>> expression, params IPropertyMap[] parameters)
        {
            Expression = expression;
            Parameters = parameters;
        }
    }
}