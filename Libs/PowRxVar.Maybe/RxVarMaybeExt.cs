using System.Reactive;
using System.Reactive.Linq;
using PowMaybe;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class RxVarMaybeExt
{
	public static IObservable<T> WhenSome<T>(this IRoVar<Maybe<T>> mayVar) =>
		mayVar.Where(e => e.IsSome()).Select(e => e.Ensure());

	public static IObservable<Unit> WhenNone<T>(this IRoVar<Maybe<T>> mayVar) =>
		mayVar.Where(e => e.IsNone()).ToUnit();

	public static IRoVar<Maybe<U>> SelectMay<T, U>(this IRoVar<Maybe<T>> v, Func<T, U> fun) =>
		v.SelectVar(e => e.Select(fun));

	public static IRoVar<T> FailWith<T>(this IRoVar<Maybe<T>> v, T def) =>
		v.SelectVar(e => e.FailWith(def));
}