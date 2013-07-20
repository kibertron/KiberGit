using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreTable : MonoBehaviour {
	public bool showScoreTable = false;
	public List<PlayerDataClass> SortingList = new List<PlayerDataClass>();
	private GUIStyle myStyle = new GUIStyle();
	private GUIStyle HeaderStyle = new GUIStyle();
	public bool updateScore = false;
	public int enemiesDestroyedInOneHit;
	public bool serverRefreshScore = false;
	
	public int greenTeamScore;
	
	private GUIStyle winStyle = new GUIStyle();
	public bool greenTeamHasWon = false;
	public int winningScore;
	public int waitTime = 7;
	
	// Use this for initialization
	void Start () 
	{
		myStyle.fontStyle = FontStyle.Bold;
		myStyle.normal.textColor = Color.white;
		HeaderStyle.fontSize = 16;
		HeaderStyle.fontStyle = FontStyle.Bold;
		HeaderStyle.normal.textColor = Color.green;
		
		winStyle.fontSize = 40;
		winStyle.normal.textColor = Color.white;
		winStyle.fontStyle = FontStyle.Bold;
		winStyle.alignment = TextAnchor.MiddleCenter;

		GameObject multiManager = GameObject.Find("MultiplayerManager");
		GTGDMultiplayerScript multiScript = multiManager.GetComponent<GTGDMultiplayerScript>();
		winningScore = multiScript.winningScore;
	}
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButton("Show Player Scores"))
		{
			showScoreTable = true;	
		}
		
		if(Input.GetButtonUp("Show Player Scores"))
		{
			showScoreTable = false;	
		}
		
		if(updateScore == true)
		{
			for(int i = 0; i < enemiesDestroyedInOneHit; i++)
			{
				networkView.RPC("UpdateGreenScore", RPCMode.All);	
			}
			enemiesDestroyedInOneHit = 0;
			updateScore = false;
		}
		if(Network.isServer && serverRefreshScore == true)
		{
			networkView.RPC("ServerRefreshScore", RPCMode.AllBuffered, 
			                greenTeamScore);	
			serverRefreshScore = false;
		}
		if(greenTeamScore >= winningScore)
		{
			greenTeamHasWon = true;	
		}
		
	}
	
	void OnGUI ()
	{
		if(showScoreTable == true)
		{
			SortingList.Clear();
			PlayerDatabase dataScript = transform.GetComponent<PlayerDatabase>();
			for(int i = 0; i < dataScript.PlayerList.Count; i++)
			{
				SortingList.Add(dataScript.PlayerList[i]);	
			}
			SortingList.Sort(delegate(PlayerDataClass player1, PlayerDataClass player2)
			{
				return player1.playerScore.CompareTo(player2.playerScore);
			});
			
			GUI.Box(new Rect(Screen.width / 2 - 260, 10, 520, 30),"");
			GUI.Label(new Rect(Screen.width / 2 - 150, 15, 300, 30), "Score Table", myStyle);
			
			GUILayout.BeginArea(new Rect(Screen.width / 2 - 260, 50, 250, Screen.height - 10));
			GUILayout.BeginVertical("box");			
			GUILayout.BeginHorizontal("");
			GUILayout.Label("Players Score" , HeaderStyle, GUILayout.Width(200));
			GUILayout.Label(greenTeamScore.ToString(), HeaderStyle, GUILayout.Width(40));
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();
			
			for(int i = SortingList.Count - 1; i >= 0; i--)
			{
					GUILayout.BeginHorizontal("box");
					GUILayout.Label(SortingList[i].playerName, myStyle, GUILayout.Width(200));	
					GUILayout.Label(SortingList[i].playerScore.ToString(), myStyle, GUILayout.Width(40));	
					GUILayout.EndHorizontal();
			}
			GUILayout.EndArea();
			/*
	
			GUILayout.BeginArea(new Rect(Screen.width / 2 + 10, 50, 250, Screen.height - 10));
			GUILayout.BeginVertical("box");
			GUILayout.BeginHorizontal("");
			GUILayout.Label("Blue Team" , blueHeaderStyle, GUILayout.Width(200));
			GUILayout.Label(blueTeamScore.ToString(), blueHeaderStyle, GUILayout.Width(40));
			GUILayout.EndHorizontal();
			GUILayout.EndVertical();

			for(int i = SortingList.Count - 1; i >= 0; i--)
			{
				if(SortingList[i].playerTeam == "blue")
				{
					GUILayout.BeginHorizontal("box");
					GUILayout.Label(SortingList[i].playerName, myStyle, GUILayout.Width(200));
					GUILayout.Label(SortingList[i].playerScore.ToString(), myStyle, GUILayout.Width(40));
					GUILayout.EndHorizontal();
				}
			}
			GUILayout.EndArea();
			*/
		}
		if(greenTeamHasWon == true)
		{
			GUI.Box(new Rect(0,0, Screen.width, Screen.height),"");
			
			GUI.Box(new Rect(0,0, Screen.width, Screen.height), "Green Team has won the match!", winStyle);
			
			/*
			if(Network.isServer)
			{
				StartCoroutine(RestartMatch());	
			}
			*/
		}
		
	}
	[RPC]
	void UpdateGreenScore ()
	{
		greenTeamScore++;
		serverRefreshScore = true;
	}
	[RPC]
	void ServerRefreshScore (int greenScore)
	{
		greenTeamScore = greenScore;
	}
/*	
	void RestartGame ()
	{
		GameObject reload = GameObject.Find("ReloadLevel");
		
		ReloadLevelScript reloadScript = reload.GetComponent<ReloadLevelScript>();
		
		reloadScript.reloadLevel = true;
	}
	IEnumerator RestartMatch ()
	{
		yield return new WaitForSeconds(waitTime);
		
		RestartGame ();
	}
	*/
}
