using System.Reactive;
using System.Reactive.Linq;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class CommonRxExtensions
{
	public static IObservable<Unit> ToUnit<T>(this IObservable<T> obs) => obs.Select(_ => Unit.Default);
}