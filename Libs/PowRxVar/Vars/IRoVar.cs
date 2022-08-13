namespace PowRxVar.Vars;

public interface IRoVar<out T> : IObservable<T>, IRoDispBase
{
	T V { get; }
}
