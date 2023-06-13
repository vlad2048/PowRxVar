using System.Runtime.CompilerServices;
using PowRxVar._Internal.Vars.BndVars;
using PowRxVar._Internal.Vars.NormalVars;

namespace PowRxVar;

public static class VarNoCheck
{
	public static IRwVar<T> Make<T>(
		T initVal,
		[CallerArgumentExpression(nameof(initVal))] string? initValExpr = null
	)
		=> new RwVar<T>(initVal, true, $"Var.Make({initValExpr})");


	public static IFullRwBndVar<T> MakeBnd<T>(
		T initVal,
		[CallerArgumentExpression(nameof(initVal))] string? initValExpr = null
	)
		=> new FullRwBndVar<T>(initVal, true, $"Var.MakeBnd({initValExpr})");


	internal static (IRoVar<T>, IDisposable) Make<T>(
		T initVal,
		IObservable<T> obs,
		[CallerArgumentExpression(nameof(initVal))] string? initValExpr = null,
		[CallerArgumentExpression(nameof(obs))] string? obsExpr = null
	)
	{
		var dbgStr = $"""
			Var.Make(
				{initValExpr},
				{obsExpr}
			)
			""";
		var v = Make(initVal, dbgStr);
		obs.Subscribe(v).D(v);
		return (v.ToReadOnly(), v);
	}
}