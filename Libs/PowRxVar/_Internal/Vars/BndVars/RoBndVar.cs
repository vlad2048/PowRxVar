using System.Reactive;

namespace PowRxVar._Internal.Vars.BndVars;

class RoBndVar<T> : IRoBndVar<T>
{
	private readonly IFullRwBndVar<T> fullRwBndVar;

	public IObservable<Unit> WhenDisposed => fullRwBndVar.WhenDisposed;
	public bool IsDisposed => fullRwBndVar.IsDisposed;

	public T V => fullRwBndVar.V;

	public IObservable<T> WhenInner => fullRwBndVar.WhenInner;
	public IObservable<T> WhenOuter => fullRwBndVar.WhenOuter;

	public RoBndVar(IFullRwBndVar<T> fullRwBndVar)
	{
		this.fullRwBndVar = fullRwBndVar;
	}

	public override string ToString() => $"RoBndVar({V})";

	public IDisposable Subscribe(IObserver<T> observer) => fullRwBndVar.Subscribe(observer);
}