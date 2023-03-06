using System.Collections.Generic;
using System.Linq;
using AsterConnect.Model;
using TMPro;
using UnityEngine;

public class SaveData : MonoBehaviour
{
	[SerializeField]
	private TMP_InputField[] userNameFields;

	[SerializeField]
	private TMP_InputField[] serverAddressFields;

	private const string userNameKey = "userName";
	private const string serverAddressKey = "serverAddress";

	private void Start()
	{
		Load();
	}

	public void Save()
	{
		PlayerPrefs.SetString(userNameKey,
			userNameFields.SingleOrDefault(f => f.gameObject.activeInHierarchy)?.text);
		PlayerPrefs.SetString(serverAddressKey,
			serverAddressFields.Single(f => f.gameObject.activeInHierarchy).text);
	}

	public void Load()
	{
		LoadFields(userNameFields, userNameKey);
		LoadFields(serverAddressFields, serverAddressKey);

		void LoadFields(IEnumerable<TMP_InputField> fields, string saveKey)
		{
			fields.ForEach(f =>
				f.text = PlayerPrefs.GetString(saveKey));
		}
	}
}
