using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RpsChoiceButton : MonoBehaviour
{
	[SerializeField] private Hand hand;
	[SerializeField] private AudioPlayer sePlayer;
	[SerializeField] private AudioClip se;

	public void OnClick()
	{
		if (!GetComponent<Toggle>().isOn) return;

		NetworkInitializer.Manager.LocalClient.PlayerObject
			.GetComponent<RpsManager>().SelectHandServerRpc(hand);

		sePlayer.PlayOneShot(se);
	}
}
