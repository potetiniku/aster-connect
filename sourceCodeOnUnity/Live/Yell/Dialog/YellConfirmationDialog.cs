using AsterConnect.Model.Connection;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

public class YellConfirmationDialog : Dialog
{
	[SerializeField] private new TMP_Text name;
	[SerializeField] private TMP_Text size;
	[SerializeField] private TMP_Text price;
	[SerializeField] private TMP_Text havingStones;

	public bool IsOkClicked { get; private set; }

	protected override DialogAnimation Animation =>
		animation ??= new YellConfirmationDialogAnimation(gameObject);

	private YellInfo target;
	private GameObject yellSample;

	/// <summary>
	/// ダイアログを表示する
	/// </summary>
	/// <returns>OKボタンが押されたらtrue、キャンセルボタンが押されたらfalse</returns>
	public async UniTask<bool> Show(YellInfo target)
	{
		name.text = target.Name;
		size.text = target.Size.GetDescription();
		price.text = target.Price.GetValue().ToString();
		havingStones.text = "∞";

		target.OriginalPrefab.InstantiateAsync().Completed += handle =>
		{
			yellSample = handle.Result;
			yellSample.AddComponent<YellSample>();
		};

		this.target = target;

		await Open();
		await UniTask.WaitUntil(() => !isOpened);
		return IsOkClicked;
	}

	protected override async UniTask Close()
	{
		await base.Close();
		target.OriginalPrefab.ReleaseInstance(yellSample);
	}

	public async void Ok()
	{
		target.Instantiate();

		IsOkClicked = true;
		await Close();
	}

	public async void Cancel()
	{
		IsOkClicked = false;
		await Close();
	}
}
