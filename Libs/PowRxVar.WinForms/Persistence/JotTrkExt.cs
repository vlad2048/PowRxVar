namespace PowRxVar.WinForms.Persistence;

public static class JotTrkExt
{
	public static T Track<T>(this T obj) where T : class
	{
		switch (obj)
		{
			case Form form:
				form.Events().Load.Subscribe(_ => JotTrk.Tracker.Track(obj)).D(form);
				break;

			default:
				JotTrk.Tracker.Track(obj);
				break;
		}
		return obj;
	}
}