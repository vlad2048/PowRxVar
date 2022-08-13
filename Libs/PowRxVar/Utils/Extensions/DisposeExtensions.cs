using PowRxVar.Vars;

namespace PowRxVar.Utils.Extensions;

public static class DisposeExtensions
{
	/*internal static ISubject<T> D<T>(this Subject<T> dispTarget, params IRoDispBase[] dispSources)
	{
		dispSources.CombineCancels().Register(dispTarget.OnCompleted);
		return dispTarget;
	}
	internal static BehaviorSubject<T> D<T>(this BehaviorSubject<T> dispTarget, params IRoDispBase[] dispSources)
	{
		dispSources.CombineCancels().Register(dispTarget.OnCompleted);
		return dispTarget;
	}*/

	public static T D<T>(this T dispTarget, params IRoDispBase[] dispSources)
		where T : IDisposable
	{
		dispSources.CombineCancels().Register(dispTarget.Dispose);
		return dispTarget;
	}

	public static T D<T>(this (T, IDisposable) dispTargetTuple, params IRoDispBase[] dispSources)
	{
		dispSources.CombineCancels().Register(dispTargetTuple.Item2.Dispose);
		return dispTargetTuple.Item1;
	}

	private static CancellationToken CombineCancels(this IEnumerable<IRoDispBase> dispSources) =>
		CancellationTokenSource.CreateLinkedTokenSource(
			dispSources
				.Select(e => e.CancelToken).ToArray()
		).Token;
}