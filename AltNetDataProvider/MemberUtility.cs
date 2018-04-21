using System;
using System.Linq.Expressions;
using System.Reflection;

namespace AltNetDataProvider
{
    public static class MemberUtility
    {
        public static MemberInfo GetMemberInfo<TSource, TMember>(Expression<Func<TSource, TMember>> propTransform)
        {
            LambdaExpression lambda = propTransform;
            if (lambda == null)
            {
                throw new ArgumentException("Not a lambda expression", nameof(propTransform));
            }

            MemberExpression memberExpr = null;

            switch (lambda.Body.NodeType)
            {
                case ExpressionType.Convert:
                    memberExpr = ((UnaryExpression) lambda.Body).Operand as MemberExpression;
                    break;
                case ExpressionType.MemberAccess:
                    memberExpr = lambda.Body as MemberExpression;
                    break;
            }

            if (memberExpr == null)
            {
                throw new InvalidOperationException("Not a member access");
            }

            return memberExpr.Member;
        }
    }
}