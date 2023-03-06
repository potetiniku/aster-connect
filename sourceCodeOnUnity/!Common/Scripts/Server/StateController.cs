using AsterConnect.Model;
using AsterConnect.Model.Connection;
using UnityEngine;

public class StateController : MonoBehaviour
{
	public static ServerState CurrentState = ServerState.Down;

	private void Start()
	{
		Server.OnSignalReceive[Request.PrepareLive] = PrepareLive;
		Server.OnSignalReceive[Request.StartLive] = StartLive;
		Server.OnSignalReceive[Request.StopLive] = StopLive;
		Server.OnSignalReceive[Request.GetServerState] = GetCurrentState;

#if UNITY_EDITOR
		PrepareLive();
#endif
	}

	private Response PrepareLive()
	{
		switch (CurrentState)
		{
			case ServerState.Down:
				CurrentState = ServerState.Prepared;
				return Response.Succeeded;
			case ServerState.Prepared:
				return Response.AlreadyThatState;
			case ServerState.Streaming:
				return Response.Invalid;
			default:
				return Response.Failed;
		}
	}

	private Response StartLive()
	{
		switch (CurrentState)
		{
			case ServerState.Down:
				return Response.Invalid;
			case ServerState.Prepared:
				CurrentState = ServerState.Streaming;
				ClientManager.ShouldLoadStage = true;
				return Response.Succeeded;
			case ServerState.Streaming:
				return Response.AlreadyThatState;
			default:
				return Response.Failed;
		}
	}

	private Response StopLive()
	{
		switch (CurrentState)
		{
			case ServerState.Down:
				return Response.AlreadyThatState;
			case ServerState.Prepared:
				return Response.Invalid;
			case ServerState.Streaming:
				CurrentState = ServerState.Down;
				ClientManager.ShouldLoadTitle = true;
				return Response.Succeeded;
			default:
				return Response.Failed;
		}
	}

	private Response GetCurrentState()
	{
		return CurrentState switch
		{
			ServerState.Down => Response.ServerIsDown,
			ServerState.Prepared => Response.ServerIsPrepared,
			ServerState.Streaming => Response.ServerIsStreaming,
			_ => Response.Failed
		};
	}
}
