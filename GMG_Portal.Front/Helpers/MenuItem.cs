using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Front.Helpers
{
    public static class MyHTMLHelpers
    {
        public static MvcHtmlString MenuItem(this HtmlHelper htmlHelper, string action, string text)
        {
            var menu = new TagBuilder("div");
            var currentAction = (string) htmlHelper.ViewContext.RouteData.Values["action"];
            if (string.Equals(
                currentAction,
                action,
                StringComparison.CurrentCultureIgnoreCase)
            )
            {
                menu.AddCssClass("highlight");
            }
            menu.SetInnerText(text);
            return MvcHtmlString.Create(menu.ToString());
        }
        public static MvcHtmlString ListItemAction(this HtmlHelper helper, string name, string actionName, string controllerName)
        {
            var currentControllerName = (string)helper.ViewContext.RouteData.Values["controller"];
            var currentActionName = (string)helper.ViewContext.RouteData.Values["action"];
            var sb = new StringBuilder();
            sb.AppendFormat("<li{0}", (currentControllerName.Equals(controllerName, StringComparison.CurrentCultureIgnoreCase) &&
                                       currentActionName.Equals(actionName, StringComparison.CurrentCultureIgnoreCase)
                ? " class=\"active\">" : ">"));
            var url = new UrlHelper(HttpContext.Current.Request.RequestContext);
            sb.AppendFormat("<a href=\"{0}\">{1}</a>", url.Action(actionName, controllerName), name);
            sb.Append("</li>");
            return new MvcHtmlString(sb.ToString());
        }
        public static IHtmlString PreserveNewLines(this HtmlHelper htmlHelper, string message)
        {
            return message == null ? null : htmlHelper.Raw(htmlHelper.Encode(message).Replace("\n", "<br/>"));
        }
    }

}