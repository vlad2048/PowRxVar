// ReSharper disable once CheckNamespace
namespace PowRxVar;

public static class RxVarPipingExtensions
{
	public static void PipeTo<T>(this IRoVar<T> vSrc, IRwVar<T> vDst) =>
		vSrc.Subscribe(e => vDst.V = e).D(vSrc, vDst);

	public static void PipeToInner<T>(this IRoVar<T> vSrc, IFullRwBndVar<T> vDst) =>
		vSrc.Subscribe(e => vDst.SetInner(e)).D(vSrc, vDst);
}