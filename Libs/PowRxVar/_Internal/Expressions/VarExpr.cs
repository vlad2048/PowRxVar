using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using PowRxVar._Internal.Expressions.Utils;

namespace PowRxVar._Internal.Expressions;

static class VarExpr
{
	public static IRoVar<T> Expr<T>(Expression<Func<T>> expr, IObservable<Unit>? whenNeedUpdate = null)
	{
		var vars = ExprUtils.PickVars(expr);
		var genLambda = ExprUtils.CreateGenLambda(expr, vars);
		object[] ParamsFun() => vars.Select(e => e.GetVal()).ToArray();

		var roVars = vars.Select(e => e.GetVar()).ToArray();
		var varDisps = roVars.OfType<IRoDispBase>().ToArray();
		var d = new Disp().D(varDisps);
		var whenChangeArr = roVars.Select(Reflex.GetWhenVarChange).ToArray();
		var whenAnyChange = (whenNeedUpdate is not null) switch
		{
			true => Observable.Merge(
				whenNeedUpdate!,
				Observable.Merge(whenChangeArr)
			),
			false => Observable.Merge(whenChangeArr)
		};

		var varArr = ExprUtils.PrecompileLambda(genLambda);
		T Calc() => ExprUtils.EvalLambda<T>(varArr, ParamsFun);

		var resultVar = Var.Make(
			Calc(),
			whenAnyChange.Select(_ => Calc())
		).D(d);

		return resultVar;
	}
}