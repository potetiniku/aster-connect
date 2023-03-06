using System;
using System.Collections.Generic;
using AsterConnect.Model.Connection;
using AsterConnect.Model.MainApp;
using UnityEngine;

public class Server : MonoBehaviour
{
	// サーバ側アドレスは0.0.0.0にしておくと確実。
	// また、サーバと同じネットワークからだと何故かグローバルIPアドレスで接続できない。
	// いずれも、Netcode for Gameobjectsでも同様。
	[SerializeField]
	private string ipAddress = "0.0.0.0";

	[SerializeField]
	private int port = 5239;

	public static Dictionary<Request, Func<Response>> OnSignalReceive = new();
	private ServerSocket socket;

	private void Start()
	{
		if (RoleSwitch.Role != Role.Server)
		{
			Destroy(gameObject);
			return;
		}
		Listen();
	}

	private void OnApplicationQuit()
	{
		Stop();
	}

	public void Listen()
	{
		socket = new(ipAddress, port);
		socket.OnReceive += Response;
		socket.Start();
	}

	public void Stop()
	{
		socket.Dispose();
	}

	private string Response(string request)
	{
		Debug.Log("Request received: " + request);
		Request received = EnumExtensions.FromString<Request>(request);
		return OnSignalReceive[received]().ToString();
	}
}
