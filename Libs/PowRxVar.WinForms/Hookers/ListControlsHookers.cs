using System.ComponentModel;
using BrightIdeasSoftware;
using DynamicData;
using System.Reactive.Linq;
using DynamicData.Binding;
using PowMaybe;
using PowRxVar.WinForms._Internal.Hookers.List;
using PowRxVar.WinForms._Internal.Hookers.List._Base;

namespace PowRxVar.WinForms.Hookers;

public static class ListControlsHookers
{
	// ***********
	// * ListBox *
	// ***********
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this ListBox ctrl, IObservable<IChangeSet<V, K>> changeSet, string? displayMember = null) where K : notnull
	{
		var bindingList = new BindingList<V>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new ListBoxListHooks<V>(ctrl);
		return HookInner(bindingList, hooks, displayMember);
	}

	public static IRwBndVar<Maybe<T>> Hook<T>(this ListBox ctrl, IObservable<IChangeSet<T>> changeSet, string? displayMember = null)
	{
		var bindingList = new BindingList<T>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new ListBoxListHooks<T>(ctrl);
		return HookInner(bindingList, hooks, displayMember);
	}


	// ************
	// * ComboBox *
	// ************
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this ComboBox ctrl, IObservable<IChangeSet<V, K>> changeSet, string? displayMember = null) where K : notnull
	{
		var bindingList = new BindingList<V>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new ComboBoxListHooks<V>(ctrl);
		return HookInner(bindingList, hooks, displayMember);
	}

	public static IRwBndVar<Maybe<T>> Hook<T>(this ComboBox ctrl, IObservable<IChangeSet<T>> changeSet, string? displayMember = null)
	{
		var bindingList = new BindingList<T>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new ComboBoxListHooks<T>(ctrl);
		return HookInner(bindingList, hooks, displayMember);
	}


	// ****************
	// * DataListView *
	// ****************
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this DataListView ctrl, IObservable<IChangeSet<V, K>> changeSet, string? displayMember = null) where K : notnull
	{
		var bindingList = new BindingList<V>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new DataListViewListHooks<V>(ctrl);
		return HookInner(bindingList, hooks, displayMember);
	}

	public static IRwBndVar<Maybe<T>> Hook<T>(this DataListView ctrl, IObservable<IChangeSet<T>> changeSet, string? displayMember = null)
	{
		var bindingList = new BindingList<T>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new DataListViewListHooks<T>(ctrl);
		return HookInner(bindingList, hooks, displayMember);
	}




	// ***********
	// * Private *
	// ***********
	private static IRwBndVar<Maybe<T>> HookInner<T>(
		BindingList<T> bindingList,
		IListHooks<T> hooks,
		string? displayMember
	)
	{
		var uiChangeSet = bindingList.ToObservableChangeSet();
		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());
		var selectedItem = HookSelectedItem(items, hooks);
		hooks.SetDataSource(bindingList, displayMember);
		return selectedItem;
	}

	private static IRwBndVar<Maybe<T>> HookSelectedItem<T>(
		IObservable<T[]> items,
		IListHooks<T> hooks
	)
	{
		var d = new Disp().D(hooks.Ctrl);

		var selectedItem = Var.MakeBnd(May.None<T>()).D(d);

		hooks.WhenSelectedIndexChanged
			.Subscribe(_ => {
				var mayVal = hooks.SelectedItem;
				selectedItem.SetInner(mayVal);
			}).D(d);

		// Code -> UI
		selectedItem.WhenOuter
			.ObserveOnUIThread()
			.Subscribe(mayVal => {
				hooks.SelectedItem = mayVal;
			}).D(d);

		// If Items ∌ SelectedItem => SelectedItem <- null
		items
			.Where(_items => selectedItem.V.IsSome(out var sel) && !_items.Contains(sel))
			.ObserveOnUIThread()
			.Subscribe(_ => {
				var mayVal = May.None<T>();
				selectedItem.SetInner(mayVal);
				hooks.SelectedItem = mayVal;
			}).D(d);

		// If Items ≠ Ø && SelectedItem = null => SelectedItem <- Items[0]
		var itemsArr = Array.Empty<T>();
		items.Subscribe(_items => itemsArr = _items).D(d);
		items.ToUnit().Merge(
			selectedItem.ToUnit()
		)
			.ObserveOnUIThread()
			.Where(_ => itemsArr.Any() && selectedItem.V.IsNone())
			.Subscribe(_ => {
				var mayVal = May.Some(itemsArr[0]);
				selectedItem.SetInner(mayVal);
				hooks.SelectedItem = mayVal;
			}).D(d);

		return selectedItem;
	}
}