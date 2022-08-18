using System.ComponentModel;
using System.Reactive;
using BrightIdeasSoftware;
using PowMaybe;
using PowRxVar.WinForms._Internal.Hookers.List._Base;

namespace PowRxVar.WinForms._Internal.Hookers.List;

class DataListViewListHooks<T> : IListHooks<T>
{
	private readonly DataListView ctrl;

	public Control Ctrl => ctrl;
	public IObservable<Unit> WhenSelectedIndexChanged { get; }

	public void SetDataSource(BindingList<T> bindingList, string? displayMember)
	{
		if (displayMember != null)
			ctrl.DataMember = displayMember;
		ctrl.DataSource = bindingList;
	}

	public Maybe<T> SelectedItem
	{
		get => ctrl.SelectedItem.RowObject is T val
			? May.Some(val)
			: May.None<T>();
		set
		{
			if (value.IsSome(out var val))
				ctrl.SelectObject(val);
			else
				ctrl.SelectedIndex = -1;
		}
	}

	public DataListViewListHooks(DataListView ctrl)
	{
		this.ctrl = ctrl;
		WhenSelectedIndexChanged = ctrl.Events().SelectedIndexChanged.ToUnit();
	}
}