using UnityEngine;
using System.Collections;

public class SpawnScript : MonoBehaviour
{
    public Transform playerPrefab;


    public void OnServerInitialized()
    {
        Spawnplayer();
    }

    public void OnConnectedToServer()
    {
        Spawnplayer();
    }

    public void Spawnplayer()
    {

        Transform myNewTrans = (Transform)Network.Instantiate(
            playerPrefab, transform.position, transform.rotation, 0);

    }

    public void OnPlayerDisconnected(NetworkPlayer player)
    {
        // Чистим за игроком
        Debug.Log("Clean up after player " + player);
        Network.RemoveRPCs(player);
        Network.DestroyPlayerObjects(player);
    }

    public void OnDisconnectedFromServer(NetworkDisconnection info)
    {
        // При отключении от сервера чистим за собой
        Debug.Log("Clean up a bit after server quit");
        Network.RemoveRPCs(Network.player);
        Network.DestroyPlayerObjects(Network.player);

        /* 
         * Запомните, что мы удаляем только наши объекты и не можем удалить объекты других игроков
         * т.к. мы не знаем где они и мы не следим за ними.
         * В игре обычно вы должны перезагрузить уровень или загрузить уровень с главным меню ;).
         * Сейчас мы можем использовать здесь "Application.LoadLevel(Application.loadedLevel);" для сброса сцены.
         */
        Application.LoadLevel(Application.loadedLevel);
    }
}