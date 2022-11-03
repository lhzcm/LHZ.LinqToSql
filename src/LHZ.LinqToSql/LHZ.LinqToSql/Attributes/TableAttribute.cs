using System;
using System.Collections.Generic;
using System.Text;

namespace LHZ.LinqToSql.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public class TableAttribute : Attribute
    {
        private readonly string _tableName;
        public TableAttribute(string tableName)
        {
            _tableName = tableName;
        }
        public string TableName => _tableName;
    }
}
