using PowMaybe;
using PowRxVar.Maybe._Internals;
using PowRxVar.Utils.Extensions;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class VarMay
{
	public static IRwMayVar<T> Make<T>(
		[CallerFilePath] string srcFile = "", [CallerLineNumber] int srcLine = 0
	)
		=> new RwMayVar<T>(May.None<T>(), false, (srcFile, srcLine).Fmt("VarMay"));

	public static IFullRwMayBndVar<T> MakeBnd<T>() => new FullRwMayBndVar<T>(May.None<T>(), false, "VarMay.MakeBnd()");

	public static (IRoMayVar<T>, IDisposable) Make<T>(IObservable<Maybe<T>> obs)
	{
		var v = Make<T>();
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
}