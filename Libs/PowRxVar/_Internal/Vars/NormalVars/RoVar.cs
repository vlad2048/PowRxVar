using System.Reactive;

namespace PowRxVar._Internal.Vars.NormalVars;

class RoVar<T> : IRoVar<T>
{
	private readonly IRwVar<T> rwVar;

	// IRoDispBase
	public IObservable<Unit> WhenDisposed => rwVar.WhenDisposed;
	public bool IsDisposed => rwVar.IsDisposed;
	// IRoVar<T>
	public T V => rwVar.V;

	public RoVar(IRwVar<T> rwVar)
	{
		this.rwVar = rwVar;
	}

	public override string ToString() => $"RoVar({V})";

	public IDisposable Subscribe(IObserver<T> observer) => rwVar.Subscribe(observer);
}