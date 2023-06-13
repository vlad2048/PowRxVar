using System.Reactive.Linq;
using PowMaybe;
using PowRxVar.Maybe._Internals;
using System.Runtime.CompilerServices;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class VarMay
{
	public static IRwMayVar<T> Make<T>() => new RwMayVar<T>(May.None<T>(), false, "VarMay.Make()");
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
		[CallerArgumentExpression(nameof(v))] string? vExpr = null
	)
		=> new RwMayBndVar<T>(v, $"ToRwMayBndVar({vExpr})");
}