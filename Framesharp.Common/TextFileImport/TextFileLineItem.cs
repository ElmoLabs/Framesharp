using System.Collections.Generic;

namespace Framesharp.Common.TextFileImport
{
    public class TextFileLineItem
    {
        public long LineNumber { get; set; }

        public string LineText { get; set; }

        public IList<string> ErrorMessages { get; set; }

        public TextFileLineItem(long lineNumber, string lineText)
        {
            LineNumber = lineNumber; 
            LineText = lineText; 
        }
    }
}