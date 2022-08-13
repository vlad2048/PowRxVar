using PowRxVar.Vars;

namespace PowRxVar._Internal.Vars;

class RoVar<T> : IRoVar<T>
{
	private readonly IRwVar<T> rwVar;

	// IRoDispBase
	public CancellationToken CancelToken => rwVar.CancelToken;
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