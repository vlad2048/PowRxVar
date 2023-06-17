using System.Runtime.CompilerServices;

namespace PowRxVar.Utils.Extensions;

static class SrcRefFmtExtensions
{
	public static string Fmt(
		this (string, int) t,
		string className,
		[CallerMemberName] string? memberName = null
	) =>
		$@"{t.Item1}:{t.Item2}  @ ""{className}.{memberName}""";
}