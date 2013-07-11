using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Reflection;
namespace MiniUIExtension.GridView
{
    public class HtmlGridView<T> : IGridView where T : class
    {
        IEnumerable<T> _models;
        IGridColumnCollection<T> _columns;
        string _gridname = "datagrid1";
        string _url;
        bool _multiSelect = true;
        object _htmlAttributes;
        public HtmlGridView(IEnumerable<T> models, string gridname)
        {
            this._models = models;
            this._gridname = gridname ?? this._gridname;
            _columns = new GridColumnCollection<T>(this);
        }

        public IGridColumnCollection<T> Columns { get { return _columns; } }

        public HtmlGridView<T> AutoGenerateColumns()
        {
            PropertyInfo[] properties = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo pi in properties)
            {
                if (pi.CanRead)
                    _columns.Add(pi);
            }
            return this;
        }

        public HtmlGridView<T> SetColumns(Action<IGridColumnCollection<T>> columnBuild)
        {
            columnBuild(_columns);
            return this;
        }

        public HtmlGridView<T> SetUrl(string url)
        {
            _url = url;
            return this;
        }
        public HtmlGridView<T> SetMultiSelect(bool multiSelect)
        {
            _multiSelect = multiSelect;
            return this;
        }

        public HtmlGridView<T> SetHtmlAttributes(object htmlAttributes)
        {
            this._htmlAttributes = htmlAttributes;
            return this;
        }

        string RenderHeader()
        {
            var columns = new TagBuilder("div");
            columns.MergeAttribute("property", "columns");
            var indexColumn = new TagBuilder("div");
            indexColumn.MergeAttribute("type", "indexcolumn");
            columns.InnerHtml = columns.InnerHtml + indexColumn.ToString();
            if (this._multiSelect)
            {
                var checkColumn = new TagBuilder("div");
                checkColumn.MergeAttribute("type", "checkcolumn");
                columns.InnerHtml = columns.InnerHtml + checkColumn.ToString();
            }
            foreach (var item in this.Columns)
            {
                var column = new TagBuilder("div");
                IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(item.HtmlAttributes);
                if (HtmlAttributes == null)
                    HtmlAttributes = new Dictionary<string, object>();
                HtmlAttributes.Add("field", item.DataField);
                HtmlAttributes.Add("allowSort", item.Sortable.ToString().ToLower());
                HtmlAttributes.Add("width", string.IsNullOrEmpty(item.Width) ? "100" : item.Width);
                HtmlAttributes.Add("headerAlign", string.IsNullOrEmpty(item.Align) ? "center" : item.Align);
                column.MergeAttributes<string, object>(HtmlAttributes);
                column.SetInnerText(item.Title);
                columns.InnerHtml = columns.InnerHtml + column.ToString();
            }
            return columns.ToString();
        }
        public string ToHtmlString()
        {
            var gridview = new TagBuilder("div");
            gridview.GenerateId(this._gridname);
            gridview.AddCssClass("mini-datagrid");
            IDictionary<string, object> HtmlAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(this._htmlAttributes);
            if (HtmlAttributes == null)
                HtmlAttributes = new Dictionary<string, object>();
            HtmlAttributes.Add("style", "width: 100%; height: 100%;");
            HtmlAttributes.Add("url", this._url);
            HtmlAttributes.Add("showColumnsMenu", "true");
            HtmlAttributes.Add("multiSelect", this._multiSelect.ToString().ToLower());
            gridview.MergeAttributes<string, object>(HtmlAttributes);
            string temp = RenderHeader();
            gridview.InnerHtml = temp;
            return gridview.ToString();
        }
    }
}
