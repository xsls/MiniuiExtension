using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using MiniUIExtension.GridView;
using System.Linq.Expressions;
namespace MiniUIExtension
{
    public static class MiniUIHelper
    {
        public static HtmlGridView<T> GridViewFor<T>(this HtmlHelper helper, IEnumerable<T> models, string gridName) where T : class
        {
            var grid = new HtmlGridView<T>(models, gridName);
            return grid;
        }
        public static MvcHtmlString TextAreaForMiniUI<T, F>(this HtmlHelper<T> helper, Expression<Func<T, F>> expression, object htmlAttributes = null) where T : class
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<T>());
            if (metadata == null)
                return MvcHtmlString.Empty;
            TagBuilder tagBuilder = new TagBuilder("input");
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            if (HtmlAttributes == null)
                HtmlAttributes = new Dictionary<string, object>();
            HtmlAttributes.Add("class", "mini-textbox");
            HtmlAttributes.Add("id", metadata.PropertyName);
            HtmlAttributes.Add("name", metadata.PropertyName);
            if (metadata.IsRequired)
                HtmlAttributes.Add("required", "true");
            HtmlAttributes.Add("emptyText", metadata.Description);
            tagBuilder.MergeAttributes<string, object>(HtmlAttributes);
            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

    }
}
