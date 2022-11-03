using System.Linq.Expressions;

namespace LHZ.LinqToSql.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Queryable

            Expression<Func<TestClass, bool>> exp1 = n => n.Id == 2;
            var exp1hashcode = ExpressionHashCode.GetExpressionHashCode(exp1);

            Expression<Func<TestClass, bool>> exp2 = n => n.Id == 2;
            var exp2hashcode = ExpressionHashCode.GetExpressionHashCode(exp2);


            TestClass tc = new TestClass();

            Expression<Func<TestClass, bool>> exp3 = n => n.Name.Length == tc.Name.Length;
            var exp3hashcode = ExpressionHashCode.GetExpressionHashCode(exp3);

        }
    }

    public class TestClass
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}