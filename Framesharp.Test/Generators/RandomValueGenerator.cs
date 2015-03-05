using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using Framesharp.Common.TypeConversion;

namespace Framesharp.Test.Generators
{
    public enum DateGenerationDirection
    {
        Past,
        Future
    }

    public class RandomValueGenerator
    {
        public static T GenerateRandomValue<T>()
        {
            if (typeof (T).IsEnum)
            {
                return (T) Enum.GetValues(typeof(T)).GetValue(0);
            }
            if (typeof(T) == typeof(string))
            {
                return "A".Convert<T>();
            }
            if (typeof(T) == typeof(int) || 
                typeof(T) == typeof(long) || 
                typeof(T) == typeof(decimal) ||
                typeof(T) == typeof(double) ||
                typeof(T) == typeof(float))
            {
                return 1.Convert<T>();
            }
            if (typeof(T) == typeof(int))
            {
                return 1.Convert<T>();
            }
            if (typeof(T) == typeof(bool))
            {
                return true.Convert<T>();
            }
            if (typeof(T) == typeof(DateTime))
            {
                return DateTime.Now.Convert<T>();
            }

            return default(T);
        }

        public static string GenerateRandomString(int size)
        {
            StringBuilder stringBuilder = new StringBuilder();

            Random rnd = new Random((int) DateTime.Now.Ticks);

            for (int i = 0; i < size; i++)
            {
                stringBuilder.Append(Convert.ToChar(Convert.ToInt32(Math.Floor(26 * rnd.NextDouble() + 65))));
            }

            return stringBuilder.ToString();
        }

        public static int GenerateRandomNumber(int digits = 1)
        {
            int number = new Random((int) DateTime.Now.Ticks).Next(int.MaxValue);

            string numberString = number.ToString(CultureInfo.InvariantCulture);

            if (digits > numberString.Length)
                digits = numberString.Length;

            return Convert.ToInt32(numberString.Substring(0, digits - 1));
        }

        public static DateTime GenerateRandomDate(DateGenerationDirection dateGenerationDirection)
        {
            Random rnd = new Random((int) DateTime.Now.Ticks);

            DateTime date = new DateTime();

            switch (dateGenerationDirection)
            {
                case DateGenerationDirection.Past:
                    date = new DateTime(rnd.Next(2000, DateTime.Now.Year - 1), rnd.Next(1, 12), rnd.Next(1, 30)) ;
                    break;
                case DateGenerationDirection.Future:
                    date = new DateTime(rnd.Next(DateTime.Now.Year, DateTime.Now.AddYears(10).Year), rnd.Next(1, 12), rnd.Next(1, 30));
                    break;
            }

            return date;
        }
    }
}
