using System.Net;
using UnityEngine;
using UnityEngine.UI;

public class ControllerEntrance : Entrance
{
	public override void UpdateInteractable()
	{
		buttonComponent ??= GetComponent<Button>();
		buttonComponent.interactable =
			IPAddress.TryParse(serverAddressField.text, out _);
	}
}
