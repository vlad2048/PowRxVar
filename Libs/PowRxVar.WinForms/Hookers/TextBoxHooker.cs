namespace PowRxVar.WinForms.Hookers;

public static class TextBoxHooker
{
	public static IFullRwBndVar<string> HookText(this TextBox ctrl)
	{
		var text = Var.MakeBnd(ctrl.Text).D(ctrl);

		ctrl.Events().TextChanged.Subscribe(_ =>
		{
			text.SetInner(ctrl.Text);
		}).D(ctrl);

		text.WhenOuter.Subscribe(val =>
		{
			ctrl.Text = val;
		}).D(ctrl);

		return text;
	}
}