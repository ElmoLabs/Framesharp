using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using Framesharp.Common.Reflection;

namespace Framesharp.Common.TextFileImport
{
    public class DomainFileImporter
    {
        public IList<IPropertyMap> PropertyMapList { get; set; }

        public IList<TextFileLineItem> TextLines { get; set; }

        private IList<Tuple<object, IList<TextFileLineItem>>> TextLineMap { get; set; }

        private DomainFileImporter()
		{
			this.TextLineMap = new List<Tuple<object, IList<TextFileLineItem>>>();
		}

        public DomainFileImporter(Stream stream)
            : this()
        {
            TextLines = TextLines ?? new List<TextFileLineItem>();

            this.ReadTextFile(stream);

            PropertyMapList = new List<IPropertyMap>();
        }

        public DomainFileImporter(Stream stream, Encoding encoding) : this()
        {
            TextLines = TextLines ?? new List<TextFileLineItem>();

            this.ReadTextFile(stream, encoding);

            PropertyMapList = new List<IPropertyMap>();
        }

        private void ReadTextFile(Stream stream)
        {
            long lineNumber = 0;

            using (TextReader reader = new StreamReader(stream, true))
            {
                string textLine;

                while ((textLine = reader.ReadLine()) != null)
                {
                    TextLines.Add(new TextFileLineItem(lineNumber, textLine));

                    lineNumber++;
                }
            }
        }
        private void ReadTextFile(Stream stream, Encoding encoding)
        {
            long lineNumber = 0;

            using (TextReader reader = new StreamReader(stream, encoding))
            {
                string textLine;

                while ((textLine = reader.ReadLine()) != null)
                {
                    TextLines.Add(new TextFileLineItem(lineNumber, textLine));

                    lineNumber++;
                }
            }
        }

        private DomainFileImporter(IList<TextFileLineItem> textLines) : this()
        {
            TextLines = textLines;
        }

        public IList<TextFileLineItem> GetLinesByDomainObject(object domainObject)
		{
			foreach (Tuple<object, IList<TextFileLineItem>> current in this.TextLineMap)
			{
				if (current.Item1 == domainObject)
				{
					return current.Item2;
				}
			}
			return null;
		}

        private void AddLinesTuple(object domainObject, IList<TextFileLineItem> lines)
        {
            this.TextLineMap.Add(Tuple.Create<object, IList<TextFileLineItem>>(domainObject, lines));
        }
        
        public IList<TEntry> ReadFileToImportEntryList<TEntry>()
            where TEntry : class
        {
            return ReadFileToImportEntryList<TEntry>(TextLines);
        }

        public int GetLineLength<TEntry>() where TEntry : class
        {
            int num = 0;
            foreach (IPropertyMap current in this.PropertyMapList)
            {
                if (current is PositionalPropertyMap<TEntry>)
                {
                    PositionalPropertyMap<TEntry> positionalPropertyMap = current as PositionalPropertyMap<TEntry>;
                    
                    int num2 = positionalPropertyMap.StartIndex + positionalPropertyMap.Length;
                    num = ((num2 > num) ? num2 : num);
                }
                else
                {
                    DomainFileImporter domainFileImporter = new DomainFileImporter(this.TextLines);

                    MethodInfo method = base.GetType().GetMethod("GetLineLength");
                    domainFileImporter.PropertyMapList = (current as IListPropertyMap).Parameters;
                    Type type = (current.Expression as LambdaExpression).Body.Type.GetGenericArguments().First<System.Type>();
                    int num2 = (int)method.MakeGenericMethod(new Type[]
					{
						type
					}).Invoke(domainFileImporter, null);

                    num = ((num2 > num) ? num2 : num);
                }
            }
            return num;
        }

