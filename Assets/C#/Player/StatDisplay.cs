using UnityEngine;
using System.Collections;

public class StatDisplay : MonoBehaviour {
	
	public Texture healthTex;
	private float health;
	private int healthForDisplay;
	private int boxWidth = 160;
	private int boxHeight = 85;	
	private int labelHeight = 20;	
	private int labelWidth = 35;	
	private int padding = 10;	
	private int gap = 120;	
	private float healthBarLength;	
	private int healthBarHeight = 15;	
	private GUIStyle healthStyle = new GUIStyle();	
	private float commonLeft;	
	private float commonTop;

	private paramHero PHScript;

	void Start () 
	{
		if(networkView.isMine == true)
		{

			PHScript = transform.GetComponent<paramHero>();			
			healthStyle.normal.textColor = Color.green;
			healthStyle.fontStyle = FontStyle.Bold;
		}
		else
		{
			enabled = false;	
		}			
	}
	
	// Update is called once per frame
	void Update () 
	{
		health = (float)PHScript.curHealth;
		healthForDisplay = Mathf.CeilToInt(health);
		healthBarLength = ((float)health /(float)PHScript.maxHealth) * 100;
	}

	void OnGUI ()
	{
		commonLeft = Screen.width / 2 + 180;		
		commonTop = Screen.height / 2 + 50;
		GUI.Box(new Rect(commonLeft, commonTop, boxWidth, boxHeight), "");
		GUI.Box(new Rect(commonLeft + padding, commonTop + padding, 100, healthBarHeight), "");
		GUI.DrawTexture(new Rect(commonLeft + padding, commonTop + padding, healthBarLength
		                         ,healthBarHeight), healthTex);
		GUI.Label(new Rect(commonLeft + gap, commonTop + padding, labelWidth, labelHeight),
		          healthForDisplay.ToString(), healthStyle);
	}
}
