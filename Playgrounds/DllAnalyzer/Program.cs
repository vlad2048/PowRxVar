using Mono.Cecil;
using Mono.Cecil.Cil;

namespace DllAnalyzer;

static class Program
{
	public static void Main()
	{
		var dllFile = @"C:\Dev_Nuget\Libs\PowRxVar\Demos\RxWinFormsDemo\bin\Debug\net6.0-windows\PowRxVar.WinForms.dll";

		var readOpt = new ReaderParameters
		{
			ReadSymbols = true
		};
		using var ass = AssemblyDefinition.ReadAssembly(dllFile, readOpt);
		var refs =

			from module in ass.Modules
			from type in module.Types
			from method in type.Methods
			where method.Body is not null
			from instr in method.Body.Instructions
			where instr.OpCode == OpCodes.Call
			let methodRef = instr.Operand as MethodReference
			where methodRef.Name == "MakeBnd"
			select (method, methodRef, instr.Offset, instr);
	}
}