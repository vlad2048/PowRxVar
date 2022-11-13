using System.ComponentModel;
using System.Diagnostics;
using BrightIdeasSoftware;
using DynamicData;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using DynamicData.Binding;
using PowMaybe;
using PowRxVar.WinForms._Internal.Hookers.List;
using PowRxVar.WinForms._Internal.Hookers.List._Base;
using PowRxVar.WinForms.Hookers.ListControls;

namespace PowRxVar.WinForms.Hookers;



public static class ListControlsHookers
{
	// ***********
	// * ListBox *
	// ***********
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this ListBox ctrl, IObservable<IChangeSet<V, K>> changeSet, Action<ListHookOpt>? optFun = null) where K : notnull
	{
		var opt = ListHookOpt.Build(optFun);
		var bindingList = new BindingList<V>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new ListBoxListHooks<V>(ctrl);
		return HookInner(bindingList, hooks, opt);
	}

	public static IRwBndVar<Maybe<T>> Hook<T>(this ListBox ctrl, IObservable<IChangeSet<T>> changeSet, Action<ListHookOpt>? optFun = null)
	{
		var opt = ListHookOpt.Build(optFun);
		var bindingList = new BindingList<T>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new ListBoxListHooks<T>(ctrl);
		return HookInner(bindingList, hooks, opt);
	}


	// ************
	// * ComboBox *
	// ************
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this ComboBox ctrl, IObservable<IChangeSet<V, K>> changeSet, Action<ListHookOpt>? optFun = null) where K : notnull
	{
		var opt = ListHookOpt.Build(optFun);
		var bindingList = new BindingList<V>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new ComboBoxListHooks<V>(ctrl);
		return HookInner(bindingList, hooks, opt);
	}

	public static IRwBndVar<Maybe<T>> Hook<T>(this ComboBox ctrl, IObservable<IChangeSet<T>> changeSet, Action<ListHookOpt>? optFun = null)
	{
		var opt = ListHookOpt.Build(optFun);
		var bindingList = new BindingList<T>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new ComboBoxListHooks<T>(ctrl);
		return HookInner(bindingList, hooks, opt);
	}


	// ****************
	// * DataListView *
	// ****************
	public static IRwBndVar<Maybe<V>> Hook<V, K>(this DataListView ctrl, IObservable<IChangeSet<V, K>> changeSet, Action<ListHookOpt>? optFun = null) where K : notnull
	{
		var opt = ListHookOpt.Build(optFun);
		var bindingList = new BindingList<V>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new DataListViewListHooks<V>(ctrl);
		return HookInner(bindingList, hooks, opt);
	}

	public static IRwBndVar<Maybe<T>> Hook<T>(this DataListView ctrl, IObservable<IChangeSet<T>> changeSet, Action<ListHookOpt>? optFun = null)
	{
		var opt = ListHookOpt.Build(optFun);
		var bindingList = new BindingList<T>();
		changeSet
			.ObserveOnUIThread()
			.Bind(bindingList)
			.Subscribe().D(ctrl);
		var hooks = new DataListViewListHooks<T>(ctrl);
		return HookInner(bindingList, hooks, opt);
	}




	// ***********
	// * Private *
	// ***********
	private static IRwBndVar<Maybe<T>> HookInner<T>(
		BindingList<T> bindingList,
		IListHooks<T> hooks,
		ListHookOpt opt
	)
	{
		var uiChangeSet = bindingList.ToObservableChangeSet();
		var items = uiChangeSet.ToCollection().Select(e => e.ToArray());
		var selectedItem = HookSelectedItem(items, hooks, opt);
		hooks.SetDataSource(bindingList, opt.DisplayMember);
		return selectedItem;
	}

	private static IRwBndVar<Maybe<T>> HookSelectedItem<T>(
		IObservable<T[]> items,
		IListHooks<T> hooks,
		ListHookOpt opt
	)
	{
		var d = new Disp().D(hooks.Ctrl);

		var selectedItem = Var.MakeBnd(May.None<T>()).D(d);

		hooks.WhenSelectedIndexChanged
			.Subscribe(_ => {
				var mayVal = hooks.SelectedItem;
				Log(opt, "(UI->Code) SelectedItem(Inner) <-", mayVal);
				selectedItem.SetInner(mayVal);
			}).D(d);

		// Code -> UI
		selectedItem.WhenOuter
			.ObserveOnUIThread()
			.Subscribe(mayVal => {
				Log(opt, "(Code->UI) list.SelectedIndex <-", mayVal);
				hooks.SelectedItem = mayVal;
			}).D(d);

		return selectedItem;
	}

	private static void Log(ListHookOpt opt, string str, object? o = null)
	{
		if (!opt.EnableLogging) return;
		var oStr = o switch
		{
			not null => $"{o}",
			null => string.Empty
		};
		var s = $"[{DateTime.Now:HH:mm:ss.zzz}]-[ListHook]: {str}".PadRight(70);
		Debug.WriteLine($"{s}{oStr}");
	}
}
