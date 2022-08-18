using System.ComponentModel;
using System.Reactive;
using PowMaybe;

namespace PowRxVar.WinForms._Internal.Hookers.List._Base;

interface IListHooks<T>
{
	Control Ctrl { get; }
	void SetDataSource(BindingList<T> bindingList, string? displayMember);
	IObservable<Unit> WhenSelectedIndexChanged { get; }
	Maybe<T> SelectedItem { get; set; }
}