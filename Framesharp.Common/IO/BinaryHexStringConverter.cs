using System;
using System.IO;

namespace Framesharp.Common.IO
{
    public class BinaryHexStringConverter
    {
        public static string Convert(string filePath)
        {
            using (
                var stream =
                    new FileStream(filePath, FileMode.Open))
            {
                return Convert(stream);
            }
        }

        public static string Convert(Stream stream)
        {
            using (var reader = new BinaryReader(stream))
            {
                byte[] buffer = reader.ReadBytes((int)stream.Length);

                return BitConverter.ToString(buffer).Replace("-", string.Empty);
            }
        }
    }
}