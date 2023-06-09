using System.Linq.Expressions;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using PowRxVar._Internal.Expressions;
using PowRxVar._Internal.Vars.BndVars;
using PowRxVar._Internal.Vars.NormalVars;

namespace PowRxVar;

public static class Var
{
	public static IRoVar<T> MakeConst<T>(T val) => new RoVarConst<T>(val);


	/// <summary>
	/// Create a writable variable with an initial value
	/// <br/>
	/// <br/>
	/// Note: The calling code is responsible for calling .Dispose() on the created variable
	/// </summary>
	/// <typeparam name="T">Variable type</typeparam>
	/// <param name="initVal">Initial Value</param>
	/// <param name="initValExpr">dbg</param>
	/// <returns>Created variable</returns>
	public static IRwVar<T> Make<T>(
		T initVal,
		[CallerArgumentExpression(nameof(initVal))] string? initValExpr = null
	)
		=> new RwVar<T>(initVal, false, $"Var.Make({initValExpr})");

	public static IRwVar<T> MakeNoCheck<T>(
		T initVal,
		[CallerArgumentExpression(nameof(initVal))] string? initValExpr = null
	)
		=> new RwVar<T>(initVal, true, $"Var.Make({initValExpr})");


	public static IFullRwBndVar<T> MakeBnd<T>(
		T initVal,
		[CallerArgumentExpression(nameof(initVal))] string? initValExpr = null
	)
		=> new FullRwBndVar<T>(initVal, false, $"Var.MakeBnd({initValExpr})");

	public static IFullRwBndVar<T> MakeBndNoCheck<T>(
		T initVal,
		[CallerArgumentExpression(nameof(initVal))] string? initValExpr = null
	)
		=> new FullRwBndVar<T>(initVal, true, $"Var.MakeBnd({initValExpr})");


