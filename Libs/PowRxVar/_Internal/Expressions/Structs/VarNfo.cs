using System.Linq.Expressions;
using Expr = System.Linq.Expressions.Expression;

namespace PowRxVar._Internal.Expressions.Structs;

class VarNfo
{
	private static readonly Type varTypeOpen = typeof(IRoVar<>);

	private readonly Func<object> pickVarFun;
	private readonly Func<object> pickValFun;

	public Type ValType { get;}
	public Type VarType => varTypeOpen.MakeGenericType(ValType);
	public object GetVar() => pickVarFun();
	public object GetVal() => pickValFun();
	public ParameterExpression ParamExpr { get; }

	public VarNfo(Type valType, MemberExpression node)
	{
		ValType = valType;
		ParamExpr = Expr.Parameter(ValType);

		var pickVarLambda = Expr.Lambda<Func<object>>(
			node.Expression!,
			Array.Empty<ParameterExpression>()
		);

		Expr valNode = ValType.IsValueType
			? Expr.Convert(node, typeof(object))
			: node;

		var pickValLambda = Expr.Lambda<Func<object>>(
			valNode,
			Array.Empty<ParameterExpression>()
		);

		pickValFun = pickValLambda.Compile();
		pickVarFun = pickVarLambda.Compile();
	}

	public override string ToString() => $"var({ValType.Name})";
}