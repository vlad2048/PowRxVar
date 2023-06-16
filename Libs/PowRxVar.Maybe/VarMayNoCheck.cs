using PowMaybe;
using PowRxVar.Maybe._Internals;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class VarMayNoCheck
{
	public static IRwMayVar<T> Make<T>() => new RwMayVar<T>(May.None<T>(), true, "VarMay.Make()");
	public static IFullRwMayBndVar<T> MakeBnd<T>() => new FullRwMayBndVar<T>(May.None<T>(), true, "VarMay.MakeBnd()");
	public static (IRoMayVar<T>, IDisposable) Make<T>(IObservable<Maybe<T>> obs)
	{
		var v = Make<T>();
		obs.Subscribe(v).D(v);
		return (v.ToReadOnlyMay(), v);
	}
}