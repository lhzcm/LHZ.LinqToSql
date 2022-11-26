using System;
using System.Collections.Generic;
using System.Text;

namespace LHZ.LinqToSql.SQL
{
    /// <summary>
    /// SQL编译转化完成类
    /// </summary>
    internal class SQLComplete
    {
        private string _sql;
        private SQLParamter[] _sqlParamters;
        public SQLComplete(string sql, SQLParamter[] sqlParamters)
        {
            _sql = sql;
            _sqlParamters = sqlParamters;
        }
        /// <summary>
        /// 获取SQL执行对象
        /// </summary>
        /// <returns></returns>
        public virtual SQLExecute GetExecute()
        {
            return new SQLExecute();
        }

    }
}
