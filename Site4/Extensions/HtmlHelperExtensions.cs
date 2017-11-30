﻿using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Portfotolio.Domain;
using Portfotolio.Domain.Persistency;
using Portfotolio.Site.Services.Models;

namespace Portfotolio.Site4.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string UserAlias(this HtmlHelper htmlHelper)
        {
            return htmlHelper.ViewData[DataKeys.UserAlias] as string;
        }

        public static MvcHtmlString AlbumUrl(this UrlHelper urlHelper, string userAlias, Album album)
        {
            if (string.IsNullOrEmpty(userAlias))
                userAlias = album.AuthorId;

            var url = urlHelper.ActionUserId("album", new {id = userAlias, secondaryId = album.PhotosetId});
            return new MvcHtmlString(url);
        }

        public static AuthenticationInfo AuthenticationInfo(this HtmlHelper htmlHelper)
        {
            var httpContextBase = htmlHelper.ViewContext.HttpContext;
            return httpContextBase.AuthenticationInfo();
        }

        public static AuthenticationInfo AuthenticationInfo(this HttpContextBase httpContextBase)
        {
	        var authenticationStorage = DependencyResolver.Current.GetService<IAuthenticationStorage>();
	        var authenticationInfo = authenticationStorage.GetAuthenticationInfo();

            return authenticationInfo;
        }

        public static ApplicationConfiguration GetApplicationConfiguration(this HtmlHelper htmlHelper)
        {
            var applicationConfigurationProvider = DependencyResolver.Current.GetService<IApplicationConfigurationProvider>();
            return applicationConfigurationProvider.GetApplicationConfiguration();
        }

        public static MvcHtmlString MetaRobots(this HtmlHelper htmlHelper)
        {
            object allowRobotsObject = htmlHelper.ViewData[DataKeys.AllowRobots];
            if (allowRobotsObject == null || !(allowRobotsObject is AllowRobots))
                return new MvcHtmlString(string.Empty);

            var allowRobots = (AllowRobots)allowRobotsObject;
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
    }
}