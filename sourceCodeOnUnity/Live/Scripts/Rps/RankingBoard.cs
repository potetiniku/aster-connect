using System.Collections.Generic;
using System.Linq;
using AsterConnect.Model;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Unity.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RankingBoard : MonoBehaviour
{
	public readonly List<string> Winners = new();

	[SerializeField] private AssetReferenceGameObject winner;
	[SerializeField] private GameObject othersLabel;

	public void Show()
	{
		Winners.Take(10).ForEach(name =>
			winner.InstantiateAsync(gameObject.Child("Winners").transform).Completed += handle =>
			handle.Result.Child("Name").GetComponent<TMP_Text>().text = name);

		if (Winners.Count > 10)
			othersLabel.SetActive(true);

		gameObject.SetActive(true);
		transform.DOMoveY(1.75f, 0.25f)
			.SetEase(Ease.OutQuart);
	}

	public async UniTask Hide()
	{
		await transform.DOMoveY(-1.5f, 0.25f).AsyncWaitForCompletion();
		gameObject.Child("Winners").Children()
			.ForEach(winner.ReleaseInstance);
		Winners.Clear();
		othersLabel.SetActive(false);
		gameObject.SetActive(false);
	}
}
