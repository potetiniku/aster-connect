using System;
using System.Net;
using AsterConnect.Model;
using AsterConnect.Model.Connection;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class Entrance : MonoBehaviour
{
	[SerializeField]
	protected TMP_InputField serverAddressField;

	[SerializeField]
	protected OkDialog okDialog;

	protected Button buttonComponent;

	public virtual async void EnterLive()
	{
		buttonComponent.interactable = false;

		Response serverState = await UniTask.RunOnThreadPool(() =>
			Request.GetServerState.Send(
				IPAddress.Parse(serverAddressField.text)));

		if (serverState == Response.ConnectionFailed)
		{
			await okDialog.Show("エラー", "サーバーに接続できませんでした");
			buttonComponent.interactable = true;
			return;
		}

		ServerState currentState = serverState switch
		{
			Response.ServerIsDown => ServerState.Down,
			Response.ServerIsPrepared => ServerState.Prepared,
			Response.ServerIsStreaming => ServerState.Streaming,
			_ => throw new Exception("予期しない応答")
		};

		if (currentState == ServerState.Down)
		{
			await okDialog.Show("情報", "開催予定のライブはありません");
			buttonComponent.interactable = true;
			return;
		}

		NetworkInitializer.ServerAddress = serverAddressField.text;
		await SceneLoader.Instance.LoadLive(currentState);
	}

	public abstract void UpdateInteractable();

	private void Start()
	{
		buttonComponent = GetComponent<Button>();
		UpdateInteractable();
	}
}