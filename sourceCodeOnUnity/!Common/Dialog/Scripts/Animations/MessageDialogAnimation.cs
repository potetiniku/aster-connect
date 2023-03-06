using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using Unity.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MessageDialogAnimation : DialogAnimation
{
	protected readonly Image header;
	protected readonly TMP_Text text;

	protected override IEnumerable<Func<float, float, Tweener>> Animations => new Func<float, float, Tweener>[]
	{
		header.DOFade,
		text.DOFade
	};

	public MessageDialogAnimation(GameObject target) : base(target)
	{
		header = target.Child("Header").GetComponent<Image>();
		text = target.Child("Text").GetComponent<TMP_Text>();
	}
}
