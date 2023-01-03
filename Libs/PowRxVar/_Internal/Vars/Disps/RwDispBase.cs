using System.Reactive;

namespace PowRxVar._Internal.Vars.Disps;

class RwDispBase : IRwDispBase
{
	public Disp D { get; }
	public IObservable<Unit> WhenDisposed => D.WhenDisposed;
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

	private void OnDisposing(bool isDisposing) => D.Dispose();

	~RwDispBase() => Dispose(false);

	public RwDispBase(string? dbgExpr) => D = new Disp(dbgExpr);
}