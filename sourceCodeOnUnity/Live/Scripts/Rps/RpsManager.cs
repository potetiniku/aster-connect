using System.Collections.Generic;
using AsterConnect.Model.MainApp;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class RpsManager : NetworkBehaviour
{
	[SerializeField] private AssetReferenceGameObject rock;
	private GameObject rockObject;
	[SerializeField] private AssetReferenceGameObject paper;
	private GameObject paperObject;
	[SerializeField] private AssetReferenceGameObject scissor;
	private GameObject scissorObject;

	public NetworkVariable<Hand> SelectedHand = new(Hand.Rock);
	public static readonly List<string> WinnerNames = new();

	[ClientRpc]
	public void StartClientRpc(ulong instantiateClientId)
	{
		if (!IsOwner) return;

		if (RoleSwitch.Role == Role.Client)
			LiveUI.Current.RpsUI.ShowTitle();

		// 色々やり方があったが、この方法でないとうまくいかなかった
		if (OwnerClientId == instantiateClientId)
			InstantiatePlatesServerRpc();
	}

	[ServerRpc]
	private void InstantiatePlatesServerRpc()
	{
		Instantiate(ref rockObject, rock, 0.25f);
		Instantiate(ref paperObject, paper, 0);
		Instantiate(ref scissorObject, scissor, -0.25f);

		static void Instantiate(ref GameObject plateObject, AssetReferenceGameObject plate, float xPosition)
		{
			if (plateObject != null) return;
			plateObject = plate.InstantiateAsync(new(xPosition, 1, 1), Quaternion.identity).WaitForCompletion();
			plateObject.GetComponent<NetworkObject>().Spawn();
		}
	}

	[ClientRpc]
	public void StartRoundClientRpc()
	{
		if (!IsOwner) return;
		LiveUI.Current.RpsUI.StartRound();
	}

	[ServerRpc]
	public void SelectHandServerRpc(Hand hand)
	{
		SelectedHand.Value = hand;
	}

	[ClientRpc]
	public void ShowResultClientRpc(Hand presenter)
	{
		if (!IsOwner) return;
		if (RoleSwitch.Role == Role.Controller) return;

		RpsResult result = SelectedHand.Value.GetResult(presenter);
		if (result == RpsResult.Win)
			AddToWinnersServerRpc(Client.TempUserName);

		LiveUI.Current.RpsUI.ShowResult(result);
	}

	[ServerRpc]
	private void AddToWinnersServerRpc(string winnerName)
	{
		WinnerNames.Add(winnerName);
	}

	[ClientRpc]
	public void AddWinnerClientRpc(string name)
	{
		if (!IsOwner) return;
		LiveUI.Current.RpsUI.RankingBoard.Winners.Add(name);
	}

	[ClientRpc]
	public void ShowRankingBoardClientRpc()
	{
		if (!IsOwner) return;
		LiveUI.Current.RpsUI.RankingBoard.Show();
	}

	[ClientRpc]
	public void EndClientRpc(ulong despawnClientId)
	{
		if (!IsOwner) return;

		if (RoleSwitch.Role == Role.Client)
			LiveUI.Current.RpsUI.InsertEndText();

		_ = LiveUI.Current.RpsUI.RankingBoard.Hide();

		if (OwnerClientId == despawnClientId)
			DespawnPlateServerRpc();
	}

	[ServerRpc]
	private void DespawnPlateServerRpc()
	{
		rockObject.GetComponent<NetworkObject>().Despawn(false);
		paperObject.GetComponent<NetworkObject>().Despawn(false);
		scissorObject.GetComponent<NetworkObject>().Despawn(false);

		rock.ReleaseInstance(rockObject);
		paper.ReleaseInstance(paperObject);
		scissor.ReleaseInstance(scissorObject);
	}
}
