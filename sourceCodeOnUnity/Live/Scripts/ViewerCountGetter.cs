using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ViewerCountGetter : NetworkBehaviour
{
	private NetworkVariable<int> count = new();

	public int Count
	{
		get
		{
			UpdateViewerCountServerRpc();
			return count.Value;
		}
	}

	[ServerRpc]
	private void UpdateViewerCountServerRpc()
	{
		// �v���[���^�̕�������
		count.Value = NetworkManager.Singleton.ConnectedClients.Count - 1;
	}
}
