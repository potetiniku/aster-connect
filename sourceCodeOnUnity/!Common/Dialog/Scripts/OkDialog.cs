using Cysharp.Threading.Tasks;

public class OkDialog : Dialog
{
	protected override DialogAnimation Animation =>
		animation ??= new OkDialogAnimation(gameObject);

	public async UniTask Show(string title, string text)
	{
		SetTexts(title, text);
		await Open();
		await UniTask.WaitUntil(() => !isOpened);
	}

	public async void Ok()
	{
		await Close();
	}
}
