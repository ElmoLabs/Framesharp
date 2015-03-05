using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Framesharp.Common.TextFileImport
{
    public class DateValueFormatter : IValueFormatter
    {
        public object Format(string value)
        {
            return DateTime.ParseExact(value, "ddMMyyyy", CultureInfo.InvariantCulture);
        }
    }
}
