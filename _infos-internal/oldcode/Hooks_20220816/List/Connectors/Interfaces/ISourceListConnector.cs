namespace PowRxVar.WinForms._Internal.Hookers.List.Connectors.Interfaces;

interface ISourceListConnector
{
	void BeginBatch();
	void EndBatch();

	void Add<T>(int idx, T e);
	void Remove(int idx);
	void Move(int idxSrc, int idxDst);
	void Replace<T>(int idx, T e);
	void Refresh(int idx);

	void AddRange<T>(int? idx, IEnumerable<T> l);
	void RemoveRange(int idx, int cnt);
	void Clear();
}