using System.Reactive.Linq;

namespace PowRxVar;

public static class WinFormsRxExtensions
{
	public static IObservable<T> ObserveOnUIThread<T>(this IObservable<T> obs) =>
		obs.ObserveOn(SynchronizationContext.Current!);
}