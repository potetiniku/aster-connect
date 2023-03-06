using System;
using UnityEngine;
using UniVRM10;

public class VRMLipSyncContextMorphTarget : MonoBehaviour
{
	// Game�r���[���A�N�e�B�u(�L�[���͂��ł�����)�ɂ��Ă����Ȃ���
	// �}�C�N�̉�������Ȃ��̂Œ���

	public OVRLipSyncContextBase context;

	[SerializeField]
	private Vrm10Instance vrmInstance;

	private Vrm10RuntimeExpression expression => vrmInstance.Runtime.Expression;

	//  { -1, 4, 4, 5, 3, 3, 3, 3, -1, 3, 2, 5, 3, 6, 4 }; //AIUEO�ւ̐U�蕪���e�[�u��
	private readonly ExpressionKey[] visemeToBlendTargets =
	{
		ExpressionKey.Neutral,
		ExpressionKey.Ou,
		ExpressionKey.Ou,
		ExpressionKey.Ih,
		ExpressionKey.Ou,
		ExpressionKey.Ou,
		ExpressionKey.Ou,
		ExpressionKey.Ou,
		ExpressionKey.Neutral,
		ExpressionKey.Ou,
		ExpressionKey.Ee,
		ExpressionKey.Ih,
		ExpressionKey.Oh,
		ExpressionKey.Aa,
		ExpressionKey.Aa,
	};

	[SerializeField, Range(0f, 5f)]
	private float LipSyncSensitivity = 1f;

	[SerializeField]
	private int SmoothAmount = 100;

	private void Start()
	{
		context = GetComponent<OVRLipSyncContextBase>();
		context.Smoothing = SmoothAmount;
	}

	private void UpdateVRMMorph(OVRLipSync.Frame frame)
	{
		expression.SetWeight(ExpressionKey.Aa, Mathf.Min(1.0f, frame.Visemes[13] * LipSyncSensitivity));
		/*/ �S���K�p����ƌ����J�����ςȂ��ɂȂ�
		for (int i = 1; i < visemeToBlendTargets.Length; i++)
		{
			if (new[] { 9, 11, 13 }.Contains(i))
				expression.SetWeight(visemeToBlendTargets[i], Mathf.Min(1.0f, frame.Visemes[i] * LipSyncSensitivity));
		}
		//*/
	}

	private void LateUpdate()
	{
		if (context == null) return;

		OVRLipSync.Frame frame = context.GetCurrentPhonemeFrame();
		if (frame != null)
		{
			UpdateVRMMorph(frame);
		}
	}
}