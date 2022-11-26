using LHZ.LinqToSql.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LHZ.LinqToSql
{
    /// <summary>
    /// Linq查询类
    /// </summary>
    /// <typeparam name="T">需查询的表映射对象类型</typeparam>
    public class SelectQueryable<T> : IQueryable<T>, IQueryable
    {
        private static string _tableName;
        private static Type _elementType = typeof(T);
        private Expression _expression;
        private IQueryProvider _queryProvider = new SelectQ3 ueryProvider();
        /// <summary>
        /// 获取表名
        /// </summary>
        public string TableName
        {
            get 
            {
                if (_tableName == null)
                {
                    var attr = System.Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute)) as TableAttribute;
                    if (attr != null)
                    {
                        _tableName = attr.TableName;
                    }
                    else
                    {
                        _tableName = typeof(T).Name;
                    }
                }
                return _tableName;
            }
        }
        public Type ElementType => _elementType;

        public Expression Expression => _expression;

        public IQueryProvider Provider => throw new NotImplementedException();

        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
