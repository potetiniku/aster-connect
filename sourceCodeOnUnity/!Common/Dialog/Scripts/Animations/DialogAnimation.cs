using System;
using System.Collections.Generic;
using AsterConnect.Model;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.Linq;
using UnityEngine;
using UnityEngine.UI;

public abstract class DialogAnimation
{
	protected readonly GameObject target;
	protected readonly float timeUnit_s = 0.1f;

	protected abstract IEnumerable<Func<float, float, Tweener>> Animations { get; }

	public DialogAnimation(GameObject target)
	{
		this.target = target;
	}

	public async UniTask Open()
	{
		GameObject background = target.Parent();

		background.SetActive(true);
		target.SetActive(true);

		OnOpenStarted();
		background.GetComponent<Image>().DOFade(0.75f, timeUnit_s);
		await target.transform.DOScaleX(1, timeUnit_s).AsyncWaitForCompletion();
		await OpenContents();
		OnOpenEnded();
	}

	public async UniTask Close()
	{
		GameObject background = target.Parent();

		// ボタンのアニメーションが終わるまで待つ
		await UniTask.Delay(55);

		OnCloseStarted();
		await CloseContents();
		background.GetComponent<Image>().DOFade(0, timeUnit_s);
		await target.transform.DOScaleX(0, timeUnit_s).AsyncWaitForCompletion();
		OnCloseEnded();

		target.SetActive(false);
		background.SetActive(false);
	}

	private async UniTask OpenContents()
	{
		Animations.ForEach(a =>
			a(1, timeUnit_s).SetEase(Ease.Linear));
		await UniTask.Delay((int)(timeUnit_s * 1000));
	}

	private async UniTask CloseContents()
	{
		Animations.ForEach(a =>
			a(0, timeUnit_s).SetEase(Ease.Linear));
		await UniTask.Delay((int)(timeUnit_s * 1000));
	}

	protected virtual void OnOpenStarted() { }
	protected virtual void OnOpenEnded() { }
	protected virtual void OnCloseStarted() { }
	protected virtual void OnCloseEnded() { }
}
