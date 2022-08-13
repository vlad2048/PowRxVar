namespace PowRxVar.Vars;

public interface IRwVar<T> : IRoVar<T>, IObserver<T>, IRwDispBase
{
	new T V { get; set; }
}