using System.Reactive;
using System.Reactive.Linq;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class CommonRxExtensions
{
	public static IObservable<Unit> ToUnit<T>(this IObservable<T> obs) => obs.Select(_ => Unit.Default);

	public static IObservable<T> MakeHot<T>(this IObservable<T> obs, IRoDispBase d)
	{
		var pub = obs.Publish();
		pub.Connect().D(d);
		return pub;
	}
}