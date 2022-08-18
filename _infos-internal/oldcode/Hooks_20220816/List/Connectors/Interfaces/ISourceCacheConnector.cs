namespace PowRxVar.WinForms._Internal.Hookers.List.Connectors.Interfaces;

interface ISourceCacheConnector
{
	void BeginBatch();
	void EndBatch();

	void Add<V>(V e, int? idx);
	void Update<V>(V prev, V next);
	void Remove<V>(V e);

	void Refresh<V>(V e);
}