using System.Linq.Expressions;
using PowRxVar._Internal.Expressions.Structs;
using PowRxVar._Internal.Expressions.Utils;

namespace PowRxVar._Internal.Expressions.Visitors;

class VarPickerExprVisitor : ExpressionVisitor
{
	private readonly HashSet<object> addedVars = new();
	private readonly List<VarNfo> vars = new();
	public VarNfo[] Vars => vars.ToArray();

	protected override Expression VisitMember(MemberExpression node)
	{
		var valType = Reflex.IsVarVAccessor(node);
		if (valType != null)
		{
			var varNfo = new VarNfo(valType, node);
			var rwVar = varNfo.GetVar();
			if (!addedVars.Contains(rwVar))
			{
				addedVars.Add(rwVar);
				vars.Add(varNfo);
			}
		}

		return base.VisitMember(node);
	}
}