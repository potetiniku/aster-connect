using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class RpsPlate : NetworkBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (IsServer) return;
		if (!other.gameObject.name.Contains("Hand")) return;
		ChangeOwnershipServerRpc(NetworkInitializer.Manager.LocalClientId);
	}

	[ServerRpc(RequireOwnership = false)]
	private void ChangeOwnershipServerRpc(ulong clientId)
	{
		GetComponent<NetworkObject>().ChangeOwnership(clientId);
	}
}
