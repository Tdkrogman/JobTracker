using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace JobTracker.TagHelpers
{
    [HtmlTargetElement("a")]
    public class ActiveNavLinkTagHelper : TagHelper
    {
        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewCtx { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            var css = context.AllAttributes["class"]?.Value?.ToString() ?? "";
            if (css.Contains("nav-link"))
            {
                string area = ViewCtx.RouteData.Values["area"]?.ToString() ?? "";
                string ctlr = ViewCtx.RouteData.Values["controller"]?.ToString();
                string action = ViewCtx.RouteData.Values["action"]?.ToString() ?? "";

                string aspArea = context.AllAttributes["asp-area"]?.Value?.ToString() ?? "";
                string aspCtlr = context.AllAttributes["asp-controller"]?.Value?.ToString();
                string aspAction = context.AllAttributes["asp-action"]?.Value?.ToString();

                if (area == aspArea && ctlr == aspCtlr && action == aspAction)
                    output.Attributes.AppendCssClass("active");
            }
        }
    }
}
