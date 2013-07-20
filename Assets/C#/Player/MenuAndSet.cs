using UnityEngine;
using System.Collections;

public class MenuAndSet : MonoBehaviour {
	public GUIStyle welcomeLabel;   
	public GUISkin customSkin;    
public Rect playGameRect;     
public Rect optionsRect;     
public Rect quitRect;   
	
private bool optionsMode = false; 
private bool menuMode = true;   
private bool gameMode = false;	
	
public paramHero ourHero;  
public float tempCurHealth = 100;
public float tempCurExp = 0;
public int b;
	
void Awake(){
    DontDestroyOnLoad(this);
}	
	
void OnGUI() 
{   GUILayout.Space(1000);
		
if (Input.GetKey(KeyCode.Escape)){  
        menuMode = true;                
        optionsMode = false;  
       // Time.timeScale = 0; - отключение процесса игры
		if(gameMode){        // не останавливаем игру, но выводим кнопки выбора 
		// например, настройки звуков, новая игра - возвращает на сцену 1, инвентарь и т.д.
        //    LookAtMouse ml = GameObject.Find("Player").GetComponent("LookAtMouse"); 
          //  ml.enable = false;
				print("gameMode = yes!");
			//GameObject go = Instantiate(Resources.Load("Player")) as GameObject;
			//go.GetComponent<LookAtMouse>().speed = 10;
        }
    }
		
if(Input.GetKey (KeyCode.P))
		{
			GUI.TextField(new Rect(new Rect (20, 145, 70, 25))," kill = "+b);
		}
if(menuMode){
if(!optionsMode)
{  GUILayout.Space(1000);
				
    GUI.Label(new Rect(Screen.width / 2, 20, 60, 30),"BLOOD KEY",welcomeLabel);  
	GUI.Label(new Rect(Screen.width / 2 + 45, 60, 60, 30),"GAME",welcomeLabel);
				
	GUI.skin = customSkin;
	if(!gameMode)
	{  	GUILayout.Space(1000);		
    	if(GUI.Button(new Rect(Screen.width/2,150,100,30),"Play Game"))// Сделать возврат в меню со стр подключения
		{ //как это сделано для Options
			menuMode = false;   
        	gameMode = true;    
        	//Time.timeScale = 1;
			Application.LoadLevel(1); //здесь можно сначала вывести меню для подключения к серверу
		}
	}
	else
    {
		if(GUI.Button(new Rect(Screen.width/2,115,100,30),"Return"))
		{
  	 		//Time.timeScale = 1; 
   		 	menuMode = false;   
    	}
   }
	GUILayout.Space(1000);			
   if(GUI.Button(new Rect(Screen.width/2,185,100,30),"Options"))
	{
		optionsMode = true; 
	}
				GUILayout.Space(1000);
    if(GUI.Button(new Rect(Screen.width/2,220,100,30),"Quit"))
	{
		Application.Quit();			
	}              	
}else
{
	GUI.Label(new Rect(Screen.width / 2, 0, 50, 20), "Options", welcomeLabel);
    GUI.skin = customSkin;        
    GUI.Label(new Rect(270, 75, 100, 20),"Current Health");
	tempCurHealth = GUI.HorizontalSlider(new Rect(50, 100, 500, 20),tempCurHealth,10,100);
    ourHero.curHealth =Mathf.CeilToInt(tempCurHealth);
    GUI.Label(new Rect(560, 95, 50, 20),ourHero.curHealth.ToString());
       
    GUI.Label(new Rect(270, 125, 130, 20),"Current Experience");
	tempCurExp = GUI.HorizontalSlider(new Rect(50, 150, 500, 20),tempCurExp, 0, 1000);
    ourHero.curExp = Mathf.CeilToInt(tempCurExp);
    GUI.Label(new Rect(560, 145, 50, 20), ourHero.curExp.ToString());
               
    if(GUI.Button(new Rect(20, 190, 100, 30),"<< Back"))
	{
    	optionsMode = false;        
    }		
}
			
}			
}
	// Update is called once per frame
	void Update () {
	
		b = PlayerPrefs.GetInt("play1");
	}
}
