using System.Linq.Expressions;

namespace WebAGK.Shared.Infrastructure.Repositories;

public class ExpressionRebinder<TSource, TDestination>(ParameterExpression parameter) : ExpressionVisitor {
    
    protected override Expression VisitMember(MemberExpression node)
    {
        if (node.Expression != null && node.Expression.NodeType == ExpressionType.Parameter &&
            node.Expression.Type == typeof(TSource))
        {
            var _newMember = typeof(TDestination).GetProperty(node.Member.Name);
            return Expression.MakeMemberAccess(parameter, _newMember);
        }

        return base.VisitMember(node);
    }

    protected override Expression VisitParameter(ParameterExpression node)
    {
        return node.Type == typeof(TSource) ? parameter : base.VisitParameter(node);
    }
}