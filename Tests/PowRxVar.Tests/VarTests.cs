namespace PowRxVar.Tests;

class VarTests
{
	[Test]
	public void _00_RwVar_Change_and_Dispose()
	{
		var t = Var.Make(3);

		var cnt = 0;
		t.Subscribe(_ => cnt++);
		cnt.ShouldBe(1);
		t.V.ShouldBe(3);

		t.V = 5;
		cnt.ShouldBe(2);
		t.V.ShouldBe(5);

		t.V = 5;
		cnt.ShouldBe(2);
		t.V.ShouldBe(5);
		
		t.V = 7;
		cnt.ShouldBe(3);
		t.V.ShouldBe(7);

		t.Dispose();
		cnt.ShouldBe(3);

		Should.Throw<ObjectDisposedException>(() => t.V);
		Should.Throw<ObjectDisposedException>(() => t.V = 11);
	}

	[Test]
	public void _01_RoVar_Change_and_Dispose_Underlying()
	{
		var t = Var.Make(3);
		var r = t.ToReadOnly();
		var cnt = 0;
		r.Subscribe(_ => cnt++);
		cnt.ShouldBe(1);
		r.V.ShouldBe(3);

		t.V = 5;
		cnt.ShouldBe(2);
		r.V.ShouldBe(5);

		t.V = 5;
		cnt.ShouldBe(2);
		r.V.ShouldBe(5);
		
		t.V = 7;
		cnt.ShouldBe(3);
		r.V.ShouldBe(7);

		t.Dispose();
		cnt.ShouldBe(3);

		Should.Throw<ObjectDisposedException>(() => r.V);
	}

	[Test]
	public void _02_Combine()
	{
		var t = Var.Make(3);
		var u = Var.Make(5);
		var v = Var.Combine(t, u, (vt, vu) => vt * 100 + vu);
		var cnt = 0;
		v.Subscribe(_ => cnt++);

		void SetT(int n)
		{
			L($"t <- {n}");
			t.V = n;
		}
		void SetU(int n)
		{
			L($"u <- {n}");
			u.V = n;
		}
		v.Subscribe(n => L($"{n}"));

		void Check(int? valT, int? valU, int? valV, int expCnt)
		{
			CheckVal(valT, t);
			CheckVal(valU, u);
			CheckVal(valV, v);
			cnt.ShouldBe(expCnt);
		}

		Check(3, 5, 305, 1);

		SetT(4);
		Check(4, 5, 405, 2);

		SetU(12);
		Check(4, 12, 412, 3);

		SetT(6);
		Check(6, 12, 612, 4);

		SetU(47);
		Check(6, 47, 647, 5);

		t.Dispose();
		Check(null, 47, null, 5);

		SetU(23);
		Check(null, 23, null, 5);

		SetU(12);
		Check(null, 12, null, 5);
	}

	[Test]
	public void _03_Expr()
	{
		var a = Var.Make(3);
		var b = Var.Make(5);
		var c = Var.Make(7);
		var r = Var.Expr(() => a.V * 3 + b.V * 2 + c.V);

		var cnt = 0;
		r.Subscribe(_ => cnt++);
		void Check(int? expVal, int cntExp)
		{
			CheckVal(expVal, r);
			cnt.ShouldBe(cntExp);
		}

		Check(3 * 3 + 5 * 2 + 7, 1);

		a.V = 1;
		Check(1 * 3 + 5 * 2 + 7, 2);

		b.V = 6;
		Check(1 * 3 + 6 * 2 + 7, 3);

		c.V = 2;
		Check(1 * 3 + 6 * 2 + 2, 4);

		b.Dispose();
		Check(null, 4);
	}

	[Test]
	public void _04_Switch()
	{
		var i0 = Var.Make(3);
		var vv = Var.Make(i0);
		var v = vv.SwitchVar(e => e);
		var cnt = 0;
		v.Subscribe(_ => cnt++);

		void Check(int? expVal, int cntExp)
		{
			CheckVal(expVal, v);
			cnt.ShouldBe(cntExp);
		}

		Check(3, 1);

		i0.V = 5;
		Check(5, 2);

		i0.V = 5;
		Check(5, 2);

		i0.V = 7;
		Check(7, 3);

		var i1 = Var.Make(30);
		vv.V = i1;
		Check(30, 4);

		i0.V = 2;
		Check(30, 4);

		i0.Dispose();
		Check(30, 4);

		i1.V = 50;
		Check(50, 5);

		i1.V = 50;
		Check(50, 5);

		i1.V = 70;
		Check(70, 6);

		i1.Dispose();
		Check(70, 6);

		var i2 = Var.Make(70);
		vv.V = i2;
		Check(70, 6);

		i2.V = 90;
		Check(90, 7);

		vv.Dispose();
		Check(null, 7);
	}

	[Test]
	public void _05_BndVar()
	{
		var v = Var.MakeBnd(3);

		var innerCnt = 0;
		var outerCnt = 0;
		v.WhenInner.Subscribe(_ => innerCnt++);
		v.WhenOuter.Subscribe(_ => outerCnt++);

		void Check(int? expVal, int expCntInner, int expCntOuter)
		{
			CheckVal(expVal, v);
			innerCnt.ShouldBe(expCntInner);
			outerCnt.ShouldBe(expCntOuter);
		}

		Check(3, 0, 0);

		v.SetInner(5);
		Check(5, 1, 0);

		v.SetInner(5);
		Check(5, 1, 0);

		v.SetInner(7);
		Check(7, 2, 0);

		v.SetOuter(7);
		Check(7, 2, 0);

		v.SetOuter(12);
		Check(12, 2, 1);

		v.SetOuter(12);
		Check(12, 2, 1);

		v.SetOuter(15);
		Check(15, 2, 2);

		v.Dispose();
		Should.Throw<ObjectDisposedException>(() => v.SetInner(123));
		Should.Throw<ObjectDisposedException>(() => v.SetOuter(456));
		Should.Throw<ObjectDisposedException>(() => v.V);
	}


	static void CheckVal(int? expVal, IRoVar<int> var)
	{
		switch (expVal)
		{
			case not null:
				var.V.ShouldBe(expVal.Value);
				break;
			case null:
				Should.Throw<ObjectDisposedException>(() => var.V);
				break;
		}
	}


	static void CheckVal(int? expVal, IRoBndVar<int> var)
	{
		switch (expVal)
		{
			case not null:
				var.V.ShouldBe(expVal.Value);
				break;
			case null:
				Should.Throw<ObjectDisposedException>(() => var.V);
				break;
		}
	}

	private static void L(string str) => Console.WriteLine(str);
}