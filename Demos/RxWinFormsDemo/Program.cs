using PowRxVar;
using RxWinFormsDemo.Wins;

namespace RxWinFormsDemo;

static class Program
{
	/// <summary>
	///  The main entry point for the application.
	/// </summary>
	[STAThread]
	static void Main()
	{
		// To customize application configuration such as set high DPI settings or default font,
		// see https://aka.ms/applicationconfiguration.
		ApplicationConfiguration.Initialize();

		//var win = new MainWin();
		//var win = new LinkedComboWin();
		var win = new AutoSelectWin();

		Application.Run(win);
	}
}