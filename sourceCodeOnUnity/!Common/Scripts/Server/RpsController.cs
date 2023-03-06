using System;
using System.Linq;
using AsterConnect.Model.Connection;
using Cysharp.Threading.Tasks;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// u‚İ‚ñ‚È‚Å! ‚¶‚á‚ñ‚¯‚ñv‚Ì–½—ß‚ğÀs‚·‚é
/// </summary>
public class RpsController : MonoBehaviour
{
	private void Start()
	{
		Server.OnSignalReceive[Request.StartRps] = StartRps;
		Server.OnSignalReceive[Request.StartRound] = StartRound;
		Server.OnSignalReceive[Request.Decide_Rock] = Decide_Rock;
		Server.OnSignalReceive[Request.Decide_Paper] = Decide_Paper;
		Server.OnSignalReceive[Request.Decide_Scissor] = Decide_Scissor;
		Server.OnSignalReceive[Request.ShowRankingBoard] = ShowRankingBoard;
		Server.OnSignalReceive[Request.EndGame] = EndGame;
	}

	private Response StartRps()
	{
		ExecuteEachClients(rps => rps.StartClientRpc(
			NetworkInitializer.Manager.ConnectedClientsList.First().ClientId));
		return Response.Succeeded;
	}

	private Response StartRound()
	{
		ExecuteEachClients(rps => rps.StartRoundClientRpc());
		return Response.Succeeded;
	}

	private Response Decide_Rock()
	{
		ExecuteEachClients(rps => rps.ShowResultClientRpc(Hand.Rock));
		return Response.Succeeded;
	}

	private Response Decide_Paper()
	{
		ExecuteEachClients(rps => rps.ShowResultClientRpc(Hand.Paper));
		return Response.Succeeded;
	}

	private Response Decide_Scissor()
	{
		ExecuteEachClients(rps => rps.ShowResultClientRpc(Hand.Scissor));
		return Response.Succeeded;
	}

	private Response ShowRankingBoard()
	{
		RpsManager.WinnerNames.ForEach(Debug.Log);
		ExecuteEachClients(rps =>
		{
			RpsManager.WinnerNames.ForEach(rps.AddWinnerClientRpc);
			rps.ShowRankingBoardClientRpc();
		});
		return Response.Succeeded;
	}

	private Response EndGame()
	{
		ExecuteEachClients(rps => rps.EndClientRpc(
			NetworkInitializer.Manager.ConnectedClientsList.First().ClientId));
		RpsManager.WinnerNames.Clear();

		return Response.Succeeded;
	}

	private void ExecuteEachClients(Action<RpsManager> action)
	{
		ClientManager.ApplyEachClients(c =>
			action(c.GetComponent<RpsManager>()));
	}
}
