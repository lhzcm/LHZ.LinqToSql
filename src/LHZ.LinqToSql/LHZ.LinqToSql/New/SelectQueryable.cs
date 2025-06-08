using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LHZ.LinqToSql.SQL;

namespace LHZ.LinqToSql.New
{
    public class SelectQueryable<T> : IQueryable<T>, IQueryable
    {
        private readonly Expression _expression = null;
        private readonly static Type _type = typeof(T);
        private readonly SelectQueryProvider _provider;

        public Expression Expression => _expression;

        public Type ElementType => _type;

        public SelectQueryable()
        {
            string tableName;
            object tableAttr = null;//Attribute.GetCustomAttribute(_type, typeof(TableAttribute)) as TableAttribute;
            if (tableAttr == null)
            {
                tableName = _type.Name;
            }
            else
            {
                tableName = "";// tableAttr.Name;
            }
            _expression = Expression.Constant(this);
            _provider = new SelectQueryProvider(this);
        }
        public SelectQueryable(Expression expression)
        {
            _expression = expression;
            _provider = new SelectQueryProvider(this);
        }

        public IQueryProvider Provider => _provider;

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }
}
