using System.Reactive.Disposables;

namespace PowRxVar.Tests;

class BndVarTests : RxBaseTest
{
	[Test]
	public void _00_DispOrder()
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
		Var.Make(123).D(D);

	[Test]
	public void _02_BndVar_Disp() =>
		Var.MakeBnd(123).D(D);

	[Test]
	public void _03_ToRwBndVar_Disp()
	{
		var rxVar = Var.MakeBnd(123).D(D);
		var rxBndVar = rxVar.ToRwBndVar();
	}

	[Test]
	public void _04_InnerOuter()
	{
		var rxVarFull = Var.MakeBnd(3).D(D);
		var rxVar = rxVarFull.ToRwBndVar();

		var cntFull = 0;
		var cntFullInner = 0;
		var cntFullOuter = 0;
		rxVarFull.Subscribe(_ => cntFull++);
		rxVarFull.WhenInner.Subscribe(_ => cntFullInner++);
		rxVarFull.WhenOuter.Subscribe(_ => cntFullOuter++);
		void CheckFull(int expVal, int expCnt, int expCntInner, int expCntOuter)
		{
			rxVarFull.V.ShouldBe(expVal);
			cntFull.ShouldBe(expCnt);
			cntFullInner.ShouldBe(expCntInner);
			cntFullOuter.ShouldBe(expCntOuter);
			//Console.WriteLine($"val:{rxVar.V}  cnt:{cnt}  cntInner:{cntInner}  cntOuter:{cntOuter}");
		}

		var cnt = 0;
		var cntInner = 0;
		var cntOuter = 0;
		rxVar.Subscribe(_ => cnt++);
		rxVar.WhenInner.Subscribe(_ => cntInner++);
		rxVar.WhenOuter.Subscribe(_ => cntOuter++);
		void CheckSimp(int expVal, int expCnt, int expCntInner, int expCntOuter)
		{
			rxVar.V.ShouldBe(expVal);
			cnt.ShouldBe(expCnt);
			cntInner.ShouldBe(expCntInner);
			cntOuter.ShouldBe(expCntOuter);
			//Console.WriteLine($"val:{rxVar.V}  cnt:{cnt}  cntInner:{cntInner}  cntOuter:{cntOuter}");
		}

		CheckFull(3, 1, 0, 0);
		CheckSimp(3, 1, 0, 0);

		rxVarFull.SetInner(4);
		CheckFull(4, 2, 1, 0);
		CheckSimp(4, 2, 1, 0);

		rxVarFull.SetOuter(5);
		CheckFull(5, 3, 1, 1);
		CheckSimp(5, 3, 1, 1);

		rxVarFull.V = 6;
		CheckFull(6, 4, 1, 2);
		CheckSimp(6, 4, 1, 2);


		rxVar.SetOuter(7);
		CheckFull(7, 5, 1, 3);
		CheckSimp(7, 5, 1, 3);

		rxVar.V = 8;
		CheckFull(8, 6, 1, 4);
		CheckSimp(8, 6, 1, 4);
	}
}