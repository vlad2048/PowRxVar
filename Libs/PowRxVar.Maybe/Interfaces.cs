using PowMaybe;

// ReSharper disable once CheckNamespace
namespace PowRxVar;

public interface IRwMayVar<T> : IRwVar<Maybe<T>> { }

public interface IRoMayVar<T> : IRoVar<Maybe<T>> { }

public interface IFullRwMayBndVar<T> : IFullRwBndVar<Maybe<T>> { }

public interface IRwMayBndVar<T> : IRwBndVar<Maybe<T>> { }

public interface IRoMayBndVar<T> : IRoBndVar<Maybe<T>> { }

