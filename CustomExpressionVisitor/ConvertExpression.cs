using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomExpressionVisitor
{
    public class ConvertExpression<TSourse, TResult>
    {
        public static Expression<Func<TResult, bool>> Convert (Expression<Func<TSourse, bool>> expression, string resultMember)
        {
            var resultParameter = Expression.Parameter(typeof(TResult), resultMember);
            var newExpression = Expression.Lambda<Func<TResult, bool>>(new CustomExpressionVisitor<TSourse,TResult>().Modify(expression.Body, resultParameter), resultParameter);
            return newExpression;
        }
    }
}
