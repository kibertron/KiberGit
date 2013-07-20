using UnityEngine;
using System.Collections;

public class paramHero : MonoBehaviour {
	
	//Публичные переменные для настроек
public int maxHealth = 50;	//здоровье игрока
public int curHealth = 50;
public float healthBarLenght;
	
public int maxExp = 50;	// опыт игрока
public int curExp = 0;
public float expBarLenght;

/*public int maxCountBullet = 200; //количество патронов
public int curBullet = 200;
public float bulletBarLenght;*/
	
public int maxOverheat = 50; // уровень перегрева при стрельбе
public int curOverheat = 0;  //при достижении 50 единиц перегрева стрельба временно
public float overheatBarLenght; // прекращается и медленно уменьшается уровень перегрева

public int maxSpeed = 50;	// скорость перемещения
public int curSpeed = 10;
public float speedBarLength;
	
//Проиводятся начальные расчеты при создании игрока
	// Use this for initialization
	void Start () 
	{
		healthBarLenght = Screen.width /8;
		expBarLenght = Screen.width /8;
		overheatBarLenght = Screen.width /8;
		speedBarLength = Screen.width /8;
//Предотвращаем ввод неправильного значения при отладке
		if (maxHealth<1)
		{ 
			maxHealth=1;
 			curHealth = maxHealth;
		}
		if (maxExp<0)
		{ 
			maxExp=0;
 			curExp = maxExp;
		}
		if(maxOverheat<0)
		{
			maxOverheat = 0;
			curOverheat = maxOverheat;
		}
		if(maxSpeed<10)
		{
			maxSpeed = 10;
			curSpeed = maxSpeed;
		}
	}

	
	// Update is called once per frame
	void Update () {
//AddjustCurrentHealth(_curHealth);
	}
	void OnGUI () 
	{
		//GUI.depth =0;
		//Выводится бар состояния здоровья игрока и его числовые значения
		GUI.Box(new Rect(Screen.width - 150,10,healthBarLenght,25), "Health                " + curHealth + "/" + maxHealth);
		GUI.Box(new Rect(Screen.width - 150,40,expBarLenght,25), "Exp                      " + curExp + "/" + maxExp);
		GUI.Box(new Rect(Screen.width - 150,70,overheatBarLenght,25), "Overheat               " + curOverheat + "/" + maxOverheat);
		GUI.Box(new Rect(Screen.width - 150,100,expBarLenght,25), "Speed                 " + curSpeed + "/" + maxSpeed);
	}

public void SetCurrentHealth (int health) {
	curHealth += health;
	if(curHealth<0) curHealth=0;
	if(curHealth > maxHealth) curHealth=maxHealth;
//Расчет бара непосредственно
//healthBarLenght=(Screen.width / 4) * (_curHealth / (float) maxHealth);
}
public void SetCurrentExp (int exp) {
	curExp += exp;
	if(curExp<0) curExp=0;
	if(curExp > maxExp) curExp=maxExp;
//Расчет бара непосредственно
}
public void SetCurrentOverheat (int exp) {
	curOverheat += exp;
	if(curExp<0) curOverheat=0;
	if(curOverheat > maxOverheat) curOverheat=maxOverheat;
//Расчет бара непосредственно
}
public void SetCurrentSpeed (int speedBonus) {
	curSpeed += speedBonus;
	if(curSpeed<0) curSpeed=0;
	if(curSpeed > maxSpeed) curSpeed=maxSpeed;
//Расчет бара непосредственно
}
}
