using System;
using System.Text;
using System.Web.Mvc;

namespace KuProStore.UI.HtmlHelpers
{
    public static class PagingHelper
    {
        public static MvcHtmlString PageLinks(this HtmlHelper htmlHelper, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder strBuilder = new StringBuilder();

            for (int i=1;i<=pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                {
                    tag.AddCssClass("selected");
                    tag.AddCssClass("btn-primary");
                }                
                tag.AddCssClass("btn btn-default");
                strBuilder.Append(tag.ToString());
            }

            return MvcHtmlString.Create(strBuilder.ToString());
        }
    }
}