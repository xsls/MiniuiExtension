using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Reflection;

namespace MiniUIExtension.GridView
{
    public interface IGridColumnCollection : IEnumerable<IGridColumn>
    {

    }
    public interface IGridColumnCollection<T> : IGridColumnCollection
    {
        IGridColumn<T> Add(IGridColumn<T> column);
        IGridColumn<T> Add<F>(Expression<Func<T, F>> express);
        IGridColumn<T> Add(PropertyInfo property);
    }
}
