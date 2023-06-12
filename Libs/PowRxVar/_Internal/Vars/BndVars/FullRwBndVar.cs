using System.Reactive.Linq;
using System.Reactive.Subjects;
using PowRxVar._Internal.Vars.Disps;

namespace PowRxVar._Internal.Vars.BndVars;

sealed class FullRwBndVar<T> : RwDispBase, IFullRwBndVar<T>
{
	private readonly ISubject<T> whenInner;
	private readonly ISubject<T> whenOuter;
	private readonly IRoVar<T> mixedVar;

	public IObservable<T> WhenInner => whenInner.AsObservable();
	public IObservable<T> WhenOuter => whenOuter.AsObservable();
	public void SetInner(T v)
	{
		if (!DisableEqualityChecks && Equals(v, V)) return;
		whenInner.OnNext(v);
	}
	public void SetOuter(T v)
	{
		if (!DisableEqualityChecks && Equals(v, V)) return;
		whenOuter.OnNext(v);
	}

	public T V
	{
		get => mixedVar.V;
		set => SetOuter(value);
	}

	public bool DisableEqualityChecks { get; }

	public FullRwBndVar(T initVal, bool noCheck, string? dbgExpr) : base(dbgExpr)
	{
		DisableEqualityChecks = noCheck;
		whenInner = new Subject<T>().D(this);
		whenOuter = new Subject<T>().D(this);
		mixedVar = noCheck switch
		{
			false =>
				Var.Make(
					initVal,
					WhenInner.Merge(WhenOuter)
				).D(this),
			true =>
				Var.MakeNoCheck(
					initVal,
					WhenInner.Merge(WhenOuter)
				).D(this),
		};
	}

	public override string ToString() => $"FullRwBndVar({V})";

	public IDisposable Subscribe(IObserver<T> observer) => mixedVar.Subscribe(observer);

	public void OnNext(T value) => SetOuter(value);
	public void OnCompleted() => whenOuter.OnCompleted();
	public void OnError(Exception error) => whenOuter.OnError(error);
}