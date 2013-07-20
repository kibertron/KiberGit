using UnityEngine;
using System.Collections;



public class Connect : MonoBehaviour
{   private static int portcounter=0;
	private string gameName = "Blood Key";
	public string GameName="обязательно введите имя игры";
	private bool refreshing=true;
	private HostData [] hostData ;

    public string connectToIP = "10.0.136.75";
    public int connectPort = 25002;
	/*
	void Awake(){
	//как бы для удаленного мастер сервера
		MasterServer.ipAddress = "127.0.0.1";
		MasterServer.port = 23466;
        Network.natFacilitatorIP = "127.0.0.1";
        Network.natFacilitatorPort = 50005;
		}*/
	
	void Update(){
		if(refreshing==true){
			if(MasterServer.PollHostList().Length>0){
				refreshing=false;
				hostData = MasterServer.PollHostList();
			}
		}
	}
	void OnMasterServerEvent(MasterServerEvent mse){
		if(mse==MasterServerEvent.RegistrationSucceeded){
			Debug.Log ("register Server");
		}
	}
    // Смешанный GUI для сервера и клиента
    public void OnGUI()
    {
		if(!Network.isClient && !Network.isServer){
			GameName=GUI.TextField(new Rect(Screen.width/2 -90,Screen.height/2 -140,140,30),GameName);
			if(GUI.Button(new Rect(Screen.width/2 -90,Screen.height/2 -100,140,50),"start Server")){
				startServer();
			}
			if(GUI.Button(new Rect(Screen.width/2 -90,Screen.height/2 -30,140,50),"Refresh")){
				Debug.Log("Refresh");
				refreshHostList();
			}
			if(hostData!=null){
				if(hostData.Length!=0){
					for(int i=0;i<hostData.Length;i++){
						if(GUI.Button (new Rect(50,100*(i+1),100,50),hostData[i].gameName)){
							Network.Connect (hostData[i]);
							Debug.Log ("Connected");
						}
					}
				}
			}
	}
				
		GUILayout.Space(100);

       /* if (Network.peerType == NetworkPeerType.Disconnected)
        {
            //Сейчас мы отключены и не являемся клиентом или хостом
            GUILayout.Label("Connection status: Disconnected");

            connectToIP = GUILayout.TextField(connectToIP, GUILayout.MinWidth(100));
            connectPort = int.Parse(GUILayout.TextField(connectPort.ToString()));

            GUILayout.BeginVertical();
           /* if (GUILayout.Button("Connect as client"))
            {
                // Подсоединяемся к "connectToIP" и "connectPort" как клиент
                // В данном случае игнорируем NAT
                Network.useNat = false;
                Network.Connect(connectToIP, connectPort);
            }
			 */
           /* if (GUILayout.Button("Start Server"))
            {
				startServer();
            }
            GUILayout.EndVertical();
		 

        }*/
		// Все что ниже лучше вынести в меню текущей статистики
      /*  else
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
		 */


    }		
		void refreshHostList(){
			MasterServer.RequestHostList(gameName);
			refreshing =true;
			Debug.Log (MasterServer.PollHostList().Length);
		}
		void startServer(){
		//portcounter для запуска нескольких серверов на одном ip( изменяя порт)
			Network.InitializeServer(3, connectPort+portcounter,true);
		   // portcounter++;
			MasterServer.RegisterHost(gameName,GameName,"this is comment");
			Debug.Log("server is created");
		}
}