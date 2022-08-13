# PowRxVar

## Table of content

- [Introduction](#introduction)
- [Basics](#basics)
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



## Basic interfaces
### Read only variable ```IRoVar<T>```
```cs
interface IRoVar<out T> : IObservable<T>, IRoDispBase
{
  // IRoDispBase
  // -> can register a callback on disposal & tell if disposed
  CancellationToken CancelToken { get; }
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
  CancellationTokenSource CancelSource { get; }

  // IObserver<T>
  // -> can be updated by subscribing it to an observable
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
static IRoBndVar<T> ToReadOnly<T>(this IRwBndVar<T> v);
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



## License

MIT
