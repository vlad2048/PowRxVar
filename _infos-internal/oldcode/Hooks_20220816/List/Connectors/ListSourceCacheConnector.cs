using System.Collections;
using PowRxVar.WinForms._Internal.Hookers.List.Connectors.Interfaces;

namespace PowRxVar.WinForms._Internal.Hookers.List.Connectors;

class ListSourceCacheConnector : ISourceCacheConnector
{
	private readonly Control ctrl;
	private readonly IList list;

	public ListSourceCacheConnector(Control ctrl, IList list)
	{
		this.ctrl = ctrl;
		this.list = list;
	}

	public void BeginBatch() => ctrl.SuspendLayout();
	public void EndBatch() => ctrl.ResumeLayout(true);

	public void Add<V>(V e, int? idx)
	{
		if (idx.HasValue)
			list.Insert(idx.Value, e);
		else
			list.Add(e);
	}

	public void Update<V>(V prev, V next)
	{
		var keyIndex = GetIndexForValue(prev);
		list[keyIndex] = next;
	}

	public void Remove<V>(V e)
	{
		var keyIndex = GetIndexForValue(e);
		list.RemoveAt(keyIndex);
	}

	public void Refresh<V>(V e)
	{
	}

	private int GetIndexForValue<V>(V e)
	{
		for (var i = 0; i < list.Count; i++)
		{
			var val = (V)list[i]!;
			if (Equals(val, e))
				return i;
		}
		throw new ArgumentException();
	}
}