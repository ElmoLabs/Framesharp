using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Framesharp.Common.Exceptions;

namespace Framesharp.Common.TextFileImport
{
    public class DefaultFormatter : IValueFormatter
    {
        string ErrorMessage = string.Empty;

        public DefaultFormatter()
        {
        }

        public DefaultFormatter(string errorMessage)
            : this()
        {
            ErrorMessage = errorMessage;
        }

        public object Format(string value)
        {
            if (!string.IsNullOrEmpty(value))
                return value;

            if (string.IsNullOrEmpty(ErrorMessage))
                throw new InvalidValueFormatterException();
            else throw new InvalidValueFormatterException(ErrorMessage);
        }
    }
}
