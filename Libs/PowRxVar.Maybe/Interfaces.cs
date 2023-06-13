using PowMaybe;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public interface IRwMayVar<T> : IRoMayVar<T>, IRwVar<Maybe<T>> { }

public interface IRoMayVar<T> : IRoVar<Maybe<T>> { }

public interface IFullRwMayBndVar<T> : IRwMayBndVar<T>, IFullRwBndVar<Maybe<T>> { }

public interface IRwMayBndVar<T> : IRwMayVar<T>, IRoMayBndVar<T>, IRwBndVar<Maybe<T>> { }

public interface IRoMayBndVar<T> : IRoMayVar<T>, IRoBndVar<Maybe<T>> { }

