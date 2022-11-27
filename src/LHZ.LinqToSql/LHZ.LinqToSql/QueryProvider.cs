using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace LHZ.LinqToSql
{
    public class SelectQueryProvider : IQueryProvider
    {
        private Expression _expresstion;

        public Expression Expression => _expresstion;
        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            _expresstion = expression;
            return new SelectQueryable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {

            return default(TResult);
        }

        public StringBuilder ToSql()
        {
            StringBuilder strBuilder = new StringBuilder();
            return strBuilder;
        }
    }
}
