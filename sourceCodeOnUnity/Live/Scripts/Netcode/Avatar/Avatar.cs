using AsterConnect.Model.MainApp;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Avatar : NetworkBehaviour
{
	[SerializeField] private GameObject[] hideBody;

	public NetworkVariable<ushort> poseAIPort = new();

	private GameObject mainCamera;

	private void Start()
	{
		if (!IsOwner) return;

		const float ratio = 0.5f;
		transform.localScale = Vector3.Scale(
			transform.localScale, new Vector3(ratio, ratio, ratio));

		// hideBody.ForEach(o => o.SetActive(false));

		mainCamera = GameObject.Find("Main Camera");
	}

	private void Update()
	{
		Vector3 target = mainCamera.transform.position;
		transform.position = new Vector3(target.x, 0, target.z);
	}

	public override void OnDestroy()
	{
		base.OnDestroy();

		if (RoleSwitch.Role == Role.Server)
		{
			Addressables.Release(gameObject);
		}
	}

	[ServerRpc]
	public void DestroyServerRpc()
	{
		GetComponent<NetworkObject>().Despawn();
	}
}
