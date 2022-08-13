using System.Reactive.Subjects;
using PowRxVar.Utils.Extensions;
using PowRxVar.Vars;

namespace PowRxVar._Internal.Vars;

class RwVar<T> : RwDispBase, IRwVar<T>
{
	private readonly BehaviorSubject<T> subj;

	public T V { get => subj.Value; set => OnNext(value); }

	public RwVar(T initVal)
	{
		subj = new BehaviorSubject<T>(initVal).D(this);
	}

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