using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framesharp.Common.Exceptions
{
    public class InvalidValueFormatterException : Exception
    {
        public InvalidValueFormatterException(string message) : base(message) { }

        public InvalidValueFormatterException() : base("Valor inválido") { }
    }
}
