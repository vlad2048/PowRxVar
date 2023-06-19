namespace PowRxVar.Tests;

class DispTests : RxBaseTest
{
	[Test]
	public void _00_LinkedDisps_1()
	{
		var d1 = new Disp();
		var d2 = new Disp();
		d1.D(d2);
		d2.D(d1);

		d1.Dispose();
	}

	[Test]
	public void _01_LinkedDisps_2()
	{
		var d1 = new Disp();
		var d2 = new Disp();
		d1.D(d2);
		d2.D(d1);

		d2.Dispose();
	}

	[Test]
	public void _02_LinkedDisps_3()
	{
		var d1 = new Disp();
		var d2 = new Disp();
		d1.D(d2);
		d2.D(d1);

		d1.Dispose();
		d2.Dispose();
	}

	[Test]
	public void _03_LinkedDisps_4()
	{
		var d1 = new Disp();
		var d2 = new Disp();
		d1.D(d2);
		d2.D(d1);

		d2.Dispose();
		d1.Dispose();
	}
}