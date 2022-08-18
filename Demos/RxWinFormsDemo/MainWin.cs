using System.Diagnostics;
using System.Reactive.Linq;
using DynamicData;
using PowMaybe;
using PowRxVar;
using PowRxVar.WinForms.Hookers;

namespace RxWinFormsDemo;

partial class MainWin : Form
{
	private static readonly Dictionary<string, string[]> masterSlaveRefMap = new()
	{
		{ "first", new[] { "1_aaa", "1_bbb", "1_ccc" } },
		{ "second", new[] { "2_ddd", "3_eee", "4_fff" } }
	};

	private enum MasterType
	{
		First,
		Second
	}

	private enum SlaveType
	{
		One_Square,
		One_Circle,
		One_Triangle,
		Two_Pim,
		Two_Pom,
		Two_Pam,
	}

	private static readonly Dictionary<MasterType, SlaveType[]> masterSlaveValMap = new()
	{
		{ MasterType.First, new[] { SlaveType.One_Square, SlaveType.One_Circle, SlaveType.One_Triangle } },
		{ MasterType.Second, new[] { SlaveType.Two_Pim, SlaveType.Two_Pom, SlaveType.Two_Pam } }
	};

	private static (IObservable<IChangeSet<T>>, IDisposable) MakeChangeSet<T>(IEnumerable<T> source)
	{
		var d = new Disp();
		var list = new SourceList<T>().D(d);
		list.AddRange(source);
		return (list.Connect(), d);
	}


