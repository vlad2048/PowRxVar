using System.Reactive.Subjects;
using PowRxVar._Internal.Vars.Disps;

namespace PowRxVar._Internal.Vars.NormalVars;

class RwVar<T> : RwDispBase, IRwVar<T>
{
	private readonly BehaviorSubject<T> subj;

	public T V { get => subj.Value; set => OnNext(value); }

	public bool DisableEqualityChecks { get; set; }

	public RwVar(T initVal, string? dbgExpr) : base(dbgExpr)
	{
		subj = new BehaviorSubject<T>(initVal).D(this);
	}

	public IDisposable Subscribe(IObserver<T> observer) => subj.Subscribe(observer);

	public void OnNext(T value)
	{
		if (!DisableEqualityChecks && Equals(value, V)) return;
		subj.OnNext(value);
	}
	public void OnCompleted() => subj.OnCompleted();
	public void OnError(Exception error) => subj.OnError(error);
}