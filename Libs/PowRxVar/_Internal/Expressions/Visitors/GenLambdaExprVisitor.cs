using System.Linq.Expressions;
using PowRxVar._Internal.Expressions.Structs;
using PowRxVar._Internal.Expressions.Utils;

namespace PowRxVar._Internal.Expressions.Visitors;

sealed class GenLambdaExprVisitor : ExpressionVisitor
{
	private readonly Dictionary<object, VarNfo> varMap;
	private readonly ParameterExpression[] paramExprs;
	private bool isFirstLambda = true;

	public GenLambdaExprVisitor(VarNfo[] vars)
	{
		varMap = vars.ToDictionary(
			e => e.GetVar(),
			e => e
		);
		paramExprs = vars.Select(e => e.ParamExpr).ToArray();
	}

	protected override Expression VisitLambda<T>(Expression<T> node)
	{
		if (!isFirstLambda) return base.VisitLambda(node);
		isFirstLambda = false;

		var lambdaBody = Visit(node.Body);
	
		return Expression.Lambda(
			lambdaBody,
			paramExprs
		);
	}

	protected override Expression VisitMember(MemberExpression node)
	{
		var newNode = Replace(node);
		return newNode;
	}

	private Expression Replace(Expression expr)
	{
		if (expr is not MemberExpression memberExpr) return base.Visit(expr);
		var valType = Reflex.IsVarVAccessor(memberExpr);
		if (valType == null) return base.VisitMember(memberExpr);

		var tempVarNfo = new VarNfo(valType, memberExpr);
		var rwVar = tempVarNfo.GetVar();

		var varNfo = varMap[rwVar];
		return varNfo.ParamExpr;
	}
}