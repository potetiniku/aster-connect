using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Random = UnityEngine.Random;

public class GrabbableYell : Yell
{
	[SerializeField] private AssetReferenceT<GameObject> giverNameUI;

	public override void Initialize(Size size, string giverName)
	{
		base.Initialize(size, giverName);

		Rigidbody rigidbody = GetComponent<Rigidbody>();
		rigidbody.useGravity = false;

		transform.position = ComputePosition(size);

		float height = GetComponent<Collider>().bounds.size.y;
		transform.position = new Vector3(
			transform.position.x, -height, transform.position.z);
		transform.DOMoveY(0, 0.25f);

		rigidbody.useGravity = true;

		_ = DespawnByLifeTime();

		static Vector3 ComputePosition(Size size)
		{
			return size switch
			{
				Size.Small => ComputeSMPosition(true),
				Size.Medium => ComputeSMPosition(false),
				Size.Large => Vector3.zero,
				_ => throw new Exception(),
			};

			static Vector3 ComputeSMPosition(bool shouldBeSmall)
			{
				while (true)
				{
					var position = new Vector3(
						Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f));

					if (!IsInCircle(position.x, position.z, 1)) continue;
					if (IsInSmallSizeArea(position) != shouldBeSmall) continue;

					const float stageRadius = 4;
					return position * stageRadius;
				}
			}

			static bool IsInSmallSizeArea(Vector3 position)
			{
				const float sSizeAreaRadius = 0.625f;
				const float sSizeAreaCenterZ = 1 - sSizeAreaRadius;
				return IsInCircle(position.x, sSizeAreaCenterZ - position.z, sSizeAreaRadius);
			}

			static bool IsInCircle(float x, float y, float r) =>
				Mathf.Pow(x, 2) + Mathf.Pow(y, 2) < Mathf.Pow(r, 2);
		}
	}

	private async UniTask DespawnByLifeTime()
	{
		await UniTask.Delay(ComputeLifeTime() * 1000);
		GetComponent<NetworkObject>().Despawn();

		int ComputeLifeTime() => Size switch
		{
			Size.Small => 120 + Random.Range(-10, 11),
			Size.Medium => 30 + Random.Range(-5, 6),
			_ => throw new Exception()
		};
	}

	private void OnTriggerEnter(Collider other)
	{
		if (IsServer) return;
		if (!other.gameObject.name.Contains("Hand")) return;
		ChangeOwnershipServerRpc(NetworkInitializer.Manager.LocalClientId);

		giverNameUI.InstantiateAsync(transform).Completed += handle =>
			handle.Result.GetComponent<GiverNameUI>().Initialize(GiverName);
	}

	[ServerRpc(RequireOwnership = false)]
	private void ChangeOwnershipServerRpc(ulong clientId)
	{
		GetComponent<NetworkObject>().ChangeOwnership(clientId);
	}

	/* // óLå¯âªÇ∑ÇÈÇ∆Ç»Ç∫Ç©åJÇËï‘Çµé¿çsÇ≥ÇÍë±ÇØÇƒÇµÇ‹Ç§
	private void OnTriggerExit(Collider other)
	{
		if (IsServer) return;
		if (!other.gameObject.name.Contains("Hand")) return;
		giverNameUI.ReleaseInstance(gameObject.Child("GiverNameUI"));
	}
	*/
}
