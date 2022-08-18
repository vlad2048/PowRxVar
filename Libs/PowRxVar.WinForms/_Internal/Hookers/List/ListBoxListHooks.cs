using System.ComponentModel;
using System.Reactive;
using PowMaybe;
using PowRxVar.WinForms._Internal.Hookers.List._Base;

namespace PowRxVar.WinForms._Internal.Hookers.List;

class ListBoxListHooks<T> : IListHooks<T>
{
	private readonly ListBox ctrl;

	public Control Ctrl => ctrl;
	public IObservable<Unit> WhenSelectedIndexChanged { get; }

	public void SetDataSource(BindingList<T> bindingList, string? displayMember)
	{
		if (displayMember != null)
			ctrl.DisplayMember = displayMember;
		ctrl.DataSource = bindingList;
	}

	public Maybe<T> SelectedItem
	{
		get => ctrl.SelectedItem is T val
			? May.Some(val)
			: May.None<T>();
		set
		{
			if (value.IsSome(out var val))
				ctrl.SelectedItem = val;
			else
				ctrl.SelectedIndex = -1;
		}
	}

	public ListBoxListHooks(ListBox ctrl)
	{
		this.ctrl = ctrl;
		WhenSelectedIndexChanged = ctrl.Events().SelectedIndexChanged.ToUnit();
	}
}