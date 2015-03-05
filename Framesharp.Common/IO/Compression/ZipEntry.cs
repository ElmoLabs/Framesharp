namespace Framesharp.Common.IO.Compression
{
    public class ZipEntry
    {
        /// <summary>
        /// Name of the file/folder inside the compressed package
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Entry represents a folder structure item
        /// </summary>
        public bool IsDirectory { get; set; }

        /// <summary>
        /// Byte array that represents the buffer for the entry content
        /// </summary>
        public byte[] Content { get; set; }
    }
}