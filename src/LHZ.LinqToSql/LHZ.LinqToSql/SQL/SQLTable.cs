using System;

namespace LHZ.LinqToSql.SQL
{

    public class SQLTable
    {
        private string _tableName;
        public SQLTable(string tableName)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("表名不能为空！");
            }
            _tableName = tableName;
        }
        public string TableName;
    }
}
