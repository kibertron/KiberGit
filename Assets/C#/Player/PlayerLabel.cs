using UnityEngine;
using System.Collections;

public class PlayerLabel : MonoBehaviour 
{
	
	public Texture healthTex;
	private Camera myCamera;
	private Transform myTransform;
	private paramHero PHScript;

	private Vector3 worldPosition  = new Vector3();	
	private Vector3 screenPosition  = new Vector3();	
	private Vector3 cameraRelativePosition = new Vector3();
	
	private float minimumZ = 1.5f;
	private int labelTop = 18;
	private int labelWidth = 110;
	private int labelHeight = 15;
	private int barTop = 1;
	private int healthBarHeight = 5;
	private int healthBarLeft = 110;
	private float healthBarLength;
	private float adjustment = 1;
	public string playerName;
	private GUIStyle myStyle = new GUIStyle();
	
	void Awake ()
	{
		if(networkView.isMine == false)
		{
			myTransform = transform;
			myCamera = Camera.main;
			PHScript = transform.GetComponent<paramHero>();				
			myStyle.normal.textColor = Color.red;	
			myStyle.fontSize = 12;
			myStyle.fontStyle = FontStyle.Bold;			
			myStyle.clipping = TextClipping.Overflow;
		}
		else
		{
			enabled = false;	
		}
	}
	// Update is called once per frame
	void Update () 
	{	
		cameraRelativePosition = myCamera.transform.InverseTransformPoint(myTransform.position);
		if(PHScript.curHealth < 1)
		{
			healthBarLength = 1;	
		}
		if(PHScript.curHealth >= 1)
		{
			healthBarLength = ((float)PHScript.curHealth / (float)PHScript.maxHealth) * 100;	
		}
	}
	void OnGUI ()
	{
		if(cameraRelativePosition.z > minimumZ)
		{	//Установка мирового пространства в точку над игроком
			worldPosition = new Vector3(myTransform.position.x, myTransform.position.y + adjustment,
			                            myTransform.position.z);
			
			//Преобразование мирового пространства в точку на экране
			screenPosition = myCamera.WorldToScreenPoint(worldPosition);
			GUI.Box(new Rect(screenPosition.x - healthBarLeft / 2, 
			                 Screen.height - screenPosition.y - barTop,
			                 100, healthBarHeight), "");
			GUI.DrawTexture(new Rect(screenPosition.x - healthBarLeft / 2,
			                         Screen.height - screenPosition.y - barTop,
			                         healthBarLength, healthBarHeight), healthTex);		
			GUI.Label(new Rect(screenPosition.x - labelWidth / 2,
			                   Screen.height - screenPosition.y - labelTop,
			                   labelWidth, labelHeight), playerName, myStyle);

		}
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
}
