// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class SubscribeRxExtensions
{
	/// <summary>
	/// Subscribe to an observable with a disposable for each time the callback is called
	/// <para/>
	/// This disposable is guaranteed to be disposed before each callback invocation
	/// </summary>
	/// <typeparam name="T">Observable type</typeparam>
	/// <param name="obs">Observable</param>
	/// <param name="action">Callback</param>
	/// <returns></returns>
	public static IDisposable SubscribeWithDisp<T>(
		this IObservable<T> obs,
		Action<T, IRoDispBase> action
	)
	{
		var d = new Disp();
		var serD = new SerialDisp<Disp>().D(d);
		obs.Subscribe(val =>
		{
			serD.Value = null;
			if (val == null) return;

			var innerD = new Disp();
			action(val, innerD);
			serD.Value = innerD;
		}).D(d);
		return d;
	}
}