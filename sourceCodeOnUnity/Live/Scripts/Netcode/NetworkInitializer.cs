using AsterConnect.Model.MainApp;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class NetworkInitializer : MonoBehaviour
{
    [SerializeField]
    private NetworkManager network;

    [SerializeField]
    private UnityTransport transport;

    public static NetworkManager Manager;
    // サーバで実行した場合はServerAddressに値が代入されることはない
    public static string ServerAddress = "127.0.0.1";

    private void Start()
    {
        Manager = network;
        transport.ConnectionData.Address = ServerAddress;

        if (RoleSwitch.Role == Role.Server)
        {
            Manager.StartServer();
        }
        else
        {
            Manager.StartClient();
        }
    }
}
