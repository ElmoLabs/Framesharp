namespace Framesharp.Common.TextFileImport
{
    public abstract class ListPropertyMap<TDomain> : PropertyMap<TDomain>, IListPropertyMap
        where TDomain : class
    {
        public IPropertyMap[] Parameters { get; set; }
    }
}