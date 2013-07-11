using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Reflection;
namespace MiniUIExtension.GridView
{
    public class GridColumn<T, F> : IGridColumn<T>
    {
        //private readonly Func<T, F> _constraint;
        public ModelMetadata Metadata { get; protected set; }
        public IGridView Grid { get; protected set; }
        public string DataField { get; set; }
        public string Width { get; set; }
        public bool Visible { get; set; }
        public bool Sortable { get; set; }
        public string Align { get; set; }
        public string Title { get; set; }
        public Type DataType { get; set; }
        public object HtmlAttributes { get; set; }

        public GridColumn(Expression<Func<T, F>> expression, IGridView grid)
        {
            Grid = grid;
            Metadata = ModelMetadata.FromLambdaExpression(expression, new ViewDataDictionary<T>());
            this.Title = Metadata.DisplayName ?? Metadata.PropertyName;
            this.DataField = Metadata.PropertyName;
            this.DataType = Metadata.ModelType;
        }

        public GridColumn(PropertyInfo property, IGridView grid)
        {
            Grid = grid;
            Metadata = ModelMetadata.FromStringExpression(property.Name, new ViewDataDictionary<T>());
            this.Title = Metadata.DisplayName ?? Metadata.PropertyName;
            this.DataField = Metadata.PropertyName;
            this.DataType = Metadata.ModelType;
        }

        public IGridColumn<T> SetDataField(string datafield)
        {
            DataField = datafield; return this;
        }

        public IGridColumn<T> SetWidth(string width)
        {
            Width = width; return this;
        }

        public IGridColumn<T> SetSortable(bool sortable)
        {
            Sortable = sortable; return this;
        }

        public IGridColumn<T> SetHtmlAttributes(object htmlAttributes)
        {
            HtmlAttributes = htmlAttributes;
            return this;
        }

        public IGridColumn<T> SetTitle(string title)
        {
            Title = title; return this;
        }
    }
}
