using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace MiniUIExtension.GridView
{
    public interface IColumn
    {
        string Title { get; set; }
        string DataField { get; set; }
        string Width { get; set; }
        bool Visible { get; set; }
        string Align { get; set; }
        bool Sortable { get; set; }
        object HtmlAttributes { get; set; }
        Type DataType { get; set; }
        ModelMetadata Metadata { get; }
    }

    public interface IGridColumn : IColumn
    {
        IGridView Grid { get; }
    }
    public interface IGridColumn<T> : IGridColumn
    {
        IGridColumn<T> SetDataField(string datafield);
        IGridColumn<T> SetWidth(string width);
        IGridColumn<T> SetTitle(string title);
        IGridColumn<T> SetSortable(bool Sortable);
        IGridColumn<T> SetHtmlAttributes(object htmlAttributes);
    }

}
