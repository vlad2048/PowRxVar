using System.Runtime.CompilerServices;
using PowRxVar._Internal.Vars.BndVars;
using PowRxVar._Internal.Vars.NormalVars;
using PowRxVar.Utils.Extensions;

namespace PowRxVar;

public static class VarNoCheck
{
	public static IRwVar<T> Make<T>(
		T initVal,
		[CallerFilePath] string srcFile = "", [CallerLineNumber] int srcLine = 0
	)
		=> new RwVar<T>(initVal, true, (srcFile, srcLine).Fmt("VarNoCheck"));


	public static IFullRwBndVar<T> MakeBnd<T>(
		T initVal,
		[CallerFilePath] string srcFile = "", [CallerLineNumber] int srcLine = 0
	)
		=> new FullRwBndVar<T>(initVal, true, (srcFile, srcLine).Fmt("VarNoCheck"));


	internal static (IRoVar<T>, IDisposable) Make<T>(
		T initVal,
		IObservable<T> obs,
		[CallerFilePath] string srcFile = "", [CallerLineNumber] int srcLine = 0
	)
	{
		// ReSharper disable ExplicitCallerInfoArgument
		var v = Make(initVal, srcFile, srcLine);
		// ReSharper restore ExplicitCallerInfoArgument
		obs.Subscribe(v).D(v);
		return (v.ToReadOnly(), v);
	}
}