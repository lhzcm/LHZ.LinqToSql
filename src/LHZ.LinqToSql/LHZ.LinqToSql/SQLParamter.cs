using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LHZ.LinqToSql
{
    internal class SQLParamter
    {
        private string _name;
        private Func<Expression, object> _valueFunc;
    }
}
