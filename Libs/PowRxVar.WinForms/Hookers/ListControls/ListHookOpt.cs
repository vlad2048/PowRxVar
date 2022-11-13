namespace PowRxVar.WinForms.Hookers.ListControls;

public enum ListSelectBehavior
{
	/// <summary>
	/// Only user actions can change the selected item
	/// </summary>
	None,

	/// <summary>
	/// The first time the list is populated, the selected item will be automatically set to the first element (default)
	/// </summary>
	AutoSelectOnce,

	/// <summary>
	/// When elements are added, if the selected item is pointing to nothing then always automatically select the first element
	/// </summary>
	AutoSelectAlways,
}

public class ListHookOpt
{
	public ListSelectBehavior SelectBehavior { get; set; } = ListSelectBehavior.AutoSelectOnce;
	public string? DisplayMember { get; set; }
	public bool EnableLogging { get; set; }

	internal static ListHookOpt Build(Action<ListHookOpt>? action)
	{
		var opt = new ListHookOpt();
		action?.Invoke(opt);
		return opt;
	}
}