using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using System.Collections.ObjectModel;
using System.Reflection;

namespace MiniUIExtension.GridView
{
    public class GridColumnCollection<T> : Collection<IGridColumn>, IGridColumnCollection<T>
    {
        IGridView _grid;
        public GridColumnCollection(IGridView grid)
        {
            _grid = grid;
        }
        public IGridColumn<T> Add(IGridColumn<T> column)
        {
            if (column == null)
                throw new ArgumentNullException("column");
            base.Add(column);
            return column;
        }

        public IGridColumn<T> Add<F>(Expression<Func<T, F>> expression)
        {
            if (expression != null || expression.Body is MemberExpression)
            {
                IGridColumn<T> column = CreateGridColumn(expression);
                Add(column);
                return column;
            }
            throw new ArgumentException("express  is not correct");
        }

        public IGridColumn<T> Add(PropertyInfo property)
        {
            IGridColumn<T> column = CreateGridColumn(property);
            Add(column);
            return column;
        }

        private IGridColumn<T> CreateGridColumn<F>(Expression<Func<T, F>> expression)
        {
            IGridColumn<T> column = new GridColumn<T, F>(expression, _grid);
            return column;
        }

        private IGridColumn<T> CreateGridColumn(PropertyInfo property)
        {
            Type entityType = typeof(T);
            Type columnType = typeof(GridColumn<,>).MakeGenericType(entityType, property.PropertyType);

            ParameterExpression parameter = Expression.Parameter(entityType, "e");
            MemberExpression expressionProperty = Expression.Property(parameter, property);

            Type funcType = typeof(Func<,>).MakeGenericType(entityType, property.PropertyType);
            LambdaExpression lambda = Expression.Lambda(funcType, expressionProperty, parameter);

            return Activator.CreateInstance(columnType, lambda, _grid) as IGridColumn<T>;
        }
    }
}
