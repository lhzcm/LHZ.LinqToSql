using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LHZ.LinqToSql.SQL
{
    /// <summary>
    /// SQL参数类
    /// </summary>
    internal class SQLParamter
    {
        private string _name;
        private Func<Expression, object> _valueFunc;

        public SQLParamter(string paramterName, Func<Expression, object> valueFunc)
        {
            _name = paramterName;
            _valueFunc = valueFunc;
        }

        public SQLParamter(string paramterName, object value)
        {
            _name = paramterName;
            _valueFunc = n=> value;
        }

        public string Name => _name;
        public object GetValue(Expression expression)
        {
            return _valueFunc(expression);
        }
    }
}
