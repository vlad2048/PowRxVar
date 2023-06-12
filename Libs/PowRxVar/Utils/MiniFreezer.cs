using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace PowRxVar.Utils;

/// <summary>
/// Keeps track of a boolean flag IsFrozen set to true on creation
/// and switched to false a short while after.
/// </summary>
public sealed class MiniFreezer : IDisposable
{
	private static readonly TimeSpan DefaultPeriod = TimeSpan.FromMilliseconds(100);

	private readonly Disp d = new();
	public void Dispose() => d.Dispose();

	private readonly ISubject<Unit> whenFrozen = new Subject<Unit>();
	private readonly ISubject<Unit> whenUnfrozen = new Subject<Unit>();

	public IObservable<Unit> WhenFrozen => whenFrozen.AsObservable();
	public IObservable<Unit> WhenUnfrozen => whenUnfrozen.AsObservable();
	public bool IsFrozen { get; private set; }

	public MiniFreezer(int? ms = null)
	{
		var period = ms switch
		{
			not null => TimeSpan.FromMilliseconds(ms.Value),
			null => DefaultPeriod
		};
		WhenFrozen.SubscribeWithDisp((_, serD) =>
		{
			IsFrozen = true;
			Obs.Timer(period)
				.Subscribe(_ =>
				{
					whenUnfrozen.OnNext(Unit.Default);
					IsFrozen = false;
				}).D(serD);
		}).D(d);
	}

	public void Freeze() => whenFrozen.OnNext(Unit.Default);
}