// ReSharper disable once CheckNamespace
namespace PowRxVar;

public interface IRoVar<out T> : IObservable<T>, IRoDispBase
{
	T V { get; }
}
