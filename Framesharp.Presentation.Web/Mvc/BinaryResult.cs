using System.Web.Mvc;

namespace Framesharp.Presentation.Web.Mvc
{
    public class BinaryResult : ActionResult
    {
        private readonly string _fileName;

        private readonly string _contentType;

        private readonly byte[] _fileBuffer;

        public BinaryResult(string fileName, string contentType, byte[] fileBuffer)
        {
            _fileName = fileName;
            _contentType = contentType;
            _fileBuffer = fileBuffer;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();

            context.HttpContext.Response.ContentType = _contentType;

            context.HttpContext.Response.AddHeader("Content-Disposition", "filename=" + _fileName);

            if (_fileBuffer != null)
            {
                context.HttpContext.Response.BinaryWrite(_fileBuffer);
            }
        }
    }
}