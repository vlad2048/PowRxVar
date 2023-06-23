using PowMaybe;
using PowRxVar._Internal.Vars.BndVars;
using PowRxVar._Internal.Vars.NormalVars;

namespace PowRxVar.Maybe._Internals;


sealed class RwMayVar<T> : RwVar<Maybe<T>>, IRwMayVar<T>
{
	public RwMayVar(Maybe<T> initVal, bool noCheck, string? dbgExpr) : base(initVal, noCheck, dbgExpr) { }
}

sealed class RoMayVar<T> : RoVar<Maybe<T>>, IRoMayVar<T>
{
	public RoMayVar(IRwVar<Maybe<T>> rwVar) : base(rwVar) { }
}

sealed class RoMayVarConst<T> : RoVarConst<Maybe<T>>, IRoMayVar<T>
{
	public RoMayVarConst(Maybe<T> v) : base(v) { }
}


sealed class FullRwMayBndVar<T> : FullRwBndVar<Maybe<T>>, IFullRwMayBndVar<T>
{
	public FullRwMayBndVar(Maybe<T> initVal, bool noCheck, string? dbgExpr) : base(initVal, noCheck, dbgExpr) { }
}

sealed class RwMayBndVar<T> : RwBndVar<Maybe<T>>, IRwMayBndVar<T>
{
	public RwMayBndVar(IFullRwBndVar<Maybe<T>> fullRwBndVar, string? dbgExpr) : base(fullRwBndVar, dbgExpr) { }
}

sealed class RoMayBndVar<T> : RoBndVar<Maybe<T>>, IRoMayBndVar<T>
{
	public RoMayBndVar(IFullRwBndVar<Maybe<T>> fullRwBndVar) : base(fullRwBndVar) { }
}

