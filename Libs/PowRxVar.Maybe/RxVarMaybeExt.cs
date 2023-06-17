using System.Reactive;
using System.Reactive.Linq;
using PowMaybe;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

/// <summary>
/// Useful extension methods combining Vars and Maybes
/// </summary>
public static class RxVarMaybeExt
{
	public static IObservable<T> WhenSome<T>(this IObservable<Maybe<T>> mayVar) =>
		mayVar.Where(e => e.IsSome()).Select(e => e.Ensure());

	public static IObservable<Unit> WhenNone<T>(this IObservable<Maybe<T>> mayVar) =>
		mayVar.Where(e => e.IsNone()).ToUnit();

	public static IRoVar<bool> WhenVarSome<T>(this IRoMayVar<T> v) =>
		v.SelectVar(e => e.IsSome());

	public static IRoVar<bool> WhenVarNone<T>(this IRoMayVar<T> v) =>
		v.SelectVar(e => e.IsNone());


	public static IObservable<U> SelectMaySwitch<T, U>(this IObservable<Maybe<T>> obs, Func<T, IObservable<U>> fun) =>
		obs
			.Select(may => may.IsSome(out var val) switch
			{
				true => fun(val),
				false => Observable.Never<U>()
			})
			.Switch();


	public static IObservable<Maybe<U>> SelectMay<T, U>(this IObservable<Maybe<T>> obs, Func<T, U> fun) =>
		obs.Select(e => e.Select(fun));

	public static IRoMayVar<U> SelectVarMay<T, U>(this IRoMayVar<T> v, Func<T, U> fun) =>
		VarMay.Make(
			v.Select(e => e.Select(fun))
		).D(v);

	// Do we really need that ?
	public static IRoMayVar<U> SelectVarMayWithRefresh<T, U>(this IRoMayVar<T> v, Func<T, U> fun, IObservable<Unit> whenRefresh) =>
		VarMay.Make(
			Obs.Merge(
					v.WhenNone()
						.Select(_ => May.None<T>()),
					v.WhenSome()
						.Select(val =>
							whenRefresh
								.Prepend(Unit.Default)
								.Select(_ => May.Some(val))
						)
						.Switch()
				)
				.Delay(TimeSpan.Zero)
				.SelectMay(fun)
		).D(v);

	public static IRoVar<T> FailWith<T>(this IRoMayVar<T> v, T def) =>
		v.SelectVar(e => e.FailWith(def));

	/// <summary>
	/// Dispose the elements in an observable of maybes once they're replaced by something else
	/// </summary>
	public static IDisposable DisposeManyMay<T>(this IObservable<Maybe<T>> obs) where T : IDisposable =>
		obs.CombineWithPrevious()
			.Where(t => t.Item1.IsSome(out var maySlnPrev) && maySlnPrev.IsSome())
			.Select(t => t.Item1.Ensure().Ensure())
			.Subscribe(slnPrev =>
			{
				slnPrev.Dispose();
			});

	private static IObservable<(Maybe<T>, T)> CombineWithPrevious<T>(this IObservable<T> obs) =>
		obs.Scan(
				(May.None<T>(), May.None<T>()),
				(t, v) => (t.Item2, May.Some(v))
			)
			.Select(t => (t.Item1, t.Item2.Ensure()));
}