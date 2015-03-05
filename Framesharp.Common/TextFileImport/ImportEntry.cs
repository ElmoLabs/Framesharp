using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Framesharp.Common.TextFileImport
{
    public class ImportEntry<TDomain> where TDomain : class
    {
        public TDomain DomainObject { get; set; }

        public IList<TextFileLineItem> LineList { get; set; }

        public ImportEntry(TDomain domainObject)
        {
            LineList = new List<TextFileLineItem>();

            DomainObject = domainObject;
        }
    }
}
