using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Unity.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class OkDialogAnimation : MessageDialogAnimation
{
	protected readonly Transform okButtonTransform;
	protected readonly Animator okButtonAnimator;

	protected override IEnumerable<Func<float, float, Tweener>> Animations =>
	base.Animations.Concat(new Func<float, float, Tweener>[]
	{
		okButtonTransform.DOScaleX
	});

	public OkDialogAnimation(GameObject target) : base(target)
	{
		GameObject okButton = target.Child("OkButton");
		okButtonTransform = okButton.transform;
		okButtonAnimator = okButtonTransform.GetComponent<Animator>();
	}

	protected override void OnOpenEnded()
	{
		okButtonAnimator.enabled = true;
	}

	protected override void OnCloseStarted()
	{
		okButtonAnimator.enabled = false;
	}
}
