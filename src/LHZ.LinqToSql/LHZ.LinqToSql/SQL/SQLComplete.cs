using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Reflection;
using System.Text;

namespace LHZ.LinqToSql.SQL
{
    internal struct SQLCompileStatus
    { 
        /// <summary>
        /// 是否引用了参数
        /// </summary>
        public bool HasLinqParameter { get; set; } 
    }
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

        public static SQLCompileStatus WhereComplete<TSource>(SelectQueryable<TSource> sources, Expression<Func<TSource, bool>> predicate)
        {
            sources.AppendSQLWhere();
            return Complete(sources, predicate.Body);
        }

        public static SQLCompileStatus BinaryComplete<TSource>(SelectQueryable<TSource> sources, BinaryExpression expression)
        {
            sources.AppendSQLString(" (");
            var leftStatus = Complete(sources, expression.Left);
            switch (expression.NodeType)
            {
                case ExpressionType.AndAlso:
                    {
                        sources.AppendSQLString(" and");
                        break;
                    }
                case ExpressionType.OrElse:
                    {
                        sources.AppendSQLString(" or");
                        break;
                    }
                case ExpressionType.Equal:
                    {
                        sources.AppendSQLString(" =");
                        break;
                    }
                case ExpressionType.NotEqual:
                    {
                        sources.AppendSQLString(" !=");
                        break;
                    }
                case ExpressionType.GreaterThan:
                    {
                        sources.AppendSQLString(" >");
                        break;
                    }
                case ExpressionType.GreaterThanOrEqual:
                    {
                        sources.AppendSQLString(" >=");
                        break;
                    }
                case ExpressionType.LessThan:
                    {
                        sources.AppendSQLString(" <");
                        break;
                    }
                case ExpressionType.LessThanOrEqual:
                    {
                        sources.AppendSQLString(" <=");
                        break;
                    }
                case ExpressionType.Add:
                case ExpressionType.AddChecked:
                    {
                        sources.AppendSQLString(" +");
                        break;
                    }
                case ExpressionType.AddAssign:
                case ExpressionType.AddAssignChecked:
                    {
                        sources.AppendSQLString(" +=");
                        break;
                    }
                case ExpressionType.Subtract:
                case ExpressionType.SubtractChecked:
                    {
                        sources.AppendSQLString(" -");
                        break;
                    }
                case ExpressionType.SubtractAssign:
                case ExpressionType.SubtractAssignChecked:
                    {
                        sources.AppendSQLString(" -=");
                        break;
                    }
                case ExpressionType.Multiply:
                case ExpressionType.MultiplyChecked:
                    {
                        sources.AppendSQLString(" *");
                        break;
                    }
                case ExpressionType.MultiplyAssign:
                case ExpressionType.MultiplyAssignChecked:
                    {
                        sources.AppendSQLString(" *=");
                        break;
                    }
                case ExpressionType.Divide:
                    {
                        sources.AppendSQLString(" /");
                        break;
                    }
                case ExpressionType.DivideAssign:
                    {
                        sources.AppendSQLString(" /=");
                        break;
                    }
                default: throw new Exception("未实现的运算符");
            }
            var rightStatus = Complete(sources, expression.Right);
            sources.AppendSQLString(" )");
            return leftStatus.HasLinqParameter ? leftStatus : rightStatus;
        }

        public static SQLCompileStatus MemberComplete<TSource>(SelectQueryable<TSource> sources, MemberExpression expression)
        {
            var status = Complete(sources, expression.Expression);
            if (status.HasLinqParameter)
            {
                sources.AppendSQLString(".");
                sources.AppendSQLString(expression.Member.Name);
            }
            else
            {
                sources.SqlParameters[sources.SqlParameters.Count - 1] = expression.Member.DeclaringType
                .InvokeMember(expression.Member.Name,
                (expression.Member.MemberType == MemberTypes.Field ? BindingFlags.GetField : BindingFlags.GetProperty) | BindingFlags.Public | BindingFlags.Instance, null,
                sources.SqlParameters[sources.SqlParameters.Count - 1], null);
            }
            return status;
        }

        public static SQLCompileStatus ConstantComplete<TSource>(SelectQueryable<TSource> sources, ConstantExpression expression)
        {
            sources.AddParamters(expression.Value);
            return new SQLCompileStatus() { HasLinqParameter = false };
        }
        public static SQLCompileStatus ParameterComplete<TSource>(SelectQueryable<TSource> sources, ParameterExpression expression)
        {
            sources.AppendSQLString(" ");
            sources.AppendSQLString(sources.TableName);
            return new SQLCompileStatus() { HasLinqParameter = true };
        }
        public static SQLCompileStatus MethodCallComplete<TSource>(SelectQueryable<TSource> sources, MethodCallExpression expression)
        {
            if (expression.Object is null)
            {
                throw new Exception("未实现的方法调用");
            }
            var curLength = sources.SQLStringLength;
            switch (expression.Method.Name)
            {
                case "ToString":
                    {
                        sources.InsertSQLString(" convert(varchar,", curLength);
                        var status = Complete(sources, expression.Object);
                        sources.AppendSQLString(")");
                        return status;
                    }
                default:
                    throw new Exception("未实现的sql方法");
            }
        }

        public static SQLCompileStatus Complete<TSource>(SelectQueryable<TSource> sources, Expression expression)
        {
            if (expression is BinaryExpression binaryExpression)
            {
                return BinaryComplete(sources, binaryExpression);
            }
            else if (expression is MemberExpression memberExpression)
            {
                return MemberComplete(sources, memberExpression);
            }
            else if (expression is ConstantExpression constantExpression)
            {
                return ConstantComplete(sources, constantExpression);
            }
            else if (expression is ParameterExpression parameterExpression)
            {
                return ParameterComplete(sources, parameterExpression);
            }
            else if (expression is MethodCallExpression methodCallExpression)
            {
                return MethodCallComplete(sources, methodCallExpression);
            }
            else
            {
                throw new Exception("未实现的表达式");
            }
        }

    }
}
