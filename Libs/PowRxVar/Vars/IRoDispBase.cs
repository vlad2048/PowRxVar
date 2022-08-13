namespace PowRxVar.Vars;

public interface IRoDispBase
{
	CancellationToken CancelToken { get; }
	bool IsDisposed { get; }
}