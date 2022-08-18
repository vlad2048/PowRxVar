// ReSharper disable once CheckNamespace
namespace PowRxVar;

public interface IFullRwBndVar<T> : IRwBndVar<T>
{
	void SetInner(T v);
}