	public MainWin()
	{
		InitializeComponent();
		var d = new Disp().D(this);

		this.Events().HandleCreated.Subscribe(_ =>
		{


			{
				// **************************
				// * Source Cache Reference *
				// **************************
				var masterSource = new SourceCache<string, string>(e => e);
				var masterChangeSet = masterSource.Connect();
				
				var slaveSource = new SourceCache<string, string>(e => e);
				var slaveChangeSet = slaveSource.Connect();

				var selMaster = cacheRefMasterList.Hook(masterChangeSet);
				var selSlave = cacheRefSlaveList.Hook(slaveChangeSet);

				selMaster
					.ObserveOnUIThread()
					.Subscribe(e => cacheRefMasterLabel.Text = $"{e}").D(d);
				selSlave
					.ObserveOnUIThread()
					.Subscribe(e => cacheRefSlaveLabel.Text = $"{e}").D(d);

				selMaster.Subscribe(mayMaster =>
				{
					var slaveItems = mayMaster.IsSome(out var master) switch
					{
						true => masterSlaveRefMap[master!],
						false => Array.Empty<string>()
					};

					slaveSource.Edit(e =>
					{
						e.Load(slaveItems);
					});

				}).D(d);

				cacheRefMasterLoadBtn.Events().Click.Subscribe(_ =>
				{
					Observable.Timer(TimeSpan.FromMilliseconds(500))
						.Subscribe(_ =>
						{
							masterSource.Edit(e =>
							{
								var list = masterSlaveRefMap.Keys.ToArray();
								e.Load(list);
							});
						}).D(d);
				}).D(d);

				cacheRefMasterClearBtn.Events().Click.Subscribe(_ =>
				{
					masterSource.Clear();
				}).D(d);

				cacheRefSlaveClearBtn.Events().Click.Subscribe(_ =>
				{
					slaveSource.Clear();
				}).D(d);
			}




			{
				// *************************
				// * Source List Reference *
				// *************************
				var masterSource = new SourceList<string>();
				var masterChangeSet = masterSource.Connect();
				
				var slaveSource = new SourceList<string>();
				var slaveChangeSet = slaveSource.Connect();

				var selMaster = listRefMasterList.Hook(masterChangeSet);
				var selSlave = listRefSlaveList.Hook(slaveChangeSet);

				selMaster
					.ObserveOnUIThread()
					.Subscribe(e => listRefMasterLabel.Text = $"{e}").D(d);
				selSlave
					.ObserveOnUIThread()
					.Subscribe(e => listRefSlaveLabel.Text = $"{e}").D(d);

				selMaster.Subscribe(mayMaster =>
				{
					var slaveItems = mayMaster.IsSome(out var master) switch
					{
						true => masterSlaveRefMap[master!],
						false => Array.Empty<string>()
					};

					slaveSource.Edit(e =>
					{
						e.Clear();
						e.AddRange(slaveItems);
					});

				}).D(d);

				listRefMasterLoadBtn.Events().Click.Subscribe(_ =>
				{
					Observable.Timer(TimeSpan.FromMilliseconds(500))
						.Subscribe(_ =>
						{
							masterSource.Edit(e =>
							{
								var list = masterSlaveRefMap.Keys.ToArray();
								e.Clear();
								e.AddRange(list);
							});
						}).D(d);
				}).D(d);

				listRefMasterClearBtn.Events().Click.Subscribe(_ =>
				{
					masterSource.Clear();
				}).D(d);

				listRefSlaveClearBtn.Events().Click.Subscribe(_ =>
				{
					slaveSource.Clear();
				}).D(d);
			}







			{
				// **********************
				// * Source Cache Value *
				// **********************
				var masterSource = new SourceCache<MasterType, MasterType>(e => e);
				var masterChangeSet = masterSource.Connect();
				
				var slaveSource = new SourceCache<SlaveType, SlaveType>(e => e);
				var slaveChangeSet = slaveSource.Connect();

				var selMaster = cacheValMasterList.Hook(masterChangeSet);
				var selSlave = cacheValSlaveList.Hook(slaveChangeSet);

				selMaster
					.ObserveOnUIThread()
					.Subscribe(e => cacheValMasterLabel.Text = $"{e}").D(d);
				selSlave
					.ObserveOnUIThread()
					.Subscribe(e => cacheValSlaveLabel.Text = $"{e}").D(d);

				selMaster.Subscribe(mayMaster =>
				{
					var slaveItems = mayMaster.IsSome(out var master) switch
					{
						true => masterSlaveValMap[master!],
						false => Array.Empty<SlaveType>()
					};

					slaveSource.Edit(e =>
					{
						e.Load(slaveItems);
					});

				}).D(d);

				cacheValMasterLoadBtn.Events().Click.Subscribe(_ =>
				{
					Observable.Timer(TimeSpan.FromMilliseconds(500))
						.Subscribe(_ =>
						{
							masterSource.Edit(e =>
							{
								var list = masterSlaveValMap.Keys.ToArray();
								e.Load(list);
							});
						}).D(d);
				}).D(d);

				cacheValMasterClearBtn.Events().Click.Subscribe(_ =>
				{
					masterSource.Clear();
				}).D(d);

				cacheValSlaveClearBtn.Events().Click.Subscribe(_ =>
				{
					slaveSource.Clear();
				}).D(d);
			}




			{
				// *********************
				// * Source List Value *
				// *********************
				var masterSource = new SourceList<MasterType>();
				var masterChangeSet = masterSource.Connect();
				
				var slaveSource = new SourceList<SlaveType>();
				var slaveChangeSet = slaveSource.Connect();

				var selMaster = listValMasterList.Hook(masterChangeSet);
				var selSlave = listValSlaveList.Hook(slaveChangeSet);

				selMaster
					.ObserveOnUIThread()
					.Subscribe(e => listValMasterLabel.Text = $"{e}").D(d);
				selSlave
					.ObserveOnUIThread()
					.Subscribe(e => listValSlaveLabel.Text = $"{e}").D(d);

				selMaster.Subscribe(mayMaster =>
				{
					var slaveItems = mayMaster.IsSome(out var master) switch
					{
						true => masterSlaveValMap[master!],
						false => Array.Empty<SlaveType>()
					};

					slaveSource.Edit(e =>
					{
						e.Clear();
						e.AddRange(slaveItems);
					});

				}).D(d);

				listValMasterLoadBtn.Events().Click.Subscribe(_ =>
				{
					Observable.Timer(TimeSpan.FromMilliseconds(500))
						.Subscribe(_ =>
						{
							masterSource.Edit(e =>
							{
								var list = masterSlaveValMap.Keys.ToArray();
								e.Clear();
								e.AddRange(list);
							});
						}).D(d);
				}).D(d);

				listValMasterClearBtn.Events().Click.Subscribe(_ =>
				{
					masterSource.Clear();
				}).D(d);

				listValSlaveClearBtn.Events().Click.Subscribe(_ =>
				{
					slaveSource.Clear();
				}).D(d);
			}


		}).D(d);
	}
}
