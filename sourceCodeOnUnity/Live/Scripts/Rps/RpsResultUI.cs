using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class RpsResultUI : MonoBehaviour
{
	[SerializeField] private GameObject win;
	[SerializeField] private GameObject draw;
	[SerializeField] private GameObject lose;

	public async UniTask Show(RpsResult result)
	{
		Debug.Log(result.ToString());
		Dictionary<RpsResult, GameObject> resultUIs = new()
		{
			{RpsResult.Win, win},
			{RpsResult.Draw, draw},
			{RpsResult.Lose, lose}
		};

		GameObject targetUI = resultUIs[result];
		targetUI.transform.localScale = Vector3.zero;
		targetUI.SetActive(true);

		await targetUI.transform.DOScale(1, 0.25f)
			.SetEase(Ease.OutBack);
		await UniTask.Delay(2000);
		await targetUI.transform.DOScale(0, 0.25f)
			.SetEase(Ease.InBack);

		targetUI.SetActive(false);
	}
}
