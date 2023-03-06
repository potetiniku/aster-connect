using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InsertionText : MonoBehaviour
{
	[SerializeField] private GameObject background;
	[SerializeField] private GameObject text;

	public async UniTask Play(string content, Color backgroundColor)
	{
		gameObject.SetActive(true);

		background.transform.localScale = new(0, 1, 1);
		background.GetComponent<Image>().color = backgroundColor;
		text.GetComponent<TMP_Text>().text = content;

		await background.transform.DOScaleX(1, 0.5f);
		await UniTask.Delay(1000);
		await background.transform.DOScaleX(0, 0.5f);

		gameObject.SetActive(false);
	}

	private void Update()
	{
		text.transform.localScale =
			new(1 / background.transform.localScale.x, 1, 1);
	}
}
