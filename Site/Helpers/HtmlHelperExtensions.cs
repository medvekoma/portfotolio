using System;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Services.Models;

namespace Portfotolio.Site.Services.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString RadioButtonForEnum<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression
        )
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var names = Enum.GetNames(metaData.ModelType);
            var sb = new StringBuilder();
            foreach (var name in names)
            {
                var id = String.Format(
                    "{0}_{1}_{2}",
                    htmlHelper.ViewData.TemplateInfo.HtmlFieldPrefix,
                    metaData.PropertyName,
                    name
                );

                var radio = htmlHelper.RadioButtonFor(expression, name, new { id = id }).ToHtmlString();
                sb.AppendFormat(
                    "{2} <label for=\"{0}\">{1}</label>",
                    id,
                    HttpUtility.HtmlEncode(name),
                    radio
                );
            }
            return MvcHtmlString.Create(sb.ToString());
        }

        public static string UserIdentifier(this HtmlHelper htmlHelper)
        {
            var session = htmlHelper.ViewContext.HttpContext.Session;
            if (session == null)
                return null;
            return session[DataKeys.UserIdentifier] as string;
        }

        public static string ActionUserId(this UrlHelper urlHelper, string actionName, object values)
        {
            string result = urlHelper.Action(actionName, values);
            return result.Replace("%40", "@");
        }

        public static MvcHtmlString ActionLinkUserId(this HtmlHelper htmlHelper, string linkText, string actionName, object values)
        {
            var originalLink = htmlHelper.ActionLink(linkText, actionName, values).ToString();
            var changedLink = originalLink.Replace("%40", "@");
            return new MvcHtmlString(changedLink);
        }

        public static MvcHtmlString ActionLinkUserId(this HtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object values, object htmlAttributes)
        {
            var originalLink = htmlHelper.ActionLink(linkText, actionName, controllerName, values, htmlAttributes).ToString();
            var changedLink = originalLink.Replace("%40", "@");
            return new MvcHtmlString(changedLink);
        }

        public static MvcHtmlString LabeledRadioButtonFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, TProperty value, string label)
        {
            string encodedLabel = htmlHelper.Encode(label);
            MvcHtmlString radioButtonFor = htmlHelper.RadioButtonFor(expression, value);
            var html = string.Format("<label>{0}{1}</label>", radioButtonFor, encodedLabel);
            return new MvcHtmlString(html);
        }

        public static MvcHtmlString MetaRobots(this HtmlHelper htmlHelper)
        {
            object allowRobotsObject = htmlHelper.ViewData[DataKeys.AllowRobots];
            if (allowRobotsObject == null || !(allowRobotsObject is AllowRobots))
                return new MvcHtmlString(string.Empty);

            var allowRobots = (AllowRobots) allowRobotsObject;
            var elements = new[] { "noindex", "nofollow", "noarchive" };
            if ((allowRobots & AllowRobots.Index) == AllowRobots.Index)
                elements[0] = "index";
            if ((allowRobots & AllowRobots.Follow) == AllowRobots.Follow)
                elements[1] = "follow";
            if ((allowRobots & AllowRobots.Archive) == AllowRobots.Archive)
                elements[1] = "archive";

            string content = string.Join(", ", elements);
            string metaTag = string.Format("<meta name='robots' content='{0}' />", content);
            return new MvcHtmlString(metaTag);
        }
    }
}