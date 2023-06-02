using System.Reactive.Disposables;
using System.Reactive.Linq;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class DisposeExtensions
{
	/// <summary>
	/// Cause a target object to be disposed when any of the source objects is disposed
	/// </summary>
	/// <typeparam name="D">Target object type, need to implement IDisposable</typeparam>
	/// <param name="dispDst">Target object</param>
	/// <param name="dispSrcs">Source objects</param>
	/// <returns>Target object</returns>
	public static D D<D>(this D dispDst, params IRoDispBase[] dispSrcs)
		where D : IDisposable
	{
		dispSrcs
			.Select(e => e.WhenDisposed)
			.Merge()
			.Subscribe(_ => dispDst.Dispose());
		return dispDst;
	}

	/// <summary>
	/// Cause a target IDisposable to be disposed when any of the source objects is disposed
	/// <para>
	/// This is the safe version
	/// </para>
	/// </summary>
	public static T D<T>(this (T, IDisposable) dispDstTuple, params IRoDispBase[] dispSrcs)
	{
		dispDstTuple.Item2.D(dispSrcs);
		return dispDstTuple.Item1;
	}

	/// <summary>
	/// Cause a target IDisposable to be disposed when any of the source objects is disposed
	/// <para>
	/// This is the safe version
	/// </para>
	/// </summary>
	public static (T1, T2) D<T1, T2>(this (T1, T2, IDisposable) dispDstTuple, params IRoDispBase[] dispSrcs)
	{
		dispDstTuple.Item3.D(dispSrcs);
		return (dispDstTuple.Item1, dispDstTuple.Item2);
	}

	public static Dictionary<K, V> D<K, V>(this Dictionary<K, V> dict, IRoDispBase d)
		where K : notnull
		where V : IDisposable
	{
		Disposable.Create(() =>
		{
			foreach (var val in dict.Values)
				val.Dispose();
			dict.Clear();
		}).D(d);
		return dict;
	}
}