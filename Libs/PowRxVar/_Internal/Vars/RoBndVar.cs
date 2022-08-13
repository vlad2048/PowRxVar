using PowRxVar.Vars;

namespace PowRxVar._Internal.Vars;

class RoBndVar<T> : IRoBndVar<T>
{
	private readonly IRwBndVar<T> rwBndVar;

	public CancellationToken CancelToken => rwBndVar.CancelToken;
	public bool IsDisposed => rwBndVar.IsDisposed;

	public T V => rwBndVar.V;

	public IObservable<T> WhenInner => rwBndVar.WhenInner;
	public IObservable<T> WhenOuter => rwBndVar.WhenOuter;

	public RoBndVar(IRwBndVar<T> rwBndVar)
	{
		this.rwBndVar = rwBndVar;
	}

	public override string ToString() => $"RoBndVar({V})";
}