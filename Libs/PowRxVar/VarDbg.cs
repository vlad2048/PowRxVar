using System.Diagnostics;

namespace PowRxVar;

public static class VarDbg
{
	/// <summary>
	/// Call this at the end of your program to check if you forgot to call Dispose() on some Disps
	/// </summary>
	/// <param name="pauseOnIssue">if true, then in case of an issue, wait for a key press before exiting</param>
	/// <returns>true if there were undisposed Disps</returns>
	public static bool CheckForUndisposedDisps(bool pauseOnIssue = false)
	{
		DisposeExtensions.DisposeExitD();
		var isOk = DispStats.Log();
		if (pauseOnIssue && !isOk)
			Console.ReadKey();
		return !isOk;
	}

	/// <summary>
	/// If a call to CheckForUndisposedDisps prints some undisposed Disps, you can use their printed allocId to breakpoint when they are created the next time the program is run
	/// </summary>
	/// <param name="allocId">Disp allocId as printed by CheckForUndisposedDisps</param>
	public static void BreakpointOnDispAlloc(int allocId)
	{
		DispStats.SetBP(allocId);
		DispStats.OnBPHit = Debugger.Break;
	}

	/// <summary>
	/// Clears the undisposed Disps counters <br/>
	/// Call this on tests [SetUp] to make them independent
	/// </summary>
	public static void ClearUndisposedCountersForTest() => DispStats.ClearForTests();
}