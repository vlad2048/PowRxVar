// ReSharper disable once CheckNamespace
namespace PowRxVar;

public interface IRwVar<T> : IRoVar<T>, IObserver<T>, IRwDispBase
{
	new T V { get; set; }
	bool DisableEqualityChecks { get; set; }
}