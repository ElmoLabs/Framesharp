using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ZipLib = ICSharpCode.SharpZipLib.Zip;

namespace Framesharp.Common.IO.Compression
{
    public static class ZipHelper
    {
        /// <summary>
        /// Extracts all files from within a compressed zip file.
        /// </summary>
        /// <param name="zipFilePath">Full path to the file to be read</param>
        /// <returns></returns>
        public static IList<ZipEntry> Extract(string zipFilePath)
        {
            // Checks the path to make sure the file exists
            if (!File.Exists(zipFilePath))
                throw new System.IO.FileNotFoundException("The file {0} does not exist.", zipFilePath);

            // Prepares the resulting list
            IList<ZipEntry> result;

            // Opens the file to open a reading stream to it
            using (FileStream stream = File.Open(zipFilePath, FileMode.Open))
            {
                result = Extract(stream);
            }

            return result;
        }

        /// <summary>
        /// Extracts all files from within a compressed zip file.
        /// </summary>
        /// <param name="fileContent">Arry of bytes representing file content</param>
        /// <returns></returns>
        public static IList<ZipEntry> Extract(byte[] fileContent)
        {
            // Prepares the resulting list
            IList<ZipEntry> result;

            // Opens the file to open a reading stream to it
            using (MemoryStream stream = new MemoryStream(fileContent))
            {
                result = Extract(stream);
            }

            return result;
        }

        /// <summary>
        /// Extracts all files from within a compressed zip file.
        /// </summary>
        /// <param name="stream">Stream object containing file content bytes</param>
        /// <returns></returns>
        public static IList<ZipEntry> Extract(Stream stream)
        {
            // Prepares the resulting list
            IList<ZipEntry> result = new List<ZipEntry>();

            // Creates a stream reader to get the file content and convert it to a list of ZipEntries
            using (ZipLib.ZipInputStream zipReader = new ZipLib.ZipInputStream(stream))
            {
                // Reads the next file entry
                ZipLib.ZipEntry entry = zipReader.GetNextEntry();

                while (entry != null)
                {
                    Compression.ZipEntry extractedFile = new Compression.ZipEntry
                        {
                            // Full file name and folder structure
                            Name = entry.Name,

                            // Entry represents a folder structure (yes/no)
                            IsDirectory = entry.IsDirectory
                        };

                    #region Workaround for ICSharpCode.SharpZipLib.Zip null file size for larger files

                    // For incremental buffer reading purposes
                    IList<byte> content = new List<byte>();

                    byte[] readByte = new byte[1];

                    // Reads out each byte for this entry into a buffer
                    while (zipReader.Read(readByte, 0, 1) > 0)
                    {
                        content.Add(readByte[0]);
                    }

                    // Converts a list of bytes into a valid file buffer associated to the zip entry
                    extractedFile.Content = content.ToArray();

                    #endregion

                    // adds the resulting object to the resulting list
                    result.Add(extractedFile);

                    // Move on to the next file entry in case there's any
                    entry = zipReader.GetNextEntry();
                }

                zipReader.Close();
            }

            return result;
        }
    }
}