	/// <summary>
	/// Create a read only variable with an initial value and an observable of updates
	/// <br/>
	/// <br/>
	/// Note: Use the <see cref="DisposeExtensions.D{T}(T,IRoDispBase[])"/> extension method to tie the returned IDisposable to the lifetime of other variables and return the simple read only variable
	/// </summary>
	/// <typeparam name="T">Variable type</typeparam>
	/// <param name="initVal">Initial Value</param>
	/// <param name="obs">Observable of updates</param>
	/// <param name="initValExpr">dbg</param>
	/// <param name="obsExpr">dbg</param>
	/// <returns>Tuple containing the created variable and an IDisposable to control the underlying variable lifetime</returns>
	public static (IRoVar<T>, IDisposable) Make<T>(
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

	internal static (IRoVar<T>, IDisposable) MakeNoCheck<T>(
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
		var v = MakeNoCheck(initVal, dbgStr);
		obs.Subscribe(v).D(v);
		return (v.ToReadOnly(), v);
	}



	/// <summary>
	/// Create a read only variable with an initial value and an observable of updates that can depend on the previous value
	/// </summary>
	/// <typeparam name="T">Variable type</typeparam>
	/// <param name="initVal">Initial Value</param>
	/// <param name="obsFun">Function that takes a reference to the variable being created (as readonly) and returns the observable of updates</param>
	/// <param name="initValExpr">dbg</param>
	/// <param name="obsFunExpr">dbg</param>
	/// <returns>Tuple containing the created variable and an IDisposable to control the underlying variable lifetime</returns>
	public static (IRoVar<T>, IDisposable) Make<T>(
		T initVal,
		Func<IRoVar<T>, IObservable<T>> obsFun,
		[CallerArgumentExpression(nameof(initVal))] string? initValExpr = null,
		[CallerArgumentExpression(nameof(obsFun))] string? obsFunExpr = null
	)
	{
		var dbgStr = $"""
			Var.Make(
				{initValExpr},
				{obsFunExpr}
			)
			""";
		var v = Make(initVal, dbgStr);
		obsFun(v).Subscribe(v).D(v);
		return (v.ToReadOnly(), v);
	}


	/// <summary>
	/// Transform a writable variable into a read only variable
	/// <br/>
	/// <br/>
	/// Note: IRwVar inherits from IRoVar so you don't technically need to call this function. However calling it will prevent the user code from casting it back to an IRwVar. Similar to Observable.AsObservable() extension method
	/// </summary>
	/// <typeparam name="T">Variable type</typeparam>
	/// <param name="v">Writable variable</param>
	/// <returns>Read only variable</returns>
	public static IRoVar<T> ToReadOnly<T>(this IRwVar<T> v) => new RoVar<T>(v);


	/// <summary>
	/// Transform a fully writable bound variable into a read only variable
	/// <br/>
	/// <br/>
	/// Note: IFullRwBndVar inherits from IRoBndVar so you don't technically need to call this function. However calling it will prevent the user code from casting it back to an IFullRwBndVar. Similar to Observable.AsObservable() extension method
	/// </summary>
	/// <typeparam name="T">Variable type</typeparam>
	/// <param name="v">Writable bound variable</param>
	/// <returns>Read only bound variable</returns>
	public static IRoBndVar<T> ToReadOnly<T>(this IFullRwBndVar<T> v) => new RoBndVar<T>(v);

	/// <summary>
	/// Transform a fully writable bound variable into a (outer only) writable bound variable
	/// <br/>
	/// <br/>
	/// Note: IFullRwBndVar inherits from IRwBndVar so you don't technically need to call this function. However calling it will prevent the user code from casting it back to an IFullRwBndVar. Similar to Observable.AsObservable() extension method
	/// </summary>
	/// <typeparam name="T">Variable type</typeparam>
	/// <param name="v">Fully writable bound variable</param>
	/// <param name="vExpr">dbg</param>
	/// <returns>(outer only) writable bound variable</returns>
	public static IRwBndVar<T> ToRwBndVar<T>(
		this IFullRwBndVar<T> v,
		[CallerArgumentExpression(nameof(v))] string? vExpr = null
	)
		=> new RwBndVar<T>(v, $"ToRwBndVar({vExpr})");


	/// <summary>
	/// Transforms a variable. Equivalent to Observable.Select()
	/// <br/>
	/// <br/>
	/// Note: The resulting variable will be automatically disposed when the input variable is disposed
	/// </summary>
	/// <typeparam name="T">Input variable type</typeparam>
	/// <typeparam name="U">Output variable type</typeparam>
	/// <param name="varT">Input variable</param>
	/// <param name="fun">Function describing how to transform the variable</param>
	/// <returns>Transformed variable</returns>
	public static IRoVar<U> SelectVar<T, U>(
		this IRoVar<T> varT,
		Func<T, U> fun
	) =>
		Make(
			fun(varT.V),
			varT.Select(fun)
		).D(varT);


	/// <summary>
	/// Combine 2 variables into 1 variable. Equivalent to Observable.CombineLatest()
	/// <br/>
	/// <br/>
	/// Note: The combined variable will be automatically disposed when either of the two variables is disposed
	/// </summary>
	/// <typeparam name="T">Variable 1 type</typeparam>
	/// <typeparam name="U">Variable 2 type</typeparam>
	/// <typeparam name="V">Result variable type</typeparam>
	/// <param name="varT">Variable 1</param>
	/// <param name="varU">Variable 2</param>
	/// <param name="fun">Function describing how to mix both variable values</param>
	/// <returns>Combined variable</returns>
	public static IRoVar<V> Combine<T, U, V>(
		IRoVar<T> varT,
		IRoVar<U> varU,
		Func<T, U, V> fun
	) =>
		Make(
			fun(varT.V, varU.V),
			Obs.CombineLatest(varT, varU, fun)
		).D(varT, varU);

	/// <summary>
	/// More general version of Combine using Expression Trees (slower but easier to use)
	/// </summary>
	/// <typeparam name="T">Result variable type</typeparam>
	/// <param name="expr">Expression tree lambda that returns the resulting value in function of any number of input variables of any underlying types</param>
	/// <param name="whenNeedUpdate"></param>
	/// <returns>Combined variable</returns>
	public static IRoVar<T> Expr<T>(Expression<Func<T>> expr, IObservable<Unit>? whenNeedUpdate = null) =>
		VarExpr.Expr(expr, whenNeedUpdate);


	/// <summary>
	/// Switch to the last inner variable. Equivalent to Observable.Switch()
	/// <br/>
	/// <br/>
	/// Note: The switched variable will be automatically disposed when the main variable is disposed
	/// </summary>
	/// <typeparam name="T">Main variable type</typeparam>
	/// <typeparam name="U">Inner variable type</typeparam>
	/// <param name="varT">Main variable</param>
	/// <param name="selFun">Selector into main variable returning the inner variable</param>
	/// <returns>Switched variable</returns>
	public static IRoVar<U> SwitchVar<T, U>(
		this IRoVar<T> varT,
		Func<T, IRoVar<U>> selFun
	) =>
		Make(
			selFun(varT.V).V,
			varT.Select(selFun).Switch()
		).D(varT);
}