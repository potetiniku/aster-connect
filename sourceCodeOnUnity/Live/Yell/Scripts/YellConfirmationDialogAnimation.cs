using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Linq;
using UnityEngine;

public class YellConfirmationDialogAnimation : DialogAnimation
{
	protected readonly Transform giveButtonTransform;
	protected readonly Animator giveButtonAnimator;
	protected readonly Transform cancelButtonTransform;
	protected readonly Animator cancelButtonAnimator;

	protected override IEnumerable<Func<float, float, Tweener>> Animations =>
	new Func<float, float, Tweener>[]
	{
		giveButtonTransform.DOScaleX,
		cancelButtonTransform.DOScaleX
	};

	public YellConfirmationDialogAnimation(GameObject target) : base(target)
	{
		giveButtonTransform = target.Child("FrontPanel").Child("GiveButton").transform;
		giveButtonAnimator = giveButtonTransform.GetComponent<Animator>();

		cancelButtonTransform = target.Child("CancelButton").transform;
		cancelButtonAnimator = cancelButtonTransform.GetComponent<Animator>();
	}

	protected override void OnOpenEnded()
	{
		giveButtonAnimator.enabled = true;
		cancelButtonAnimator.enabled = true;
	}

	protected override void OnCloseStarted()
	{
		giveButtonAnimator.enabled = false;
		cancelButtonAnimator.enabled = false;
	}
}
