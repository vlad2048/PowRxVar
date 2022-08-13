using System.Reactive.Linq;
using System.Reactive.Subjects;
using PowRxVar.Utils.Extensions;
using PowRxVar.Vars;

namespace PowRxVar._Internal.Vars;

class RwBndVar<T> : RwDispBase, IRwBndVar<T>
{
	private readonly ISubject<T> whenInner;
	private readonly ISubject<T> whenOuter;
	private readonly IRoVar<T> mixedVar;

	public IObservable<T> WhenInner => whenInner.AsObservable();
	public IObservable<T> WhenOuter => whenOuter.AsObservable();
	public void SetInner(T v)
	{
		if (Equals(v, V)) return;
		whenInner.OnNext(v);
	}
	public void SetOuter(T v)
	{
		if (Equals(v, V)) return;
		whenOuter.OnNext(v);
	}

	public T V => mixedVar.V;

	public RwBndVar(T initVal)
	{
		whenInner = new Subject<T>().D(this);
		whenOuter = new Subject<T>().D(this);
		var rwMixedVar = new RwVar<T>(initVal).D(this);
		Obs.Merge(WhenInner, WhenOuter).Subscribe(rwMixedVar).D(this);
		mixedVar = rwMixedVar.ToReadOnly();
	}

	public override string ToString() => $"RwBndVar({V})";
}