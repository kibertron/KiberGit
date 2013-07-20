using UnityEngine;
using System.Collections;

public class healthHero : MonoBehaviour {
	
	//Публичные переменные для настроек
public int maxHealth = 50;

//Блок переменных локального пользователя
public int _curHealth = 50;

public float healthBarLenght;
        
//Проиводятся начальные расчеты при содание игрока
	// Use this for initialization
	void Start () {
//Задаем начальную длину здоровья игрока
//healthBarLenght = Screen.width /8;
//Предотвращаем ввод неправильного значения
//Максимального здоровья
//if (maxHealth<1) maxHealth=1;
// _curHealth = maxHealth;
	}

	
	// Update is called once per frame
	void Update () {
	//С помощью этой функции изменяется размер бара здоровья (можно удалить)
AddjustCurrentHealth(_curHealth);
	}
	void OnGUI () {
//Выводится бар состояния здоровья игрока и его числовые значения
GUI.Box(new Rect(Screen.width - 10 + healthBarLenght,10,healthBarLenght,20), _curHealth + "/" + maxHealth);
		GUI.Box(new Rect(Screen.width - 10 + healthBarLenght,40,100,55), "Hello \n Friend!\n I'm Sergey");
}

//Производим расчет нужной ширины состояния  здоровья
public void AddjustCurrentHealth (int adj) {
//Блок по продотвращению получения неверного состояния здоровья
//Меньше нуля и больше максимума
//так как изменяется здоровье извне
if(_curHealth<0) _curHealth=0;
if(_curHealth> maxHealth) _curHealth=maxHealth;
//Расчет бара непосредственно
healthBarLenght=(Screen.width / 4) * (_curHealth / (float) maxHealth);
}
	
}
