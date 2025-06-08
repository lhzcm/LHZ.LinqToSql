using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LHZ.LinqToSql.Enums;

namespace LHZ.LinqToSql
{
    /// <summary>
    /// Linq查询类
    /// </summary>
    /// <typeparam name="T">需查询的表映射对象类型</typeparam>
    public class SelectQueryable<T> : IQueryable<T>, IQueryable
    {
        private static string _tableName;
        /// <summary>
        /// 字符串
        /// </summary>
        private readonly StringBuilder _strBuilder = new StringBuilder();
        //类型
        private static Type _elementType = typeof(T);
        //表达式
        private readonly Expression _expression;
        private readonly SelectQueryProvider _queryProvider;
      
        private readonly List<object> _parameters = new List<object>();
        //最后的操作
        private readonly SQLStatement lastOpear = SQLStatement.Select;
        private readonly bool _hasWhere = false;
        public List<object> SqlParameters => _parameters;

        public void AppendSQLWhere()
        {
            if (!_hasWhere)
            {
                _strBuilder.Append(" where");
            }
        }

        public void AddParamters(object parameter)
        {
            _strBuilder.Append(" {");
            _strBuilder.Append(_parameters.Count);
            _strBuilder.Append("}");

            _parameters.Add(parameter);
        }

        public void AppendSQLString(string str)
        {
            _strBuilder.Append(str);
        }

        public void InsertSQLString(string str, int index)
        {
            _strBuilder.Insert(index, str);
        }
        public int SQLStringLength => _strBuilder.Length;

        public SelectQueryable()
        {
            //_expression = Expression.Parameter(typeof(T));
            _expression = Expression.Constant(this);
            this._queryProvider = new SelectQueryProvider(this);
            //this._strBuilder = "select "
        }

        public SelectQueryable(Expression expression)
        {
            this._expression = expression;
        }

        public SelectQueryable(SelectQueryProvider queryProvider)
        {
            this._expression = queryProvider.Expression;
            this._queryProvider = queryProvider;
        }

        /// <summary>
        /// 获取表名
        /// </summary>
        public string TableName
        {
            get 
            {
                return "t";
                // if (_tableName == null)
                // {
                //     var attr = System.Attribute.GetCustomAttribute(typeof(T), typeof(TableAttribute)) as TableAttribute;
                //     if (attr != null)
                //     {
                //         _tableName = attr.Name;
                //     }
                //     else
                //     {
                //         _tableName = typeof(T).Name;
                //     }
                // }
                // return _tableName;
            }
        }
        public Type ElementType => _elementType;

        public Expression Expression => _expression;

        public IQueryProvider Provider => _queryProvider;

        public IEnumerator<T> GetEnumerator()
        {
            //throw new NotImplementedException();
            return null;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public StringBuilder ToSql()
        {
            return _queryProvider.ToSql();
        }
    }
}
