using System;
using System.Collections.Generic;
using AsterConnect.Model.Connection;
using AsterConnect.Model.MainApp;
using UnityEngine;

public class Server : MonoBehaviour
{
	// �T�[�o���A�h���X��0.0.0.0�ɂ��Ă����Ɗm���B
	// �܂��A�T�[�o�Ɠ����l�b�g���[�N���炾�Ɖ��̂��O���[�o��IP�A�h���X�Őڑ��ł��Ȃ��B
	// ��������ANetcode for Gameobjects�ł����l�B
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
