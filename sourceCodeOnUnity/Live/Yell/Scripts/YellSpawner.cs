using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class YellSpawner : NetworkBehaviour
{
	public static YellSpawner Instance;

	private void Start()
	{
		if (!IsOwner) return;
		Instance = this;
	}

	[ServerRpc]
	public void SpawnServerRpc(string assetPath, Size size, string giverName)
	{
		Addressables.InstantiateAsync(assetPath).Completed += handle =>
		{
			GameObject gameObject = handle.Result;
			Yell component = gameObject.GetComponent<Yell>();
			component.Initialize(size, giverName);
			gameObject.GetComponent<NetworkObject>().Spawn();
		};
	}
}
