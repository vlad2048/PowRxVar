using DynamicData;
using PowMaybe;
using PowRxVar;
using PowRxVar.WinForms.Hookers;
using PowRxVar.WinForms.Hookers.ListControls;

namespace RxWinFormsDemo.Wins;

partial class AutoSelectWin : Form
{
	public AutoSelectWin()
	{
		InitializeComponent();
		var d = new Disp().D(this);

		SetupList(ListSelectBehavior.None, noneList, nonePopBtn, noneClearBtn, noneUnselectBtn, noneLabel).D(d);
		//SetupList(ListSelectBehavior.AutoSelectOnce, onceList, oncePopBtn, onceClearBtn, onceUnselectBtn, onceLabel).D(d);
		//SetupList(ListSelectBehavior.AutoSelectAlways, alwaysList, alwaysPopBtn, alwaysClearBtn, alwaysUnselectBtn, alwaysLabel).D(d);
	}


	private static IDisposable SetupList(
		ListSelectBehavior behavior,
		ListBox listCtrl,
		Button popBtn,
		Button clearBtn,
		Button unselectBtn,
		Label label
	)
	{
		var d = new Disp();

		var listSource = new SourceList<string>().D(d);
		var list = listSource.Connect();

		var selItem = listCtrl.Hook(list, opt =>
		{
			opt.SelectBehavior = behavior;
		});

		popBtn.Events().Click.Subscribe(_ =>
		{
			listSource.Edit(e =>
			{
				e.Clear();
				e.AddRange(data);
			});
		}).D(d);

		clearBtn.Events().Click.Subscribe(_ =>
		{
			listSource.Clear();
		}).D(d);

		unselectBtn.Events().Click.Subscribe(_ =>
		{
			selItem.V = May.None<string>();
		}).D(d);

		selItem
			.ObserveOnUIThread()
			.Subscribe(mayItem =>
			{
				label.Text = $"{mayItem}";
			}).D(d);

		return d;
	}

	private static readonly string[] data =
	{
		"First",
		"Second",
		"Third",
		"Fourth",
		"Fifth",
	};
}
