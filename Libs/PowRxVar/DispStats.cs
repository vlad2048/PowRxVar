using System.Collections.Concurrent;
using System.Diagnostics;

namespace PowRxVar;

static class DispStats
{
	public static void SetBP(int id) => bpId = id;

	public static Action? OnBPHit { get; set; }

	public static bool Log()
	{
		L("");
		var arr = map.Values.OrderBy(e => e.Id).ToArray();
		var title = arr.Length switch
		{
			0 => "All Disps released",
			_ => $"{arr.Length} unreleased Disp{(arr.Length == 1 ? "" : "s")}"
		};
		L(title);
		L(new string('=', title.Length));
		if (arr.Length > 0)
			L(string.Join(", ", arr.Select(e => $"{e}")));
		L("");
		return arr.Length == 0;
	}




	private record VarNfo(int Id, string? Expr)
	{
		public override string ToString() => $"[{Id}, {ExprStr}]";
		private string ExprStr => Expr switch
		{
			null => "_",
			not null => $"'{Expr}'"
		};
	}

	private static readonly ConcurrentDictionary<int, VarNfo> map = new();
	private static int? bpId;
	private static int dispIdIdx;

	internal static int GetNextDispId() => Interlocked.Increment(ref dispIdIdx);
	internal static void ClearForTests()
	{
		dispIdIdx = 0;
		map.Clear();
	}


	internal static void DispCreated(int id, string? dbgExpr)
	{
		try
		{
			if (bpId == id) OnBPHit?.Invoke();
			if (map.ContainsKey(id)) throw new ArgumentException("Invalid Disp.id");
			map[id] = new VarNfo(id, dbgExpr);
		}
		catch (Exception)
		{
			Debugger.Launch();
			Debugger.Break();
		}
	}

	internal static void DispDisposed(int id)
	{
		try
		{
			if (!map.ContainsKey(id)) throw new ArgumentException("Invalid Disp.id");
			map.TryRemove(id, out _);
		}
		catch (Exception)
		{
			Debugger.Launch();
			Debugger.Break();
		}
	}

	private static void L(string s) => Console.WriteLine(s);
}