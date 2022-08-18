// ReSharper disable once CheckNamespace
namespace PowRxVar;

public interface IRoBndVar<out T> : IRoVar<T>
{
	IObservable<T> WhenInner { get; }
	IObservable<T> WhenOuter { get; }
}