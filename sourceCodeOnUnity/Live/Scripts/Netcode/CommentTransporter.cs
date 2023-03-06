using System.Collections.Generic;
using System.Linq;
using AsterConnect.Model;
using TMPro;
using Unity.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CommentTransporter : NetworkBehaviour
{
	[SerializeField]
	private AssetReferenceT<GameObject> commentLv1;

	private GameObject commentView;
	private GameObject commentsParent =>
		commentView.Child("Viewport").Child("Content");

	private GameObject commentField;
	private Client client;

	private readonly Queue<GameObject> comments = new();

	private void Start()
	{
		if (!IsOwner) return;

		SceneLoader.Instance.OnLoadTitle += () =>
		{
			// Lv2���o�ꂵ���炻���comments�ɍ������Ă��܂����߁A�����͂����Ȃ�
			comments.ForEach(commentLv1.ReleaseInstance);
		};
	}

	private void Update()
	{
		if (!IsOwner) return;

		// Start()����CommentField���A�N�e�B�u�ȕۏ؂��Ȃ�
		if (commentField == null)
		{
			commentField = GameObject.Find("CommentField");
			if (commentField == null) return;

			TMP_InputField field = commentField.GetComponent<TMP_InputField>();
			field.onEndEdit.AddListener(c =>
			{
				if (Input.GetKey(KeyCode.Return)) Send(c);
			});
			field.onTouchScreenKeyboardStatusChanged.AddListener(status =>
			{
				if (status == TouchScreenKeyboard.Status.Done) Send(field.text);
			});
		}

		// ��������Ȃ��ƍŏ��̃R�����g�����M���ꂽ�Ƃ��ɉ�ʂ��X�V����Ȃ�
		comments.SingleOrDefault()?.SetActive(false);
		comments.SingleOrDefault()?.SetActive(true);
	}

	public void Send(string comment)
	{
		if (!IsOwner) return;

		TMP_InputField field = commentField.GetComponent<TMP_InputField>();
		if (field.text == string.Empty) return;
		if (field.text.Length > 20) return;
		field.text = string.Empty;

		client ??= GetComponent<Client>();
		SendServerRpc(client.UserName.Value.ToString(), comment);
	}

	[ServerRpc]
	private void SendServerRpc(string userName, string comment)
	{
		ClientManager.AddCommentForEachClients(userName, comment);
	}

	[ClientRpc]
	public void AddCommentClientRpc(string userName, string comment)
	{
		if (!IsOwner) return;
		commentView ??= GameObject.Find("CommentView");

		commentLv1.InstantiateAsync(commentsParent.transform).Completed += handle =>
		{
			GameObject added = handle.Result;
			added.Child("UserName").GetComponent<TMP_Text>().text = userName;
			added.Child("Content").GetComponent<TMP_Text>().text = comment;

			comments.Enqueue(added);

			if (comments.Count > 40)
			{
				commentLv1.ReleaseInstance(comments.Dequeue());
			}
		};
	}
}