        protected IList<TEntry> ReadFileToImportEntryList<TEntry>(IList<TextFileLineItem> textLines) where TEntry : class
        {
            int lineLength = this.GetLineLength<TEntry>();
            
            IList<ImportEntry<TEntry>> importEntryList = new List<ImportEntry<TEntry>>();

            var keyPropertyMapList = PropertyMapList.Where(propertyMap =>
                                                           propertyMap is PositionalPropertyMap<TEntry> &&
                                                           (propertyMap as PositionalPropertyMap<TEntry>).IsKey).ToList();

            foreach (var textLine in textLines)
            {
                if (textLine.LineText.Length < lineLength)
                {
                    textLine.ErrorMessages = (textLine.ErrorMessages ?? new List<string>());
                    textLine.ErrorMessages.Add("Linha com tamanho diferente do especificado para o arquivo.");
                }
                else
                {       
                    TEntry entry = Activator.CreateInstance<TEntry>();

                    ImportEntry<TEntry> importEntry = null;

                    try
                    {
                        foreach (IPropertyMap propertyMap in keyPropertyMapList)
                        {
                            if (propertyMap is PositionalPropertyMap<TEntry>)
                            {
                                PositionalPropertyMap<TEntry> positionalPropertyMap =
                                    propertyMap as PositionalPropertyMap<TEntry>;

                                object keyValue =
                                    propertyMap.ValueFormatter.Format(
                                        textLine.LineText.Substring(positionalPropertyMap.StartIndex,
                                                                    positionalPropertyMap.Length));

                                entry = LambdaReflection.SetPropertyValue(entry, propertyMap.Expression, keyValue);
                            }
                        }
                    
                        foreach (ImportEntry<TEntry> item in importEntryList)
                        {
                            bool match = keyPropertyMapList.Count > 0;

                            foreach (var keyPropertyMap in keyPropertyMapList)
                            {
                                if (Convert.ToString(LambdaReflection.GetPropertyValue(item.DomainObject, keyPropertyMap.Expression)) != Convert.ToString(LambdaReflection.GetPropertyValue(entry, keyPropertyMap.Expression)))
                                {
                                    match = false;
                                    break;
                                }
                            }
                            if (match)
                            {
                                importEntry = item;
                                break;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        textLine.ErrorMessages = (textLine.ErrorMessages ?? new List<string>());
                        textLine.ErrorMessages.Add(ex.Message);
                        continue;
                    }
                    
                    IList<TextFileLineItem> list = null;
                    if (importEntry == null)
                    {
                        importEntry = new ImportEntry<TEntry>(entry);

                        importEntryList.Add(importEntry);

                        list = new List<TextFileLineItem>();
                        this.AddLinesTuple(entry, list);
                    }

                    importEntry.LineList.Add(textLine);

                    list = (list ?? this.GetLinesByDomainObject(importEntry.DomainObject));
                    list.Add(textLine);
                }
            }

            foreach (ImportEntry<TEntry> importEntry in importEntryList)
            {                
                foreach (IPropertyMap propertyMap in PropertyMapList)
                {
                    if (!(propertyMap is PositionalPropertyMap<TEntry>) ||
                        !(propertyMap as PositionalPropertyMap<TEntry>).IsKey)
                    {
                        if (propertyMap is PositionalPropertyMap<TEntry>)
                        {
                            PositionalPropertyMap<TEntry> positionalPropertyMap = propertyMap as PositionalPropertyMap<TEntry>;

                            foreach (var textLine in importEntry.LineList)
                            {
                                try
                                {
                                    object propertyValue = propertyMap.ValueFormatter.Format(textLine.LineText.Substring(positionalPropertyMap.StartIndex, positionalPropertyMap.Length));
                                    importEntry.DomainObject = LambdaReflection.SetPropertyValue<TEntry>(importEntry.DomainObject, propertyMap.Expression, propertyValue);
                                }
                                catch (Exception ex)
                                {
                                    textLine.ErrorMessages = (textLine.ErrorMessages ?? new List<string>());
                                    textLine.ErrorMessages.Add(ex.Message);
                                }
                            }
                        }
                        else
                        {
                            DomainFileImporter listPropertyImporter = new DomainFileImporter(importEntry.LineList);

                            MethodInfo methodInfo = listPropertyImporter.GetType().GetMethod("ReadFileToImportEntryList");

                            listPropertyImporter.PropertyMapList = (propertyMap as IListPropertyMap).Parameters;

                            var objType = (propertyMap.Expression as LambdaExpression).Body.Type.GetGenericArguments().First();

                            var propertyValue = methodInfo.MakeGenericMethod(objType).Invoke(listPropertyImporter, null);

                            importEntry.DomainObject = LambdaReflection.SetPropertyValue(importEntry.DomainObject, propertyMap.Expression, propertyValue);

                            foreach (var tuple in listPropertyImporter.TextLineMap)
                            {
                                this.AddLinesTuple(tuple.Item1, tuple.Item2);
                            }
                            
                        }
                    }                        
                }
                
            }

            return importEntryList.Select(item => item.DomainObject).ToList();
        }    
    }
}
