using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

public class AppExit : MonoBehaviour
{
	[SerializeField]
	private GameObject dialogContainer;

	[SerializeField]
	private OkCancelDialog dialog;

	private async UniTask Update()
	{
		if (!Input.GetKeyDown(KeyCode.Escape)) return;
		if (dialogContainer.activeInHierarchy) return;
		if (!await dialog.Show("確認", "アプリを終了しますか？")) return;
		Application.Quit();
	}
}
