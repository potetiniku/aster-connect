using Cysharp.Threading.Tasks;
using TMPro;
using Unity.Linq;
using UnityEngine;

public abstract class Dialog : MonoBehaviour
{
	private TMP_Text titleText;
	private TMP_Text contentText;

	protected bool isOpened;
	private bool isInAnimation;

	protected new DialogAnimation animation;
	protected abstract DialogAnimation Animation { get; }

	protected async UniTask Open()
	{
		if (isInAnimation) return;

		isOpened = true;

		isInAnimation = true;
		await Animation.Open();
		isInAnimation = false;
	}

	protected virtual async UniTask Close()
	{
		if (isInAnimation) return;

		isInAnimation = true;
		await Animation.Close();
		isInAnimation = false;

		isOpened = false;
	}

	protected void SetTexts(string title, string text)
	{
		titleText ??= gameObject.Child("Title").GetComponent<TMP_Text>();
		contentText ??= gameObject.Child("Text").GetComponent<TMP_Text>();

		titleText.text = title;
		contentText.text = text;
	}
}
