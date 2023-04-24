# PowRxVar

## Table of content

- [Introduction](#introduction)
- [Basics](#basics)
- [Combining with Expression Trees](#combining-with-expression-trees)
- [Basic interfaces](#basic-interfaces)
- [Bound variables](#bound-variables)
- [License](#license)



## Introduction

Provides reactive variables integrated with Rx.Net:
- implement IObservable<T> so can be queried using reactive extensions
- can access and set the current value
- can be combined into other variables (Combine, Switch)



## Basics

```c#
// Create variables
var a = Var.Make(3);
var b = Var.Make(7);

// Get and set the value
Console.WriteLine($"{a.V}"); // 3
a.V = 4;

// Combine variables
var c = Var.Combine(a, b, (va, vb) => va * 100 + vb);
Console.WriteLine($"{c.V}"); // 407
b.V = 8;
Console.WriteLine($"{c.V}"); // 408

// Dispose a variable
a.Dispose();
var valA = a.V; // Exception
var valB = b.V; // 8
var valC = c.V; // Exception
```



## Combining with Expression Trees
There is a more general mechanism to combine any number of variables of any type:
```c#
var a = Var.Make(3);
var b = Var.Make(5);
var c = Var.Make(7);
var r = Var.Expr(() => a.V * 3 + b.V * 2 + c.V);

// r.V == 26
```
Similarly to ```Combine()```, ```r``` will be disposed as soon as either **a** or **b** or **c** is disposed.

Note: this technique is slower to setup at runtime than using Combine() because of the need to compile expression trees.


## Basic interfaces
### Read only variable ```IRoVar<T>```
```cs
interface IRoVar<out T> : IObservable<T>, IRoDispBase
{
  // IRoDispBase
  // -> can register a callback on disposal & tell if disposed
  IObservable<Unit> WhenDisposed { get; }
  bool IsDisposed { get; }

  // IObservable<T>
  // -> can use it as a normal observable
  IDisposable Subscribe(IObserver<T> observer);

  // -> can read its value
  T V { get; }
}
```
### Read write variable ```IRwVar<T>```
```cs
interface IRwVar<T> : IRoVar<T>, IObserver<T>, IRwDispBase
{
  // IRwDispBase
  // -> inherits from IRoDispBase but can also dispose it
  void Dispose();

  // IObserver<T>
  // -> can be updated by subscribing an observable to it
  void OnNext(T value);
  void OnCompleted();
  void OnError(Exception error);

  // -> can read and write its value
  T V { get; set; }
}
```
### Note
As ```IRwVar<T>``` inherits from ```IRoVar<T>``` you can naturally use a read write variable in place where a read only one is required.

However, if you want to prevent the calling code from casting it back to an ```IRwVar<T>```, you can use this extension method:
```c#
static IRoVar<T> ToReadOnly<T>(this IRwVar<T> v);
```
This is similar to ```Observable.AsObservable```



## Bound variables
When binding variables to a UI control, it's sometimes useful to differentiate between updates done by the user through the control (inner) and updates done by the code (outer).

### Usage
```c#
// Create
var v = Var.MakeBnd(7);

// Set
v.SetInner(3);
v.SetOuter(5);

// Listen to updates
v.WhenInner.Subscribe(...);
v.WhenOuter.Subscribe(...);

// Get the latest value
Console.WriteLine($"{v.V}"); // 5
```

Similarly to ```IRoVar<T>``` & ```IRwVar<T>```, bound variables use ```IRoBndVar<T>``` & ```IRwBndVar<T>```



## Bound variable interfaces
The interfaces here are more complex, but I believe they make sense.

- ```IRoBndVar<T> : IRoVar<T>```
So a read only bound variable can listen to either:
  - the outer stream WhenOuter
  - the inner stream WhenInner
  - the merged stream through itself as it implements ```IObservable<T>``` through ```IRoVar<T>```

  So IRoBndVar< T > is an IRoVar< T > in its merged variable

- ```IFullRwBndVar<T> : IRwBndVar<T>```
  additionally allows to:
  - set the inner value
  - set the outer value
  Functions that hook variables to the UI (both ways) will use this interface internally

  Setting var.V = xxx will set its outer value.
  So IFullRwBndVar< T > is an IRwVar< T > in its outer variable

- ```IRwBndVar<T>```
  this is the same as ```IFullRwBndVar<T>``` with the exception that it cannot set inner values
  Functions that hook variables to the UI (both ways) will return this interface to user code (as the user doesn't need to set the inner value - that's the role of the hook)

  Again, setting var.V = xxx will set its outer value.
  So IRwBndVar< T > is an IRwVar< T > in its outer variable

## Ownership and disposable interfaces

### Read only variables implement ```IRoDispBase```
- Can tell if it's disposed (```bool IsDisposed```)
- Can React to a disposable (```IObservable<Unit> WhenDisposed```)
- **Cannot** call ```Dispose()```

### Read write variables implement ```IRwDispBase```
- Inherits ```IRoDispBase```
- Additionally, **Can** call ```Dispose()``` (```IDisposable```)



## License

MIT

