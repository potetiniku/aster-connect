using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Unity.Linq;
using UnityEngine;

public class OkCancelDialogAnimation : MessageDialogAnimation
{
	protected readonly Transform okButtonTransform;
	protected readonly Animator okButtonAnimator;
	protected readonly Transform cancelButtonTransform;
	protected readonly Animator cancelButtonAnimator;

	protected override IEnumerable<Func<float, float, Tweener>> Animations =>
	base.Animations.Concat(new Func<float, float, Tweener>[]
	{
		okButtonTransform.DOScaleX,
		cancelButtonTransform.DOScaleX
	});

	public OkCancelDialogAnimation(GameObject target) : base(target)
	{
		okButtonTransform = target.Child("OkButton").transform;
		okButtonAnimator = okButtonTransform.GetComponent<Animator>();

		cancelButtonTransform = target.Child("CancelButton").transform;
		cancelButtonAnimator = cancelButtonTransform.GetComponent<Animator>();
	}

	protected override void OnOpenEnded()
	{
		okButtonAnimator.enabled = true;
		cancelButtonAnimator.enabled = true;
	}

	protected override void OnCloseStarted()
	{
		okButtonAnimator.enabled = false;
		cancelButtonAnimator.enabled = false;
	}
}
