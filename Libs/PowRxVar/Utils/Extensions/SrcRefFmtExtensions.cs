using System.Runtime.CompilerServices;

namespace PowRxVar.Utils.Extensions;

static class SrcRefFmtExtensions
{
	public static string Fmt(
		this (string, int) t,
		[CallerMemberName] string? memberName = null
	) =>
		$"'{memberName}' @ {t.Item1}:{t.Item2}";
}