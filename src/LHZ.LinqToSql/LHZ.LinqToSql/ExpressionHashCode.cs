using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace LHZ.LinqToSql
{
    public class ExpressionHashCode
    {
        private static int[] _expressionTypeHashCodes = new int[84];
        private static readonly int _rand = new Random().Next();

        static ExpressionHashCode()
        {
            foreach (var type in _expressionTypeHashCodes)
            {
                _expressionTypeHashCodes[type] = HashCode.Combine(type, _rand);
            }
        }

        public static int GetExpressionHashCode(Expression expression)
        {
            if (expression == null)
            {
                return 0;
            }
            return LDRReadExpression(expression);
        }

        private static int LDRReadExpression(Expression expression)
        {
            var binary = expression as BinaryExpression;
            int nodecode = _expressionTypeHashCodes[(int)expression.NodeType];

            if (binary != null)
            {
                return HashCode.Combine(nodecode, LDRReadExpression(binary.Left), LDRReadExpression(binary.Right));
            }
            var parameter = expression as ParameterExpression;
            if (parameter != null)
            {
                return HashCode.Combine(nodecode, parameter.Name, parameter.NodeType.GetHashCode());
            }

            var constant = expression as ConstantExpression;
            if (constant != null)
            {
                return HashCode.Combine(nodecode, constant.Value.GetHashCode());
            }
            var menber = expression as MemberExpression;
            if (menber != null)
            {
                return HashCode.Combine(nodecode, menber.Member.MetadataToken, menber.Expression == null ? 0 : LDRReadExpression(menber.Expression));
            }

            var lambda = expression as LambdaExpression;
            if (lambda != null)
            {
                int paramterCode = 0;
                foreach (var item in lambda.Parameters)
                {
                    paramterCode = HashCode.Combine(paramterCode, item.Name, item.NodeType.GetHashCode());
                }
                return HashCode.Combine(nodecode, lambda.Name, lambda.ReturnType.GetHashCode(), paramterCode, LDRReadExpression(lambda.Body));
            }
            throw new Exception("未能解析Expression类型" + expression.NodeType.ToString());
        }
    }
}
