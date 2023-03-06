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
    // �T�[�o�Ŏ��s�����ꍇ��ServerAddress�ɒl���������邱�Ƃ͂Ȃ�
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
