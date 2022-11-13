using System.Reactive.Subjects;
using PowRxVar._Internal.Vars.Disps;

namespace PowRxVar._Internal.Vars.NormalVars;

class RwVar<T> : RwDispBase, IRwVar<T>
{
	private readonly BehaviorSubject<T> subj;

	public T V { get => subj.Value; set => OnNext(value); }

	public RwVar(T initVal)
	{
		subj = new BehaviorSubject<T>(initVal).D(this);
	}

	/*

	// ****************
	// * Safe Version *
	// ****************

	private T cachedVal;

	public T V
	{
		get => IsDisposed switch
		{
			false => subj.Value,
			true => cachedVal
		};
		set
		{
			if (IsDisposed) return;
			cachedVal = value;
			OnNext(value);
		}
	}

	public RwVar(T initVal)
	{
		cachedVal = initVal;
		subj = new BehaviorSubject<T>(initVal).D(this);
	}

	*/

	public override string ToString() => $"RwVar({V})";

	public IDisposable Subscribe(IObserver<T> observer) => subj.Subscribe(observer);

	public void OnNext(T value)
	{
		if (Equals(value, V)) return;
		subj.OnNext(value);
	}
	public void OnCompleted() => subj.OnCompleted();
	public void OnError(Exception error) => subj.OnError(error);
}