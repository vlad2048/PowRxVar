namespace RxWinFormsDemo.Structs;

record ComboRec(string Name)
{
	public override string ToString() => Name;

	public static readonly ComboRec[] Data =
	{
		new("Peter"),
		new("Jackson"),
		new("Laurel"),
		new("Michael"),
		new("Sonia"),
	};
}