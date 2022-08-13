namespace PowRxVar.Vars;

public interface IRwDispBase : IRoDispBase, IDisposable
{
	CancellationTokenSource CancelSource { get; }
}