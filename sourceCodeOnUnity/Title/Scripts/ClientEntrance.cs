using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClientEntrance : Entrance
{
	[SerializeField]
	private TMP_InputField userNameField;

	public override void UpdateInteractable()
	{
		string userName = userNameField.text;

		// SaveData.Start()でInputFieldが書き換えられて
		// UpdateInteractive()が呼ばれた場合は
		// Start()がすでに呼ばれている保証がないため
		buttonComponent ??= GetComponent<Button>();

		buttonComponent.interactable =
			userName != string.Empty &&
			userName.Length <= 8 &&
			IPAddress.TryParse(serverAddressField.text, out _);
	}

	public override void EnterLive()
	{
		Client.TempUserName = userNameField.text;
		base.EnterLive();
	}
}
