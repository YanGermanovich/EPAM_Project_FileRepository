using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Reflection;
namespace CustomExpressionVisitor
{
    class CustomExpressionVisitor<TSourse,TResult>  : ExpressionVisitor
    {
        private ParameterExpression parameter;
        public Expression Modify(Expression expression, ParameterExpression parameter)
        {
            this.parameter = parameter;
            return Visit(expression);
        }
        protected override Expression VisitLambda<T>(Expression<T> node)
        {
            return Expression.Lambda<Func<TResult, bool>>(Visit(node.Body), Expression.Parameter(typeof(TResult)));
        }

        protected override Expression VisitMember(System.Linq.Expressions.MemberExpression node)
        {
            var att = node.Member.GetCustomAttribute<CustomAttributeMapper>();
            if (att != null)
            {
                var newMember = typeof(TResult).GetProperty(att.MapTo);
                if (newMember != null)
                {
                    return Expression.Property(
                        parameter,newMember);
                }
            }
            return base.VisitMember(node);
        }

    }
}
