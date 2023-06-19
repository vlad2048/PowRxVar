namespace PowRxVar.Tests;

class RxBaseTest
{
	protected Disp D { get; private set; } = null!;

	[SetUp]
	public void Setup()
	{
		VarDbg.ClearUndisposedCountersForTest();
		D = new Disp();
	}

	[TearDown]
	public void Teardown()
	{
		D.Dispose();
		VarDbg.CheckForUndisposedDisps().ShouldBeFalse();
	}
}