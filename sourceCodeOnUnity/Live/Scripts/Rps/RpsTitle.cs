using System.Collections;
using System.Collections.Generic;
using AsterConnect.Model;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class RpsTitle : MonoBehaviour
{
	[SerializeField] private Transform baloon;
	[SerializeField] private Transform text;
	[SerializeField] private Transform rock;
	[SerializeField] private Transform paper;
	[SerializeField] private Transform scissor;

	public async UniTask Show()
	{
		gameObject.SetActive(true);

		Transform[] uis = { baloon, rock, scissor, paper, text };
		uis.ForEach(ui => ui.localScale = Vector3.zero);

		const float timeUnit = 0.5f;
		Show(baloon, 0);
		Show(rock, timeUnit);
		Show(scissor, timeUnit * 1.5f);
		Show(paper, timeUnit * 2);
		Show(text, timeUnit * 3);

		await UniTask.Delay(1000 * 3);

		uis.ForEach(ui => ui.DOScale(0, timeUnit / 2).SetEase(Ease.InBack));
		await UniTask.Delay((int)(timeUnit * 1000));

		gameObject.SetActive(false);

		static void Show(Transform target, float delay)
		{
			target.DOScale(1, timeUnit)
				.SetEase(Ease.OutBounce)
				.SetDelay(delay);
		}
	}
}
