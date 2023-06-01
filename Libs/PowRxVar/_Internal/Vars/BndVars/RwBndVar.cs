using PowRxVar._Internal.Vars.Disps;

namespace PowRxVar._Internal.Vars.BndVars;

class RwBndVar<T> : RwDispBase, IRwBndVar<T>
{
	private readonly IFullRwBndVar<T> fullRwBndVar;

	public IObservable<T> WhenInner => fullRwBndVar.WhenInner;
	public IObservable<T> WhenOuter => fullRwBndVar.WhenOuter;
	public void SetOuter(T v) => fullRwBndVar.SetOuter(v);
	public T V
	{
		get => fullRwBndVar.V;
		set => SetOuter(value);
	}

	public bool DisableEqualityChecks
	{
		get => fullRwBndVar.DisableEqualityChecks;
		set => fullRwBndVar.DisableEqualityChecks = value;
	}

	public RwBndVar(IFullRwBndVar<T> fullRwBndVar, string? dbgExpr) : base(dbgExpr)
	{
		this.fullRwBndVar = fullRwBndVar;
		this.D(fullRwBndVar);
	}

	public override string ToString() => $"RwBndVar({V})";

	public IDisposable Subscribe(IObserver<T> observer) => fullRwBndVar.Subscribe(observer);

	public void OnNext(T value) => SetOuter(value);
	public void OnCompleted() => fullRwBndVar.OnCompleted();
	public void OnError(Exception error) => fullRwBndVar.OnError(error);
}