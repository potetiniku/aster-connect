using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class Client : NetworkBehaviour
{
	public static string TempUserName;
	public NetworkVariable<FixedString64Bytes> UserName;

	private NetworkObject networkObject;
	public NetworkVariable<ushort> AvatarPort = new();

	private static readonly Queue<Action<Client>> shouldCalls = new();

	private Avatar mine => FindObjectsOfType<Avatar>()
		.SingleOrDefault(a => a.poseAIPort.Value == AvatarPort.Value);

	private void Start()
	{
		networkObject = GetComponent<NetworkObject>();

		if (!IsOwner) return;
		SetUserNameServerRpc(TempUserName);
	}

	private void Update()
	{
		if (!IsOwner) return;

		while (shouldCalls.Any())
		{
			shouldCalls.Dequeue()(this);
		}
	}

	public static void DestroyMine()
	{
		shouldCalls.Enqueue(c =>
		{
			c.DestroyServerRpc();
			c.mine?.DestroyServerRpc();
		});
	}

	[ClientRpc]
	public void LoadStageClientRpc()
	{
		// RPCのメソッドはasyncにできない
#pragma warning disable CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます
		GetSceneLoader().LoadStage();
#pragma warning restore CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます
	}

	[ClientRpc]
	public void LoadTitleClientRpc()
	{
		// RPCのメソッドはasyncにできない
#pragma warning disable CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます
		GetSceneLoader().LoadTitle();
#pragma warning restore CS4014 // この呼び出しは待機されなかったため、現在のメソッドの実行は呼び出しの完了を待たずに続行されます
	}

	[ServerRpc]
	private void DestroyServerRpc()
	{
		networkObject.Despawn();
	}

	[ServerRpc]
	private void SetUserNameServerRpc(string userName)
	{
		UserName.Value = userName;
	}

	private SceneLoader GetSceneLoader()
	{
		return GameObject.Find("Transition").GetComponent<SceneLoader>();
	}
}
