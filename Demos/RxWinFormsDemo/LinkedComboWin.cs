using DynamicData;
using PowMaybe;
using PowRxVar;
using PowRxVar.WinForms.Hookers;

namespace RxWinFormsDemo;

partial class LinkedComboWin : Form
{
	private static readonly Dictionary<string, string[]> masterSlaveMap = new()
	{
		{ "first", new[] { "1_aaa", "1_bbb", "1_ccc" } },
		{ "second", new[] { "2_ddd", "3_eee", "4_fff" } }
	};

	public LinkedComboWin()
	{
		InitializeComponent();
		var d = new Disp().D(this);

		this.Events().HandleCreated.Subscribe(_ =>
		{

			var masterChangeSet = MakeChangeSet(new[] { "first", "second" }).D(d);
			var selMaster = masterCombo.Hook(masterChangeSet);

			var slaveList = new SourceCache<string, string>(e => e);
			var slaveChangeSet = slaveList.Connect();
			var selSlave = slaveCombo.Hook(slaveChangeSet);

			selMaster
				.ObserveOnUIThread()
				.Subscribe(e => masterLabel.Text = $"{e}").D(d);
			selSlave
				.ObserveOnUIThread()
				.Subscribe(e => slaveLabel.Text = $"{e}").D(d);

			selMaster.Subscribe(mayMaster =>
			{
				var slaveItems = mayMaster.IsSome(out var master) switch
				{
					true => masterSlaveMap[master!],
					false => Array.Empty<string>()
				};

				slaveList.Edit(e =>
				{
					e.Load(slaveItems);
				});

			}).D(d);

			clearBtn.Events().Click.Subscribe(_ =>
			{
				slaveList.Clear();
			}).D(d);

			
			
			
			
			/*loadBtn.Events().Click.Subscribe(_ =>
			{
				slaveCombo.Items.Add("First");
				slaveCombo.Items.Add("Second");
				slaveCombo.Items.Add("Third");
			}).D(d);
			clearBtn.Events().Click.Subscribe(_ =>
			{
				slaveCombo.Items.Clear();
			}).D(d);

			slaveCombo.Events().SelectedIndexChanged
				.ObserveOnUIThread()
				.Subscribe(_ =>
				{
					slaveLabel.Text = $"{slaveCombo.SelectedIndex}";
				}).D(d);*/

		}).D(d);
	}


	private static (IObservable<IChangeSet<T, T>>, IDisposable) MakeChangeSet<T>(IEnumerable<T> source) where T : notnull
	{
		var d = new Disp();
		var list = new SourceCache<T, T>(e => e).D(d);
		list.Edit(e =>
		{
			e.AddOrUpdate(source);
		});
		return (list.Connect(), d);
	}
}
