using System;
using AsterConnect.Model.MainApp;
using PoseAI;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AvatarLifeCycle : NetworkBehaviour
{
	private static ushort nextControllerPort = 39828;

	private void Start()
	{
		if (!IsOwner) return;

		if (RoleSwitch.Role == Role.Controller)
		{
			InstantiateServerRpc(AvatarConfig.AssetPath,
				AvatarConfig.Mode, AvatarConfig.UseUpperBodyOnly);
		}
	}

	[ServerRpc]
	private void InstantiateServerRpc(string assetPath, PoseAI_Modes mode, bool useUpperBodyOnly)
	{
		Addressables.InstantiateAsync(assetPath).Completed += handle =>
		{
			GameObject avatar = handle.Result;
			PoseAISourceDirect source = avatar.GetComponent<PoseAISourceDirect>();
			source.Port = nextControllerPort;
			source.Mode = mode;
			source.enabled = true;
			avatar.GetComponent<PoseAICharacterAnimator>().UseUpperBodyOnly = useUpperBodyOnly;
			avatar.GetComponent<NetworkObject>().SpawnWithOwnership(OwnerClientId);
			avatar.GetComponent<Avatar>().poseAIPort.Value = nextControllerPort;
			nextControllerPort++;
		};

		gameObject.GetComponent<Client>().AvatarPort.Value = nextControllerPort;
		ShowPortNumberClientRpc(nextControllerPort);
	}

	[ClientRpc]
	private void ShowPortNumberClientRpc(ushort portNumber)
	{
		if (!IsOwner) return;
		_ = GameObject.Find("UISwitcher")
			.GetComponent<UISwitcher>()
			.ShowPortNumber(portNumber);
	}
}
