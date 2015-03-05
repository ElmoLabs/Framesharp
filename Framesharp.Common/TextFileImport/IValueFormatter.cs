namespace Framesharp.Common.TextFileImport
{
    public interface IValueFormatter
    {
        object Format(string value);
    }
}