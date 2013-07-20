using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to Players.
/// 
/// This script accesses the PlayerDatabase script
/// in the GameManager to access the PlayerList.
/// 
/// This script accesses the SpawnScript to check which
/// team this player is on.
/// 
/// This script accesses the ScoreTable script for 
/// incrementing the team score.
/// 
/// This script is accessed by the HealthAndDamage script.
/// </summary>


public class PlayerScore : MonoBehaviour {

	public string destroyedEnemyName;
	
	public bool iDestroyedAnEnemy = false;
	
	public int enemiesDestroyedInOneHit;
	
	public int myScore;

	// Use this for initialization
	void Start () 
	{
		if(networkView.isMine == true)
		{
			GameObject gameManager = GameObject.Find("GameManager");
			
			PlayerDatabase dataScript = gameManager.GetComponent<PlayerDatabase>();
			
			for(int i = 0; i < dataScript.PlayerList.Count; i++)
			{
				if(dataScript.PlayerList[i].networkPlayer == int.Parse(Network.player.ToString()))
				{
					myScore = dataScript.PlayerList[i].playerScore;
					UpdateScoreInPlayerDatabase(myScore);
				}
			}
		}
		else
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(iDestroyedAnEnemy == true)
		{
			for(int i = 0; i < enemiesDestroyedInOneHit; i++)
			{
				myScore++;
				
				UpdateScoreInPlayerDatabase(myScore);
				
				TellScoreTableToUpdateTeamScore();
			}
			enemiesDestroyedInOneHit = 0;
			iDestroyedAnEnemy = false;
		}
	}
	void UpdateScoreInPlayerDatabase (int score)
	{
		GameObject gameManager = GameObject.Find("GameManager");
		PlayerDatabase dataScript = gameManager.GetComponent<PlayerDatabase>();	
		dataScript.scored = true;
		dataScript.playerScore = score;
	}
	
	void TellScoreTableToUpdateTeamScore ()
	{
		GameObject spawnManager = GameObject.Find("SpawnManager");
		SpawnScript spawnScript = spawnManager.GetComponent<SpawnScript>();
		GameObject gameManager = GameObject.Find("GameManager");
		ScoreTable tableScript = gameManager.GetComponent<ScoreTable>();
		tableScript.enemiesDestroyedInOneHit = enemiesDestroyedInOneHit;
	}
}
