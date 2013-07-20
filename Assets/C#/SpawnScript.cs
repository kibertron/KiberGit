using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour
{
	private bool justConnectedToServer = false;
	private bool justServerConnect = false;
	private GameObject[] ourSpawnPoints;
	public bool iAmDestroyed = false;
	public bool firstSpawn = false;
    public GameObject playerPrefab;
	public GameObject cameraPrefab;
	public GameObject lightpref;
	public GameObject minimapcam;
	int countPlayers = 0;
	
	//массивы под ловушки и их позиции
	public Transform[] trapPoint;
	public Transform[] trapTipes;
    //массивы под бонусы и их позиции
    public float timerBaseBonusTime = 40.0f;//базовое значение таймера 
    public float timer;//понадобится для создания задержки во время атаки 
    public GameObject[] bonusPoint;
    public GameObject[] bonusType;
	public GameObject[] TeleportPoint;
	//private bool InitializationBonus = false; 
	
     void OnServerInitialized()
    {		
		justServerConnect = true;
    //    Spawnplayer();
		Shuffle<Transform>(trapPoint); // ловушки
		for(int i=0;i<6;i++){
			 Network.Instantiate(trapTipes[i],trapPoint[i].position,Quaternion.identity,0);
		}
        Shuffle<GameObject>(bonusPoint);
        for (int i = 0; i < bonusType.Length; i++) // бонусы
        {
            Network.Instantiate(bonusType[i].transform, bonusPoint[i].transform.position, Quaternion.identity, 0);
        }
		for (int i = 0; i < TeleportPoint.Length; i++) // бонусы
        {
            Network.Instantiate(TeleportPoint[i].transform, TeleportPoint[i].transform.position, Quaternion.identity, 0);
        }
		//InitializationBonus = true;
    }

    void OnConnectedToServer()
    {	
		justConnectedToServer = true;
		countPlayers++;
		justServerConnect = false;
        Spawnplayer();
		firstSpawn = true;
    }

    public void Spawnplayer()
    {
		//if(!justServerConnect)
		//{
		ourSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
        GameObject randomSpawn = ourSpawnPoints[Random.Range(0, ourSpawnPoints.Length)];
        Network.Instantiate(playerPrefab, randomSpawn.transform.position, randomSpawn.transform.rotation, 0);

		Camera.Instantiate(cameraPrefab);
		cameraPrefab.transform.position = new Vector3(0,230,-40);
		cameraPrefab.transform.eulerAngles = new Vector3(80,0,0);
		
		Light.Instantiate(lightpref);
		lightpref.transform.position = new Vector3(0,80,0);
		lightpref.transform.eulerAngles = new Vector3(90,0,0);
		
		Camera.Instantiate(minimapcam);
		minimapcam.transform.position = new Vector3(0,600,0);
		minimapcam.transform.eulerAngles = new Vector3(90,0,0);
		
		GameObject gameManager = GameObject.Find("GameManager");
		PlayerDatabase dataScript = gameManager.GetComponent<PlayerDatabase>();
		dataScript.joinedID = true;
		dataScript.playerID = countPlayers;
	//	}
	}
    
	
     void OnPlayerDisconnected(NetworkPlayer player)
    {
        // Чистим за игроком
        Debug.Log("Clean up after player " + player);
		justConnectedToServer = false;
  //      Network.RemoveRPCs(player);
   //     Network.DestroyPlayerObjects(player);

    }

     void OnDisconnectedFromServer()
    {
        // При отключении от сервера чистим за собой
        Debug.Log("Clean up a bit after server quit");
		justServerConnect = false;
		justConnectedToServer = false;
      //  Network.RemoveRPCs(Network.player);
	//	networkView.RPC("DestroySelf", RPCMode.All);
       // Network.DestroyPlayerObjects(Network.player);
	 
        /*
         * Запомните, что мы удаляем только наши объекты и не можем удалить объекты других игроков
         * т.к. мы не знаем где они и мы не следим за ними.
         * В игре обычно вы должны перезагрузить уровень или загрузить уровень с главным меню ;).
         * Сейчас мы можем использовать здесь "Application.LoadLevel(Application.loadedLevel);" для сброса сцены.
        */
	//	Application.LoadLevel(Application.loadedLevel);
   }

	
    void Start() {
        timer = timerBaseBonusTime;
    }
    void Update()
    {
        if (timer > 0)
            timer -= Time.deltaTime;
        if (timer < 0)
            timer = 0;//создали работу таймера(Позаимствовано из урока romaan27 :) ) 

  /*      if (GameObject.FindGameObjectsWithTag("bonusType") != null && timer == 0)//??? ошибка RPC исправить
        {
            networkView.RPC("SpawBonus", RPCMode.AllBuffered);
            timer = timerBaseBonusTime;//восстановили работу таймера 
        }
        */
    }
    // Bonus 
    [RPC]
    void SpawBonus()
    {
        GameObject[] bonusTypes = GameObject.FindGameObjectsWithTag("bonusType");
        Shuffle<GameObject>(bonusPoint);
        for (int i = 0; i < bonusTypes.Length; i++)
        {
            print(bonusPoint[i]);

            bonusTypes[i].transform.position = bonusPoint[i].transform.position;
            bonusTypes[i].transform.rotation = bonusPoint[i].transform.rotation;
            bonusTypes[i].collider.enabled = true;
            bonusTypes[i].renderer.enabled = true;
        }
    }	
	
	public  void Shuffle<T>(T[] array)
  {
    var rand = new System.Random();
    for (int i = array.Length; i > 0; i--)
    {
        int j = rand.Next(i);
        T tmp = array[j];
        array[j] = array[i - 1];
        array[i - 1] = tmp;
    }
  }

}
