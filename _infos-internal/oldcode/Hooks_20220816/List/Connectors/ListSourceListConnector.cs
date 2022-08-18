using System.Collections;
using PowRxVar.WinForms._Internal.Hookers.List.Connectors.Interfaces;

namespace PowRxVar.WinForms._Internal.Hookers.List.Connectors;

class ListSourceListConnector : ISourceListConnector
{
	private readonly Control ctrl;
	private readonly IList list;

	public ListSourceListConnector(Control ctrl, IList list)
	{
		this.ctrl = ctrl;
		this.list = list;
	}

	public void BeginBatch() => ctrl.SuspendLayout();
	public void EndBatch() => ctrl.ResumeLayout(true);

	public void Add<T>(int idx, T e) => list.Insert(idx, e);
	public void Remove(int idx) => list.RemoveAt(idx);
	public void Move(int idxSrc, int idxDst)
	{
		if (idxSrc == idxDst) return;
		var elt = list[idxSrc];
		list.RemoveAt(idxSrc);
		list.Insert(idxDst, elt);
	}
	public void Replace<T>(int idx, T e) => list[idx] = e;
	public void Refresh(int idx)
	{

	}

	public void AddRange<T>(int? idx, IEnumerable<T> l)
	{
		var i = idx ?? list.Count;
		foreach (var elt in l)
			list.Insert(i++, elt);
	}
	public void RemoveRange(int idx, int cnt)
	{
		for (var i = 0; i < cnt; i++)
			list.RemoveAt(idx);
	}
	public void Clear() => list.Clear();
}