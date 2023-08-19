using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace PowRxVar.Utils;

public static class RxEventMaker
{
	/// <summary>
	/// Create a signaller + observer
	/// </summary>
	/// <typeparam name="T">Event type</typeparam>
	/// <returns>(signaller, observer, IDisposable)</returns>
	public static (Action<T>, IObservable<T>, IDisposable) Make<T>()
	{
		var d = new Disp();
		ISubject<T> evtSig = new Subject<T>().D(d);
		var evtObs = evtSig.AsObservable();
		return (evtSig.OnNext, evtObs, d);
	}
}