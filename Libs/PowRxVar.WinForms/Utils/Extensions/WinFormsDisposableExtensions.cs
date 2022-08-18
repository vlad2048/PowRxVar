using System.Reactive.Linq;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class WinFormsDisposableExtensions
{
	/// <summary>
	/// Cause a target object to be disposed when a source control is destroyed (HandleDestroyed event)
	/// </summary>
	/// <typeparam name="D">Target object type, need to implement IDisposable</typeparam>
	/// <param name="dispDst">Target object</param>
	/// <param name="ctrl">Source control</param>
	/// <returns>Target object</returns>
	public static D D<D>(this D dispDst, Control ctrl)
		where D : IDisposable =>
			dispDst.D(ctrl.Events().HandleDestroyed);

	/// <summary>
	/// Cause a target IDisposable to be disposed when a source control is destroyed (HandleDestroyed event)
	/// <para>
	/// This is the safe version
	/// </para>
	/// </summary>
	public static T D<T>(this (T, IDisposable) dispDstTuple, Control ctrl)
	{
		dispDstTuple.Item2.D(ctrl.Events().HandleDestroyed);
		return dispDstTuple.Item1;
	}

	/// <summary>
	/// Cause a target IDisposable to be disposed when a source control is destroyed (HandleDestroyed event)
	/// <para>
	/// This is the safe version
	/// </para>
	/// </summary>
	public static (T1, T2) D<T1, T2>(this (T1, T2, IDisposable) dispDstTuple, Control ctrl)
	{
		dispDstTuple.Item3.D(ctrl.Events().HandleDestroyed);
		return (dispDstTuple.Item1, dispDstTuple.Item2);
	}

	private static D D<D, T>(this D dispDst, IObservable<T> dispSrc)
		where D : IDisposable
	{
		dispSrc.Take(1).Subscribe(_ => dispDst.Dispose());
		return dispDst;
	}
}