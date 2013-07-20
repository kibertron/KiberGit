using UnityEngine;
using System.Collections;

public class Connect : MonoBehaviour
{

    public string connectToIP = "127.0.0.1";
    public int connectPort = 25001;

    // Смешанный GUI для сервера и клиента
    public void OnGUI()
    {

        if (Network.peerType == NetworkPeerType.Disconnected)
        {
            //Сейчас мы отключены и не являемся клиентом или хостом
            GUILayout.Label("Connection status: Disconnected");

            connectToIP = GUILayout.TextField(connectToIP, GUILayout.MinWidth(100));
            connectPort = int.Parse(GUILayout.TextField(connectPort.ToString()));

            GUILayout.BeginVertical();
            if (GUILayout.Button("Connect as client"))
            {
                // Подсоединяемся к "connectToIP" и "connectPort" как клиент
                // В данном случае игнорируем NAT
                Network.useNat = false;
                Network.Connect(connectToIP, connectPort);
            }

            if (GUILayout.Button("Start Server"))
            {
                // Создаем север с 32 клиентами используя порт "connectPort" 
                // Так же игнорируем NAT
                Network.useNat = false;
                Network.InitializeServer(32, connectPort);
            }
            GUILayout.EndVertical();


        }
        else
        {
            //Мы имеем подключение(я)!


            if (Network.peerType == NetworkPeerType.Connecting)
            {
                // Статус - пдключение
                GUILayout.Label("Connection status: Connecting");

            }
            else if (Network.peerType == NetworkPeerType.Client)
            {
                // Статус - клиент
                GUILayout.Label("Connection status: Client!");
                GUILayout.Label("Ping to server: " + Network.GetAveragePing(Network.connections[0]));

            }
            else if (Network.peerType == NetworkPeerType.Server)
            {
                // Статус - сервер
                GUILayout.Label("Connection status: Server!");
                GUILayout.Label("Connections: " + Network.connections.Length);
                if (Network.connections.Length == 1)
                {
                    GUILayout.Label("Ping to first player: " + Network.GetAveragePing(Network.connections[0]));
                }
            }

            if (GUILayout.Button("Disconnect"))
            {
                Network.Disconnect(200);
            }
        }


    }
}