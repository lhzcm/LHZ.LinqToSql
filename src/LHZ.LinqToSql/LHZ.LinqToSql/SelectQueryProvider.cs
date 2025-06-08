using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using LHZ.LinqToSql.SQL;

namespace LHZ.LinqToSql
{
    public class SelectQueryProvider : IQueryProvider
    {
        private Expression _expresstion;
        public Expression Expression => _expresstion;
        private readonly IQueryable _queryable;

        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }
        public SelectQueryProvider(IQueryable queryable)
        {
            _queryable = queryable;
        }
        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            
            if(expression is MethodCallExpression callExpression)
            {
                switch(callExpression.Method.Name)
                {
                    case "Where" : 
                    {
                        if(callExpression.Arguments.Count == 2)
                        {
                            var a = ((UnaryExpression) callExpression.Arguments[1]);
                            SQLComplete.WhereComplete((SelectQueryable<TElement>)_queryable, (Expression<Func<TElement, bool>>)a.Operand);
                        }
                        break;
                    }
                    default : throw new Exception("未实现该linq方法");
                }
                if(callExpression.Method.Name == "Where" && callExpression.Arguments.Count == 2)
                {

                }

            }
            else
            {
                throw new Exception("未实现的表达式");
            }
            _expresstion = expression;
            return new SelectQueryable<TElement>(this);
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
