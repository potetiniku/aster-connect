using AsterConnect.Model;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RpsChoicesUI : MonoBehaviour
{
	[SerializeField] private TMP_Text remainTime;
	[SerializeField] private Toggle[] choices;
	[SerializeField] private InsertionText insertionText;
	[SerializeField] private Color timeUpInsertion;

	public void Show()
	{
		remainTime.text = 15.ToString();
		choices.ForEach(c => c.interactable = true);
		gameObject.SetActive(true);
		_ = DecreaseTime();
	}

	private async UniTask DecreaseTime()
	{
		for (int i = 15; i > 0; i--)
		{
			remainTime.text = i.ToString();
			await UniTask.Delay(1000);
		}
		remainTime.text = 0.ToString();

		Close();
	}

	private void Close()
	{
		choices.ForEach(c => c.interactable = false);
		_ = insertionText.Play("タイムアップ!", timeUpInsertion);
	}

	public void Hide()
	{
		gameObject.SetActive(false);
	}
}
