using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using AsterConnect.Model.Connection;
using UnityEngine;

public class ServerSocket : IDisposable
{
	public ServerSocket(string ipAddress, int port)
	{
		IpAddress = ipAddress;
		Port = port;
	}

	public string IpAddress { get; set; }
	public int Port { get; set; }

	public Func<string, string> OnReceive;

	private TcpListener listener;
	private TcpClient client;
	private NetworkStream networkStream;

	public void Start()
	{
		Task.Run(() =>
		{
			while (true) Communicate();
		});
	}

	/// <summary>
	/// クライアント側から通信を監視し続け、切断されるまで通信し続ける
	/// </summary>
	private void Communicate()
	{
		var address = IPAddress.Parse(IpAddress);
		listener = new(address, Port);
		listener.Start();

		client = listener.AcceptTcpClient();
		networkStream = client.GetStream();

		byte[] buffer = new byte[1024 * 1024];
		networkStream.Read(buffer);
		string received = Encoding.UTF8.GetString(buffer);
		networkStream.Write(Encoding.UTF8.GetBytes(OnReceive(received)));
		Dispose();
	}

	public void Dispose()
	{
		networkStream?.Dispose();
		client?.Dispose();
		listener?.Stop();
	}
}
