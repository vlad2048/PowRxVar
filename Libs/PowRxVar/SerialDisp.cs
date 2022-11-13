using System.Reactive;

namespace PowRxVar;

public class SerialDisp<T> : IRwDispBase where T : class, IDisposable
{
	private readonly Disp d = new();
	private T? value;

	// IRwDispBase
	public IObservable<Unit> WhenDisposed => d.WhenDisposed;
	public bool IsDisposed => d.IsDisposed;
	public void Dispose() => d.Dispose();

	public T? Value
	{
		get => value;
		set
		{
			if (value == this.value) return;
			if (value != null && this.value != null)
				throw new ArgumentException("You first need to set SerialDisp.Value to null before reassigning a value to it");

			if (this.value != null)
			{
				this.value.Dispose();
				d.Clear();
				this.value = null;
			}

			this.value = value;

			if (this.value != null)
			{
				if (IsDisposed)
					this.value.Dispose();
				else
					d.Add(this.value);
			}
		}
	}
}