using PowMaybe;
using PowRxVar.Maybe._Internals;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class VarMay
{
	public static IRwMayVar<T> Make<T>() => new RwMayVar<T>(May.None<T>(), false, "VarMay.Make()");
	public static IFullRwMayBndVar<T> MakeBnd<T>() => new FullRwMayBndVar<T>(May.None<T>(), false, "VarMay.MakeBnd()");
}