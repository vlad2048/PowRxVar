using System.Reactive;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public interface IRoDispBase
{
	IObservable<Unit> WhenDisposed { get; }
	bool IsDisposed { get; }
}