using UnityEngine;
using System.Collections;

public class AssignHealth : MonoBehaviour 
{
	private GameObject[] AllPlayers;
	private float waitTime = 5;

	void OnConnectedToServer ()
	{
		StartCoroutine(AssignHealthOnJoiningGame());	
	}

	IEnumerator AssignHealthOnJoiningGame()
	{
		yield return new WaitForSeconds (waitTime);
		
		GameObject spawnManager = GameObject.Find("SpawnManager");
		SpawnScript spawnScript = spawnManager.GetComponent<SpawnScript>();
		
		if(spawnScript.firstSpawn == true)
		{
			AllPlayers = GameObject.FindGameObjectsWithTag("Enemy");
		
		foreach(GameObject all in AllPlayers)
		{
			paramHero PHScript = all.GetComponent<paramHero>();	
			
			PHScript.curHealth = PHScript.previousHealth;
		}
			spawnScript.firstSpawn = false;
		}
	//один раз применяется при присоединении к серверу и отключаем
		enabled = false;
	}
	
}
