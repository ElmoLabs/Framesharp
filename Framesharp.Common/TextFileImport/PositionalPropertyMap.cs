using System;
using System.Globalization;
using System.Linq.Expressions;

namespace Framesharp.Common.TextFileImport
{
    public class PositionalPropertyMap<TDomain> : PropertyMap<TDomain> where TDomain : class
    {
        public int StartIndex { get; set; }

        public int Length { get; set; }

        public bool IsKey { get; private set; }

        public PositionalPropertyMap(Expression<Func<TDomain, object>> expression, int startIndex, int length, bool isKey)
            : this(expression, startIndex, length, isKey, new DefaultFormatter())
        {
        }

        public PositionalPropertyMap(Expression<Func<TDomain, object>> expression, int startIndex, int length, bool isKey, string errorMessage)
            : this(expression, startIndex, length, isKey, new DefaultFormatter(errorMessage))
        {
        }

        public PositionalPropertyMap(Expression<Func<TDomain, object>> expression, int startIndex, int length, bool isKey, IValueFormatter valueFormatter)
        {
            Expression = expression;
            StartIndex = startIndex;
            Length = length;
            IsKey = isKey;

            ValueFormatter = valueFormatter ?? new DefaultFormatter();
        }
    }
}