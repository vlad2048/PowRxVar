using System.Reactive.Linq;
using PowRxVar._Internal.Vars;
using PowRxVar.Utils.Extensions;
using PowRxVar.Vars;

namespace PowRxVar;

public static class Var
{
	/// <summary>
	/// Create a writable variable with an initial value
	/// <br/>
	/// <br/>
	/// Note: The calling code is responsible for calling .Dispose() on the created variable
	/// </summary>
	/// <typeparam name="T">Variable type</typeparam>
	/// <param name="initVal">Initial Value</param>
	/// <returns>Created variable</returns>
	public static IRwVar<T> Make<T>(T initVal) => new RwVar<T>(initVal);


	/// <summary>
	/// Create a read only variable with an initial value and an observable of updates
	/// <br/>
	/// <br/>
	/// Note: Use the <see cref="DisposeExtensions.D{T}(T,PowRxVar.Vars.IRoDispBase[])"/> extension method to tie the returned IDisposable to the lifetime of other variables and return the simple read only variable
	/// </summary>
	/// <typeparam name="T">Variable type</typeparam>
	/// <param name="initVal">Initial Value</param>
	/// <param name="obs">Observable of updates</param>
	/// <returns>Tuple containing the created variable and an IDisposable to control the underlying variable lifetime</returns>
	public static (IRoVar<T>, IDisposable) Make<T>(T initVal, IObservable<T> obs)
	{
		var v = Make(initVal);
		obs.Subscribe(v).D(v);
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
	/// Transform a writable bound variable into a read only variable
	/// <br/>
	/// <br/>
	/// Note: IRwBndVar inherits from IRoBndVar so you don't technically need to call this function. However calling it will prevent the user code from casting it back to an IRwVar. Similar to Observable.AsObservable() extension method
	/// </summary>
	/// <typeparam name="T">Variable type</typeparam>
	/// <param name="v">Writable bound variable</param>
	/// <returns>Read only bound variable</returns>
	public static IRoBndVar<T> ToReadOnly<T>(this IRwBndVar<T> v) => new RoBndVar<T>(v);


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
	public static IRoVar<U> SwitchVar<T, U>(this IRoVar<T> varT, Func<T, IRoVar<U>> selFun) =>
		Make(
			selFun(varT.V).V,
			varT.Select(selFun).Switch()
		).D(varT);





	public static IRwBndVar<T> MakeBnd<T>(T initVal) => new RwBndVar<T>(initVal);
}