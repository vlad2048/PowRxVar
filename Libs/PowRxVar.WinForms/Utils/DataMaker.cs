using DynamicData;

namespace PowRxVar.WinForms.Utils;

public static class DataMaker
{
	public static (IObservable<IChangeSet<V, K>>, IDisposable) MakeConstantCache<V, K>(IEnumerable<V> source, Func<V, K> keyFun) where K : notnull
	{
		var d = new Disp();
		var list = new SourceCache<V, K>(keyFun).D(d);
		list.Edit(e =>
		{
			e.AddOrUpdate(source);
		});
		return (list.Connect(), d);
	}

	public static (IObservable<IChangeSet<T>>, IDisposable) MakeConstantList<T>(IEnumerable<T> source)
	{
		var d = new Disp();
		var list = new SourceList<T>().D(d);
		list.AddRange(source);
		return (list.Connect(), d);
	}
}