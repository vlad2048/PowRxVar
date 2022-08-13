using PowRxVar.Vars;

namespace PowRxVar._Internal.Vars;

class RwDispBase : IRwDispBase
{
	private CancellationTokenSource? cancelSource = new();

	public CancellationTokenSource CancelSource => cancelSource!;
	public CancellationToken CancelToken => CancelSource.Token;
	public bool IsDisposed { get; private set; }

	public void Dispose()
	{
		if (IsDisposed) return;
		Dispose(true);
		GC.SuppressFinalize(this);
	}

	private void Dispose(bool isDisposing)
	{
		if (IsDisposed) return;
		OnDisposing(isDisposing);
		IsDisposed = true;
	}

	private void OnDisposing(bool isDisposing)
	{
		if (cancelSource == null) return;
		cancelSource.Cancel();
		cancelSource = null;
	}

	~RwDispBase() => Dispose(false);
}