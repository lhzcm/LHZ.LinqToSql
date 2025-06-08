using System.Linq;
using System.Linq.Expressions;

namespace LHZ.LinqToSql.New
{
    public class SelectQueryProvider : IQueryProvider
    {
        private readonly IQueryable _queryable;
        public SelectQueryProvider(IQueryable queryable)
        {
            _queryable = queryable;
        }

        public IQueryable CreateQuery(Expression expression)
        {
            throw new System.NotImplementedException();
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return new SelectQueryable<TElement>(expression);
        }

        public object Execute(Expression expression)
        {
            throw new System.NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            throw new System.NotImplementedException();
        }

    }
}
