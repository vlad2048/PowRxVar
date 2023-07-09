using System.Reactive.Linq;
using PowMaybe;
using PowRxVar.Maybe._Internals;
using PowRxVar.Utils.Extensions;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class VarMay
{
	public static IRoMayVar<T> MakeConst<T>() => new RoMayVarConst<T>(May.None<T>());
	public static IRoMayVar<T> MakeConst<T>(T val) => new RoMayVarConst<T>(May.Some(val));

	public static IRwMayVar<T> Make<T>(
		[CallerFilePath] string srcFile = "", [CallerLineNumber] int srcLine = 0
	)
		=> new RwMayVar<T>(May.None<T>(), false, (srcFile, srcLine).Fmt("VarMay"));
	public static IRwMayVar<T> Make<T>(
		T initVal,
		[CallerFilePath] string srcFile = "", [CallerLineNumber] int srcLine = 0
	)
		=> new RwMayVar<T>(May.Some(initVal), false, (srcFile, srcLine).Fmt("VarMay"));

	public static IFullRwMayBndVar<T> MakeBnd<T>() => new FullRwMayBndVar<T>(May.None<T>(), false, "VarMay.MakeBnd()");


	public static (IRoMayVar<T>, IDisposable) Make<T>(IObservable<Maybe<T>> obs)
	{
		var v = Make<T>();
		obs.Subscribe(v).D(v);
		return (v.ToReadOnlyMay(), v);
	}
	public static (IRoMayVar<T>, IDisposable) Make<T>(T initVal, IObservable<Maybe<T>> obs)
	{
		var v = Make(initVal);
		obs.Subscribe(v).D(v);
		return (v.ToReadOnlyMay(), v);
	}

	public static IRoMayVar<T> ToReadOnlyMay<T>(this IRwMayVar<T> v) => new RoMayVar<T>(v);

	public static IRoMayBndVar<T> ToReadOnlyMay<T>(this IFullRwMayBndVar<T> v) => new RoMayBndVar<T>(v);

	public static IRwMayBndVar<T> ToRwMayBndVar<T>(
		this IFullRwMayBndVar<T> v,
		[CallerFilePath] string srcFile = "", [CallerLineNumber] int srcLine = 0
	)
		=> new RwMayBndVar<T>(v, (srcFile, srcLine).Fmt("VarMay"));





	private static IRwMayVar<T> Make<T>(
		Maybe<T> initVal,
		[CallerFilePath] string srcFile = "", [CallerLineNumber] int srcLine = 0
	)
		=> new RwMayVar<T>(initVal, false, (srcFile, srcLine).Fmt("VarMay"));
	private static (IRoMayVar<T>, IDisposable) Make<T>(Maybe<T> initVal, IObservable<Maybe<T>> obs)
	{
		var v = Make(initVal);
		obs.Subscribe(v).D(v);
		return (v.ToReadOnlyMay(), v);
	}

	public static IRoMayVar<U> SwitchMayVar<T, U>(
		this IRoVar<T> varT,
		Func<T, IRoMayVar<U>> selFun
	) =>
		Make(
			selFun(varT.V).V,
			varT.Select(selFun).Switch()
		).D(varT);



	public static IRoMayVar<U> SwitchMayVar<T, U>(
		this IRoMayVar<T> varT,
		Func<T, IRoVar<U>> selFun
	) =>
		Make(
			varT.V.Select(t => selFun(t).V),
			varT.Select(t => t.Select(u => selFun(u).V))
		).D(varT);
}