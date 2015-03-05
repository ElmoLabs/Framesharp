using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;

[assembly: WebResource("Framesharp.Presentation.Web.Mvc.Resources.Javascript.utilities.js", "application/x-javascript")]
[assembly: WebResource("Framesharp.Presentation.Web.Mvc.Resources.Javascript.notifier.js", "application/x-javascript")]
[assembly: WebResource("Framesharp.Presentation.Web.Mvc.Resources.Javascript.mvc.extensions.js", "application/x-javascript")]
[assembly: WebResource("Framesharp.Presentation.Web.Mvc.Resources.Javascript.datagrid.js", "application/x-javascript")]
[assembly: WebResource("Framesharp.Presentation.Web.Mvc.Resources.CSS.datagrid.css", "text/css")]

namespace Framesharp.Presentation.Web.Mvc
{
    public static class ResourceManager
    {
        public static HtmlString IncludeCSSResources()
        {
            List<string> cssFileList = new List<string>();

            cssFileList.Add(GetFormattedCSSIncludeTag("Framesharp.Presentation.Web.Mvc.Resources.CSS.datagrid.css"));

            return new HtmlString(string.Join(Environment.NewLine, cssFileList));
        }

        public static HtmlString IncludeJavascriptResources()
        {
            List<string> javascriptFileList = new List<string>();

            javascriptFileList.Add(GetFormattedJavascriptIncludeTag("Framesharp.Presentation.Web.Mvc.Resources.Javascript.utilities.js"));
            javascriptFileList.Add(GetFormattedJavascriptIncludeTag("Framesharp.Presentation.Web.Mvc.Resources.Javascript.notifier.js"));
            javascriptFileList.Add(GetFormattedJavascriptIncludeTag("Framesharp.Presentation.Web.Mvc.Resources.Javascript.mvc.extensions.js"));
            javascriptFileList.Add(GetFormattedJavascriptIncludeTag("Framesharp.Presentation.Web.Mvc.Resources.Javascript.datagrid.js"));

            return new HtmlString(string.Join(Environment.NewLine, javascriptFileList));
        }

        private static string GetFormattedCSSIncludeTag(string cssFilePath)
        {
            Page page = new Page();
            
            string cssFileUrl = page.ClientScript.GetWebResourceUrl(typeof(ResourceManager), cssFilePath);

            return string.Format("<link href='{0}' rel='stylesheet' type='text/css' />", cssFileUrl);
        }

        private static string GetFormattedJavascriptIncludeTag(string javascriptFilePath)
        {
            Page page = new Page();
            
            string javascriptFileUrl = page.ClientScript.GetWebResourceUrl(typeof(ResourceManager), javascriptFilePath);

            return string.Format("<script src='{0}' type='text/javascript'></script>", javascriptFileUrl);
        }
    }
}
