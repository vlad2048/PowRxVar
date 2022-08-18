using DynamicData;
using PowRxVar.WinForms._Internal.Hookers.List.Connectors.Interfaces;

namespace PowRxVar.WinForms._Internal.Hookers.List;

static class HookingListLogic
{
	public static (IObservable<IChangeSet<V, K>>, IDisposable) HookItems<V, K>(
		IObservable<IChangeSet<V, K>> changeSet,
		ISourceCacheConnector conn
	)
		where K : notnull
	{
		var uiChangeSet = changeSet.ObserveOnUIThread();
		var d = uiChangeSet
			.Subscribe(cs =>
			{
				var requireBatch = cs.Count > 1;
				if (requireBatch) conn.BeginBatch();

				foreach (var c in cs)
				{
					switch (c.Reason)
					{
						case ChangeReason.Add:
						{
							int? idx = c.CurrentIndex != -1 ? c.CurrentIndex : null;
							conn.Add(c.Current, idx);
							break;
						}
						case ChangeReason.Update:
						{
							conn.Update(c.Previous.Value, c.Current);
							break;
						}
						case ChangeReason.Remove:
						{
							conn.Remove(c.Current);
							break;
						}

						case ChangeReason.Refresh:
						{
							conn.Refresh(c.Current);
							break;
						}

						case ChangeReason.Moved:
							throw new NotImplementedException();

						default:
							throw new ArgumentException();
					}
				}

				if (requireBatch) conn.EndBatch();
			});

		return (uiChangeSet, d);
	}


	public static (IObservable<IChangeSet<T>>, IDisposable) HookItems<T>(
		IObservable<IChangeSet<T>> changeSet,
		ISourceListConnector conn
	)
	{
		var uiChangeSet = changeSet.ObserveOnUIThread();

		var d = uiChangeSet
			.Subscribe(cs =>
			{
				var requireBatch = cs.TotalChanges > 1;
				if (requireBatch) conn.BeginBatch();

				foreach (var c in cs)
				{
					var item = c.Item;
					var range = c.Range;
					switch (c.Reason)
					{
						case ListChangeReason.Add:
							conn.Add(item.CurrentIndex, item.Current);
							break;
						case ListChangeReason.Remove:
							conn.Remove(item.CurrentIndex);
							break;
						case ListChangeReason.Replace:
							conn.Replace(item.PreviousIndex, item.Current);
							break;
						case ListChangeReason.Moved:
							conn.Move(item.PreviousIndex, item.CurrentIndex);
							break;
						case ListChangeReason.Refresh:
							conn.Refresh(item.CurrentIndex);
							break;

						case ListChangeReason.Clear:
							conn.Clear();
							break;
						case ListChangeReason.AddRange:
							conn.AddRange(range.Index == -1 ? null : range.Index, range);
							break;
						case ListChangeReason.RemoveRange:
							conn.RemoveRange(range.Index, range.Count);
							break;

						default:
							throw new ArgumentException();
					}
				}

				if (requireBatch) conn.EndBatch();
			});

		return (uiChangeSet, d);
	}
}