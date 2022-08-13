namespace PowRxVar.Vars;

public interface IRwBndVar<T> : IRoBndVar<T>, IRwDispBase
{
	void SetInner(T v);
	void SetOuter(T v);
}