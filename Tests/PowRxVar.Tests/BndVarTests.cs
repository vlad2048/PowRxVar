using System.Reactive.Disposables;

namespace PowRxVar.Tests;

class BndVarTests
{
	private Disp d = null!;

	[Test]
	public void _01_DispOrder()
	{
		var list = new List<int>();
		using (var dd = new Disp())
		{
			Disposable.Create(() => list.Add(1)).D(dd);
			Disposable.Create(() => list.Add(2)).D(dd);
		}
		CollectionAssert.AreEqual(list, new [] { 2, 1 });
	}

	[Test]
	public void _01_Var_Disp() =>
		Var.Make(123).D(d);

	[Test]
	public void _02_BndVar_Disp() =>
		Var.MakeBnd(123).D(d);

	[Test]
	public void _03_ToRwBndVar_Disp()
	{
		var rxVar = Var.MakeBnd(123).D(d);
		var rxBndVar = rxVar.ToRwBndVar();

		/*
		Issue before I linked the lifetimes of both

		[1, '123'], [2, 'WhenInner.Merge(WhenOuter)'], [3, 'rxVar']
		var rxVar = Var.MakeBnd(123);
		var rxBndVar = rxVar.ToRwBndVar();

		[3, 'rxVar']
		var rxVar = Var.MakeBnd(123).D(d);			<---
		var rxBndVar = rxVar.ToRwBndVar();

		[1, '123'], [2, 'WhenInner.Merge(WhenOuter)']
		var rxVar = Var.MakeBnd(123);
		var rxBndVar = rxVar.ToRwBndVar().D(d);		<---

		*/
	}


	[SetUp]
	public void Setup()
	{
		DispStats.ClearForTests();
		d = new Disp();
	}

	[TearDown]
	public void Teardown()
	{
		d.Dispose();
		DispStats.Log().ShouldBeTrue();
	}
}