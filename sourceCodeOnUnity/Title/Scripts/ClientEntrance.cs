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

		// SaveData.Start()‚ÅInputField‚ª‘‚«Š·‚¦‚ç‚ê‚Ä
		// UpdateInteractive()‚ªŒÄ‚Î‚ê‚½ê‡‚Í
		// Start()‚ª‚·‚Å‚ÉŒÄ‚Î‚ê‚Ä‚¢‚é•ÛØ‚ª‚È‚¢‚½‚ß
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
