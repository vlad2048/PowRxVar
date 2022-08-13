namespace PowRxVar.Vars;

public interface IRoBndVar<out T> : IRoDispBase
{
	IObservable<T> WhenInner { get; }
	IObservable<T> WhenOuter { get; }
	T V { get; }
}