// ReSharper disable once CheckNamespace
namespace PowRxVar;

public interface IRwBndVar<T> : IRwVar<T>, IRoBndVar<T>
{
	void SetOuter(T v);
}