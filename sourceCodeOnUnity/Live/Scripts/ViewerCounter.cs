using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using TMPro;
using Unity.Netcode;
using UnityEngine;

public class ViewerCounter : MonoBehaviour
{
	[SerializeField] private TMP_Text target;
	private ViewerCountGetter countGetter;

	private async UniTask Start()
	{
		await UniTask.WaitUntil(() => (countGetter =
			NetworkManager.Singleton.LocalClient?.PlayerObject
			.GetComponent<ViewerCountGetter>()) != null);

		InvokeRepeating(nameof(UpdateCount), 0, 1);
	}

	private void UpdateCount()
	{
		target.text = countGetter.Count.ToString();
	}
}
