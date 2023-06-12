using System.Reactive;
using System.Reactive.Disposables;

namespace PowRxVar._Internal.Vars.NormalVars;

sealed class RoVar<T> : IRoVar<T>
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


sealed class RoVarConst<T> : IRoVar<T>
{
	// IRoDispBase
	public IObservable<Unit> WhenDisposed => Obs.Never<Unit>();
	public bool IsDisposed => false;

	// IRoVar<T>
	public T V { get; }

	public RoVarConst(T v)
	{
		V = v;
	}

	public IDisposable Subscribe(IObserver<T> observer)
	{
		observer.OnNext(V);
		return Disposable.Empty;
	}
}