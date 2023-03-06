using Cysharp.Threading.Tasks;

public class OkCancelDialog : Dialog
{
	public bool IsOkClicked { get; private set; }

	protected override DialogAnimation Animation =>
		animation ??= new OkCancelDialogAnimation(gameObject);

	/// <summary>
	/// �_�C�A���O��\������
	/// </summary>
	/// <returns>OK�{�^���������ꂽ��true�A�L�����Z���{�^���������ꂽ��false</returns>
	public async UniTask<bool> Show(string title, string text)
	{
		SetTexts(title, text);
		await Open();
		await UniTask.WaitUntil(() => !isOpened);
		return IsOkClicked;
	}

	public async void Ok()
	{
		IsOkClicked = true;
		await Close();
	}

	public async void Cancel()
	{
		IsOkClicked = false;
		await Close();
	}
}
