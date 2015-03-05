using System;

namespace Framesharp.Common.TypeConversion
{
    public static class ValueTypeConverter
    {
        public static object Convert(this Object extended, Type type)
        {
            if (extended == null) return null;

            return System.Convert.ChangeType(extended, type);
        }

        public static T Convert<T>(this Object extended)
        {
            if (extended == null) return default(T);

            return (T)System.Convert.ChangeType(extended, typeof (T));
        }
    }
}