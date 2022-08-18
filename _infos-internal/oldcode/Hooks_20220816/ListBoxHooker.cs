using System.ComponentModel;
using System.Diagnostics;
using System.Reactive;
using BrightIdeasSoftware;
using DynamicData;
using System.Reactive.Linq;
using DynamicData.Binding;
using PowMaybe;
using PowRxVar.WinForms._Internal.Hookers.List;
using PowRxVar.WinForms._Internal.Hookers.List.Connectors;

namespace PowRxVar.WinForms.Hookers.List;

public static class ListBoxHooker
{
	// ***********
	// * ListBox *
	// ***********
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this ListBox ctrl, IObservable<IChangeSet<V, K>> changeSet) where K : notnull
	{
		var bindingList = new BindingList<V>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);

		return ctrl.HookInner(bindingList);
	}
	public static IRwBndVar<Maybe<T>> Hook<T>(this ListBox ctrl, IObservable<IChangeSet<T>> changeSet)
	{
		var bindingList = new BindingList<T>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);

		return ctrl.HookInner(bindingList);
	}


	private static IRwBndVar<Maybe<T>> HookInner<T>(this ListBox ctrl, BindingList<T> bindingList)
	{
		var uiChangeSet = bindingList.ToObservableChangeSet();
		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());

		var selectedItem = HookSelectedItem(
			items,
			ctrl.Events().SelectedIndexChanged.ToUnit(),
			() => ctrl.SelectedItem,
			e =>
			{
				//ctrl.SelectedItem = e.ToNullable();
				if (e.IsSome(out var val))
					ctrl.SelectedItem = val;
				else
					ctrl.SelectedIndex = -1;
			}).D(ctrl);

		ctrl.DataSource = bindingList;

		return selectedItem;
	}

	/*public static IRwBndVar<Maybe<V>> Hook<V, K>(this ListBox ctrl, IObservable<IChangeSet<V, K>> changeSet) where K : notnull where V : class
	{
		var conn = new ListSourceCacheConnector(ctrl, ctrl.Items);
		var uiChangeSet = HookingListLogic.HookItems(changeSet, conn).D(ctrl);
		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());
		return HookSelectedItem(
			items,
			ctrl.Events().SelectedIndexChanged.ToUnit(),
			() => ctrl.SelectedItem,
			e => ctrl.SelectedItem = e.ToNullable()
		).D(ctrl);
	}

	public static IRwBndVar<Maybe<T>> Hook<T>(this ListBox ctrl, IObservable<IChangeSet<T>> changeSet)
	{
		var conn = new ListSourceListConnector(ctrl, ctrl.Items);
		var uiChangeSet = HookingListLogic.HookItems(changeSet, conn).D(ctrl);
		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());
		return HookSelectedItem(
			items,
			ctrl.Events().SelectedIndexChanged.ToUnit(),
			() => ctrl.SelectedItem,
			e => ctrl.SelectedItem = e.FailWith(default)
		).D(ctrl);
	}*/

	/*public static IRwBndVar<Maybe<T>> Hook<T>(this ListBox ctrl, IObservable<IChangeSet<T>> changeSet)
	{
		ctrl.HookItems(changeSet);
		return ctrl
			.HookSelectedItem(
				changeSet.ToCollection().Select(e => e.ToArray())
			).D(ctrl);
	}
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this ListBox ctrl, IObservable<IChangeSet<V, K>> changeSet) where K : notnull
	{
		ctrl.HookItems(changeSet);
		return ctrl
			.HookSelectedItem(
				changeSet.ToCollection().Select(e => e.ToArray())
			).D(ctrl);
	}

	public static IRwBndVar<Maybe<T>> HookSelectedItem<T>(this ListBox ctrl) => ctrl.HookSelectedItem<T>(null);
	private static IRwBndVar<Maybe<T>> HookSelectedItem<T>(this ListBox ctrl, IObservable<T[]>? whenItemsChange) =>
		HookSelectedItem(
			() => ctrl.SelectedItem,
			e => ctrl.SelectedItem = e,
			() => ctrl.SelectedIndex = -1,
			ctrl.Events().SelectedIndexChanged.ToUnit(),
			whenItemsChange
		).D(ctrl);

	public static void HookItems<T>(this ListBox ctrl, IObservable<IChangeSet<T>> changeSet)
	{
		var conn = new ListSourceListConnector(ctrl, ctrl.Items);
		HookingListLogic.HookItems(changeSet, conn).D(ctrl);
	}

	public static void HookItems<V, K>(this ListBox ctrl, IObservable<IChangeSet<V, K>> changeSet) where K : notnull
	{
		var conn = new ListSourceCacheConnector(ctrl, ctrl.Items);
		HookingListLogic.HookItems(changeSet, conn).D(ctrl);
	}*/



	// ************
	// * ComboBox *
	// ************
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this ComboBox ctrl, IObservable<IChangeSet<V, K>> changeSet) where K : notnull where V : class
	{
		var conn = new ListSourceCacheConnector(ctrl, ctrl.Items);
		var uiChangeSet = HookingListLogic.HookItems(changeSet, conn).D(ctrl);
		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());
		return HookSelectedItem(
			items,
			ctrl.Events().SelectedIndexChanged.ToUnit(),
			() => ctrl.SelectedItem,
			e => ctrl.SelectedItem = e.ToNullable()
		).D(ctrl);
	}

	public static IRwBndVar<Maybe<T>> Hook<T>(this ComboBox ctrl, IObservable<IChangeSet<T>> changeSet)
	{
		var conn = new ListSourceListConnector(ctrl, ctrl.Items);
		var uiChangeSet = HookingListLogic.HookItems(changeSet, conn).D(ctrl);
		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());
		return HookSelectedItem(
			items,
			ctrl.Events().SelectedIndexChanged.ToUnit(),
			() => ctrl.SelectedItem,
			e => ctrl.SelectedItem = e.FailWith(default)
		).D(ctrl);
	}




	// ******************
	// * ObjectListView *
	// ******************
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this ObjectListView ctrl, IObservable<IChangeSet<V, K>> changeSet) where K : notnull where V : class
	{
		var conn = new ListSourceCacheConnector(ctrl, ctrl.Items);
		var uiChangeSet = HookingListLogic.HookItems(changeSet, conn).D(ctrl);
		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());
		return HookSelectedItem(
			items,
			ctrl.Events().SelectedIndexChanged.ToUnit(),
			() => ctrl.SelectedItem.RowObject,
			e => ctrl.SelectObject(e.FailWith(default))
		).D(ctrl);
	}

	public static IRwBndVar<Maybe<T>> Hook<T>(this ObjectListView ctrl, IObservable<IChangeSet<T>> changeSet)
	{
		var conn = new ListSourceListConnector(ctrl, ctrl.Items);
		var uiChangeSet = HookingListLogic.HookItems(changeSet, conn).D(ctrl);
		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());
		return HookSelectedItem(
			items,
			ctrl.Events().SelectedIndexChanged.ToUnit(),
			() => ctrl.SelectedItem.RowObject,
			e => ctrl.SelectObject(e.FailWith(default))
		).D(ctrl);
	}
	/*public static IRwBndVar<Maybe<T>> Hook<T>(this ObjectListView ctrl, IObservable<IChangeSet<T>> changeSet)
	{
		ctrl.HookItems(changeSet);
		return ctrl
			.HookSelectedItem(
				changeSet.ToCollection().Select(e => e.ToArray())
			).D(ctrl);
	}
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this ObjectListView ctrl, IObservable<IChangeSet<V, K>> changeSet) where K : notnull
	{
		ctrl.HookItems(changeSet);
		return ctrl
			.HookSelectedItem(
				changeSet.ToCollection().Select(e => e.ToArray())
			).D(ctrl);
	}

	public static IRwBndVar<Maybe<T>> HookSelectedItem<T>(this ObjectListView ctrl) => ctrl.HookSelectedItem<T>(null);
	private static IRwBndVar<Maybe<T>> HookSelectedItem<T>(this ObjectListView ctrl, IObservable<T[]>? whenItemsChange) =>
		HookSelectedItem(
			() => ctrl.SelectedItem?.RowObject,
			ctrl.SelectObject,
			() => ctrl.SelectedIndex = -1,
			ctrl.Events().SelectedIndexChanged.ToUnit(),
			whenItemsChange
		).D(ctrl);

	public static void HookItems<T>(this ObjectListView ctrl, IObservable<IChangeSet<T>> changeSet)
	{
		var conn = new ListSourceListConnector(ctrl, ctrl.Items);
		HookingListLogic.HookItems(changeSet, conn).D(ctrl);
	}

	public static void HookItems<V, K>(this ObjectListView ctrl, IObservable<IChangeSet<V, K>> changeSet) where K : notnull
	{
		var conn = new ListSourceCacheConnector(ctrl, ctrl.Items);
		HookingListLogic.HookItems(changeSet, conn).D(ctrl);
	}*/





	// ***********
	// * Private *
	// ***********
	private static (IRwBndVar<Maybe<T>>, IDisposable) HookSelectedItem<T>(
		IObservable<T[]> items,
		IObservable<Unit> whenSelectedIndexChanged,
		Func<object> getFun,
		Action<Maybe<T>> setFun
	)
	{
		var d = new Disp();

		var selectedItem = Var.MakeBnd(May.None<T>()).D(d);

		whenSelectedIndexChanged
			.Subscribe(_ =>
			{
				var obj = getFun();
				var mayVal = obj switch
				{
					null => May.None<T>(),
					not null => May.Some((T)obj)
				};
				selectedItem.SetInner(mayVal);
			}).D(d);

		// Code -> UI
		selectedItem.WhenOuter
			.ObserveOnUIThread()
			.Subscribe(mayVal =>
			{
				setFun(mayVal);
			}).D(d);

		// If Items ∌ SelectedItem => SelectedItem <- null
		items
			.Where(_items => selectedItem.V.IsSome(out var sel) && !_items.Contains(sel))
			.ObserveOnUIThread()
			.Subscribe(_ =>
			{
				var mayVal = May.None<T>();
				selectedItem.SetInner(mayVal);
				setFun(mayVal);
			}).D(d);

		// If Items ≠ Ø && SelectedItem = null => SelectedItem <- Items[0]
		var itemsArr = Array.Empty<T>();
		items.Subscribe(_items => itemsArr = _items).D(d);
		Obs.Merge(
			items.ToUnit(),
			selectedItem.ToUnit()
		)
			.ObserveOnUIThread()
			.Where(_ => itemsArr.Any() && selectedItem.V.IsNone())
			.Subscribe(_items =>
			{
				var mayVal = May.Some(itemsArr[0]);
				selectedItem.SetInner(mayVal);
				setFun(mayVal);
			}).D(d);

		/*items
			.Where(_items => _items.Any() && selectedItem.V.IsNone())
			.ObserveOnUIThread()
			.Subscribe(_items =>
			{
				var mayVal = May.Some(_items[0]);
				selectedItem.SetInner(mayVal);
				setFun(mayVal);
			}).D(d);*/

		return (selectedItem, d);
	}




	/*private static (IRwBndVar<Maybe<T>>, IDisposable) HookSelectedItem<T>(
		IObservable<IChangeSet<T>> uiChangeSet,
		IObservable<Unit> whenSelectedIndexChanged,
		Func<object> getFun,
		Action<Maybe<T>> setFun
	)
	{
		var d = new Disp();

		var selectedItem = Var.MakeBnd(May.None<T>()).D(d);

		whenSelectedIndexChanged
			.Subscribe(_ =>
			{
				var obj = getFun();
				var mayVal = obj switch
				{
					null => May.None<T>(),
					not null => May.Some((T)obj)
				};
				selectedItem.SetInner(mayVal);
			}).D(d);

		// Code -> UI
		selectedItem.WhenOuter
			.ObserveOnUIThread()
			.Subscribe(mayVal =>
			{
				setFun(mayVal);
			}).D(d);

		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());

		// If Items ∌ SelectedItem => SelectedItem <- null
		items
			.Where(_items => selectedItem.V.IsSome(out var sel) && !_items.Contains(sel))
			.Subscribe(_ =>
			{
				var mayVal = May.None<T>();
				selectedItem.SetInner(mayVal);
				setFun(mayVal);
			}).D(d);

		// If Items ≠ Ø && SelectedItem = null => SelectedItem <- Items[0]
		items
			.Where(_items => _items.Any() && selectedItem.V.IsNone())
			.Subscribe(_items =>
			{
				var mayVal = May.Some(_items[0]);
				selectedItem.SetInner(mayVal);
				setFun(mayVal);
			}).D(d);

		return (selectedItem, d);
	}*/




	/*private static (IRwBndVar<Maybe<T>>, IDisposable) HookSelectedItem<T>(
		Func<object?> getter,
		Action<object?> setter,
		Action setIndexToMinusOne,
		IObservable<Unit> whenIndexChanged,
		IObservable<T[]>? whenItemsChanged
	)
	{
		var d = new Disp();

		var selectedItem = Var.MakeBnd(May.None<T>()).D(d);


		void MaySetter(Maybe<T> mayVal)
		{
			switch (mayVal.IsSome(out var val))
			{
				case true:
					setter(val);
					break;

				case false:
					setIndexToMinusOne();
					break;
			}
		}

		// UI -> Code
		whenIndexChanged
			.Subscribe(_ =>
			{
				var obj = getter();
				var mayVal = obj switch
				{
					null => May.None<T>(),
					not null => May.Some((T)obj)
				};
				selectedItem.SetInner(mayVal);
			}).D(d);

		// Code -> UI
		selectedItem.WhenOuter
			.ObserveOnUIThread()
			.Subscribe(mayVal =>
			{
				MaySetter(mayVal);
			}).D(d);

		if (whenItemsChanged != null)
		{
			//var items = Array.Empty<T>();
			//whenItemsChanged.Subscribe(e => items = e).D(d);

			// If Items ∌ SelectedItem => SelectedItem <- null
			whenItemsChanged
				.Where(items => selectedItem.V.IsSome(out var sel) && !items.Contains(sel))
				.Subscribe(_ =>
				{
					var mayVal = May.None<T>();
					selectedItem.SetInner(mayVal);
					MaySetter(mayVal);
				}).D(d);

			// If Items ≠ Ø && SelectedItem = null => SelectedItem <- Items[0]
			whenItemsChanged
				.Where(items => items.Any() && selectedItem.V.IsNone())
				.Subscribe(items =>
				{
					var mayVal = May.Some(items[0]);
					selectedItem.SetInner(mayVal);
					MaySetter(mayVal);
				}).D(d);
		}

		return (selectedItem, d);
	}*/
}