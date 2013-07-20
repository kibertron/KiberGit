/// <summary>
/// Player bar display.
/// Выводит на экран бары игрока
/// Вешать на игрока
/// </summary>
using UnityEngine;
using System.Collections;

public class PlayerBarDisp : MonoBehaviour
{
	//playerBar разделительные линии панели здоровья и маны
	//HealthBar полоса здоровья игрока
	//ManaBar Полоса маны игрока
	//Bar фон панели
	public GUISkin mySkin; // Скин где хранятся текстуры баров
	public paramHero Char; // Объект на котором висят статы
	public bool Visible = true; //Видимость бара
	
	// Use this for initialization
	void Start ()
	{
	
	}
	
	[RPC]
	void OnGUI ()
	{
		if (Visible && networkView.isMine) {
			//назначаем mySkin текущим скином для GUI
			GUI.skin = mySkin;
			
			GUI.Box (new Rect (10, 13, 140, 18), "");
			GUI.Box (new Rect (12, 14, 137 * Char.healthBarLenght, 15), "", GUI.skin.GetStyle ("HealthBar"));
			GUI.Label (new Rect (12, 10, 135, 25), "Health            " + Char.curHealth + "/" + Char.maxHealth);
			//GUI.Box (new Rect (10, 33, 140, 18), "");
			//GUI.Box (new Rect (11, 34, 137 * Char.expBarLenght, 16), "", GUI.skin.GetStyle ("ExpBar"));
			//GUI.Label (new Rect (12, 30, 135, 25), "Exp                  " + Char.curExp);
			
			GUI.Box (new Rect (10, 33, 140, 18), "");
			GUI.Box (new Rect (11, 34, 137 * Char.timelength, 16), "", GUI.skin.GetStyle ("ExpBar"));
			GUI.Label (new Rect (12, 30, 135, 25), "Time                   " + (int)Char.curtime);
			
			GUI.Box (new Rect (10, 53, 140, 18), "");
			GUI.Box (new Rect (12, 54, 136 * Char.overheatBarLenght, 15), "", GUI.skin.GetStyle ("HealthBar"));
			GUI.Label (new Rect (12, 50, 135, 25), "Overheat              " + Char.curOverheat);
			//GUI.Box (new Rect (10, 73, 140, 18), "");
			//GUI.Box (new Rect (12, 74, 136 * Char.speedBarLenght, 15), "", GUI.skin.GetStyle ("SpeedBar"));
			//GUI.Label (new Rect (12, 70, 135, 25), "Speed                   " + Char.curSpeed);
	
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
