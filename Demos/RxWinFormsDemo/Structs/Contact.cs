namespace RxWinFormsDemo.Structs;

record Contact(
	string Name,
	int Age
)
{
	public override string ToString() => Name;
}