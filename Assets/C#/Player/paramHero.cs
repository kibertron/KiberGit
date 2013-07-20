using UnityEngine;
using System.Collections;

public class paramHero : MonoBehaviour
{
	//Публичные переменные для настроек
//private BulletScript flyBullet;
	
	public int countShots;	// для учета количества выстрелов с обнулением
	public float coolDown; // задержка выстрела пули
	public float attackTimer; // задержка выстрела
	public float range = 1000 ; //для рейкаста

	public int maxHealth = 100;	//здоровье игрока
	public int curHealth = 100;
	public int previousHealth = 100;
	public float healthBarLenght;
	public int maxExp = 10000;	// опыт игрока
	public int curExp = 60;
	public float expBarLenght;
	public int maxOverheat = 30; // уровень перегрева при стрельбе
	public int curOverheat = 0;  //при достижении 50 единиц перегрева стрельба временно
	public float overheatBarLenght; // прекращается и медленно уменьшается уровень перегрева
    // бонус скорости
    public float timerBaseBonusSpeedTime = 30.0f;//базовое значение таймера 
    private float timerSpeed;// создания задержк 
    public float maxSpeed = 400;	// скорость перемещения
    private float curSpeed;  // начальное значение скорости игрока
    public bool bonusSpeed = false;
	public Texture bonusSpeedTexture;
    // бонус урона
    public float timerBaseBonusDamageTime = 30.0f;//базовое значение таймера 
    private float timerDamage;// создания задержки
    public int maxDamage = 50;  // максимальный урон
    private int curDamage;  // начальное значение урона
    public bool bonusDamage = false;
	 public Texture bonusDamageTexture;

	public float speedBarLenght;
	public float maxtime = 120;
	public float curtime = 120;
	public float timelength;
	public bool iskey = false;
	
	private GameObject[] ourSpawnPoints;
	private GameObject thisObject;
	public string myAttacker;
	public bool iWasJustAttacked;
	public bool hitByBullet = false;
	
	public int damageLevel = 10; // текущее значение 
	public float forceScale = 200;	
	private bool destroyed = false;
	public GameObject attackers;
	public paramHero par;
	public int countKill ;
	
	
//Проиводятся начальные расчеты при создании игрока
	// Use this for initialization
	void Start ()
	{	
		thisObject = transform.gameObject;
			countKill = 0;
//Проверка на правильность значений параметров при ручном вводе
		if (maxHealth < 1) { 
			maxHealth = 1;
			curHealth = maxHealth;
		}
		if (curHealth > maxHealth) { 
			curHealth = maxHealth;
		}
		if (curHealth < 1) { 
			curHealth = 0;
			//Сюда написать код уничтожения объекта
		}
		if (maxExp < 0) { 
			maxExp = 0;
			curExp = maxExp;
		}
		if (curExp > maxExp) { 
			curExp = maxExp;
		}
		if (curExp < 0) { 
			curExp = 0;
		}
		if (maxOverheat < 0) {
			maxOverheat = 0;
			curOverheat = maxOverheat;
		}
		if (curOverheat > maxOverheat) {
			curOverheat = maxOverheat;
		}
		if (curOverheat < 0) {
			curOverheat = 0;
		}
		if (maxSpeed < 1) {
			maxSpeed = 1;
            forceScale = maxSpeed;
		}
        if (forceScale > maxSpeed)
        {
            forceScale = maxSpeed;
		}
        if (forceScale < 0)
        {
            forceScale = 0;
		}
        if (maxDamage < 1)
        {
            maxDamage = 1;
            damageLevel = maxDamage;
        }
        if (damageLevel > maxDamage)
        {
            damageLevel = maxDamage;
        }
        if (damageLevel < 0)
        {
            damageLevel = 0;
        }
        // Задаём начальные значения 
        curSpeed = forceScale;
        curDamage = damageLevel;
        timerSpeed = timerBaseBonusSpeedTime;
        timerDamage = timerBaseBonusDamageTime;
	}
	void OnGUI()
	{
		if(Input.GetKey(KeyCode.Z))
		{
			int b = PlayerPrefs.GetInt("play1");
			GUI.TextField(new Rect(new Rect (20, 145, 70, 25)),myAttacker + " kill = "+b);
		}
		
		if(bonusSpeed == true)
		{
		// if(timerSpeed>=10)
		//	{	
			GUI.TextField(new Rect(new Rect (20, 145, 70, 25)),""+timerSpeed);
				// анимированная картинка
			GUI.Label(new Rect(30,75,bonusSpeedTexture.width,bonusSpeedTexture.height),bonusSpeedTexture);
		//	}
				
		}
	if (bonusDamage == true)
        {
		//	if(timerDamage>=10)
		//	{
            GUI.TextField(new Rect(new Rect(20, 45 + 145 * 1.5f, 70, 25)), "" + timerDamage);
				// анимированная картинка
            GUI.Label(new Rect(30, 75 * 2.5f, bonusDamageTexture.width, bonusDamageTexture.height), bonusDamageTexture);
		//	}
		}
	}
	
	
	void Update ()
	{
		 attackers = GameObject.Find(myAttacker).gameObject;	
		par = attackers.GetComponent<paramHero>();
		
		healthBarLenght = (float)curHealth / (float)maxHealth;
		expBarLenght = (float)curExp / (float)maxExp;
		overheatBarLenght = (float)curOverheat / (float)maxOverheat;
		speedBarLenght = (float)curSpeed / (float)maxSpeed;
		timelength = (float)curtime/(float)maxtime;
		
		// звуки выстрелов
		if(Input.GetMouseButtonDown(0))
		{
			if(curOverheat==maxOverheat)
			{
				audio.Stop();
			}
			else
			{
				audio.Play();	
			}
		}
		
		if(iWasJustAttacked == true)
		{
			GameObject gameManager = GameObject.Find("GameManager");
			PlayerDatabase dataScript = gameManager.GetComponent<PlayerDatabase>();
			
			for(int i = 0; i < dataScript.PlayerList.Count; i++)
			{
				if(myAttacker == dataScript.PlayerList[i].playerName)
				{
					if(int.Parse(Network.player.ToString()) == dataScript.PlayerList[i].networkPlayer)
					{	//проверка что игрок попал
						if(hitByBullet == true && destroyed == false)
						{
							curHealth = curHealth - damageLevel;
							par.changeCurrentExp(100);
							networkView.RPC("UpdateMyCurrentAttackerEverywhere",RPCMode.Others, myAttacker);
							networkView.RPC("UpdateMyCurrentHealthEverywhere",RPCMode.Others, curHealth);
							hitByBullet = false;
						}
						if(curHealth <= 0 && destroyed == false)
						{
							par.changeCurrentExp(500);
							curHealth = 0;
							destroyed = true;
							++countKill;
							PlayerPrefs.SetInt("play1",countKill);
							GameObject attacker = GameObject.Find(myAttacker);		
							PlayerScore scoreScript = attacker.GetComponent<PlayerScore>();		
							scoreScript.iDestroyedAnEnemy = true;
							scoreScript.enemiesDestroyedInOneHit++;
						}
						networkView.RPC("UpdateEnemyCurExp",RPCMode.All, par.curExp);
					}
				}
			}
			iWasJustAttacked = false;
		}
		if(curHealth <= 0 && networkView.isMine == true)
		{			
			networkView.RPC("respawn",RPCMode.AllBuffered);
		//	Network.RemoveRPCs(Network.player);
			networkView.RPC("TellEveryoneWhoDestroyedWho", RPCMode.All, myAttacker, thisObject.name);
		//	networkView.RPC("DestroySelf", RPCMode.All);
		}
		
		if(curHealth > 0 && networkView.isMine == true)
		{
			if(curHealth != previousHealth)
			{
				networkView.RPC("UpdateMyHealthRecordEverywhere", RPCMode.AllBuffered, curHealth);	
			}
		}
	/*	// А ЭТО САМОВОССТАНОВЛЕНИЕ ЗДОРОВЬЯ. Надо задать float healthRegenRate
		
		if(curHealth < maxHealth)
		{
			curHealth = curHealth + healthRegenRate * Time.deltaTime;
		}
	*/	
		if(curHealth > maxHealth)
		{
			curHealth = maxHealth;	
		}
		
		// Время жиизни бонуса
        if (bonusSpeed == true)
        {
            if (timerSpeed > 0)
                timerSpeed -= Time.deltaTime;
            if (timerSpeed < 0)
                timerSpeed = 0;//создали работу таймера(Позаимствовано из урока romaan27 :) ) 

            if (timerSpeed == 0)
            {
                timerSpeed = timerBaseBonusSpeedTime;//восстановили работу таймера 
                forceScale = curSpeed;
                bonusSpeed = false;
            }
        }
        if (bonusDamage == true)
        {
            if (timerDamage > 0)
                timerDamage -= Time.deltaTime;
            if (timerDamage < 0)
                timerDamage = 0;//создали работу таймера(Позаимствовано из урока romaan27 :) ) 

            if (timerDamage == 0)
            {
                timerDamage = timerBaseBonusDamageTime;//восстановили работу таймера 
                damageLevel = curDamage;
                bonusDamage = false;
            }
        }

	
		if (iskey) {
			if (curtime < 0) {
				curtime = maxtime;
			}
			curtime -= Time.deltaTime;
		}
	}
	
	[RPC]
	void UpdateMyCurrentAttackerEverywhere (string attacker)
	{
		myAttacker = attacker;	
	}
	
	
	[RPC]
	void UpdateMyCurrentHealthEverywhere (int health)
	{
		curHealth = health;	
	}
	
	[RPC]
	void UpdateEnemyCurExp(int exp)
	{
	// attacker = GameObject.Find(myAttacker);	
		//paramHero param = attackers.GetComponent<paramHero>();
		par.curExp = exp;
	}
	
	[RPC]
	void DestroySelf ()
	{
		Destroy(thisObject);	
	}
	
	[RPC]
	void UpdateMyHealthRecordEverywhere (int health)
	{
		previousHealth = health;	
	}
	
	
	[RPC]
	void TellEveryoneWhoDestroyedWho (string attacker, string destroyed)
	{
		//Access the CombatWindow script in the GameManager and
		//tell it who destroyed who so that it appears in the combat
		//window.
		
		GameObject gameManager = GameObject.Find("GameManager");
		CombatWindow combatScript = gameManager.GetComponent<CombatWindow>();
		combatScript.attackerName = attacker;
		combatScript.destroyedName = destroyed;
		combatScript.addNewEntry = true;
	}
	
	
	
	public void changeCountShot (int count)
	{
		countShots += count;
		if (countShots <= 0) {
			countShots = 0;
		}
	}
	
		[RPC]
	public void changeCurrentHealth (int health)
	{
		curHealth -= health;
		if(curHealth > maxHealth)
			curHealth = maxHealth;
		if(curHealth <= 0)
		{
			curHealth = 0;
			networkView.RPC("respawn",RPCMode.AllBuffered);
		}
	}
	
/*	[RPC]
	public void changeCurrentHealthForEnemy (int health)
	{
		curHealth -= health;
		networkView.RPC("UpdateMyCurrentAttackerEverywhere", RPCMode.Others, myAttacker);
		networkView.RPC("UpdateMyCurrentHealthEverywhere", RPCMode.Others, curHealth);
		hitByBullet = false;
		
		if (curHealth > maxHealth)
			curHealth = maxHealth;
		if(curHealth <= 0)
		{
			curHealth = 0;
		}
	}
	*/ 
	 
	[RPC]
	void changeMaxHealth (int health)
	{
		maxHealth += health;
		if (maxHealth < 0)
			curHealth = maxHealth = 0;
		if (maxHealth < curHealth)
			curHealth = maxHealth;
	}

	//[RPC]
	public void changeCurrentExp (int exp)
	{
		curExp += exp;
		if (curExp < 0)
			curExp = 0;
		if (curExp > maxExp)
			curExp = maxExp;
	}

	[RPC]
	void changeMaxExp (int exp)
	{
		maxExp += exp;
		if (maxExp < 0)
			curExp = maxExp = 0;
		if (maxExp < curExp)
			curExp = maxExp;
	}

	[RPC]
	public void changeCurrentOverheat (int over)
	{
		curOverheat += over;
		if (curOverheat < 0)
			curOverheat = 0;
		if (curOverheat > maxOverheat)
			curOverheat = maxOverheat;

	}
    [RPC]
    void changeCurrentBonusHealth(int health)
    {
        curHealth += health;
        if (curHealth > maxHealth)
            curHealth = maxHealth;
    }

    [RPC]
    void changeCurrentSpeed(int speedBonus)
    {
        forceScale += speedBonus;
        if (forceScale < 0)
            forceScale = 0;
        if (forceScale > maxSpeed)
            forceScale = maxSpeed;
        timerSpeed = timerBaseBonusSpeedTime; // Если подобрал еще раз то востанавливаем счетчик
        bonusSpeed = true;
    }

    [RPC]
    void changeCurrentDamage(int damage)
    {
        damageLevel += damage;
        if (damageLevel < 0)
            damageLevel = 0;
        if (damageLevel > maxDamage)
            damageLevel = maxDamage;
        timerDamage = timerBaseBonusDamageTime;// Если подобрал еще раз то востанавливаем счетчик
        bonusDamage = true;
    }
	
	// ниже приведенные функции с префиксом Apply следует использовать 
	//с коллайдером взаимодействующего объекта
	void  ApplyDamage (int damage) //пуля с чужим героем
	{
		print ("damage");
		networkView.RPC ("changeCurrentHealth", RPCMode.All, damage); 
	}
    void ApplyCurrentHealth(int health) // наш герой с бонусом жизний
    {
        print("health");
        networkView.RPC("changeCurrentBonusHealth", RPCMode.All, health);
    }

    void ApplyCurrentSpeed(int speedBonus) //наш герой с бонусом скорости
    {
        print("speedBonus");
        networkView.RPC("changeCurrentSpeed", RPCMode.All, speedBonus);
    }
    void ApplyCurrentDamage(int damage)   // наш герой с бонусом урона
    {
        print("speedBonus");
        networkView.RPC("changeCurrentSpeed", RPCMode.All, damage);
    }

    public void ApplyCurrentExp(int expBonus)	// наш герой с бонусом опыта
    //также попадание в противника, обхождение ловушек и т.д.
    {
        print("expBonus");
        networkView.RPC("changeCurrentExp", RPCMode.Others, expBonus);
    }

	[RPC]
   void respawn()
   {
       //бросить ключ
     /*  try
       {
           GameObject Key = GameObject.Find("Key").gameObject as GameObject;
           Key.collider.enabled = true;
           Key.renderer.enabled = true;
           transform.Find("Key").parent = null;
       }
       catch { ;}*/
	// Изъять бонус
       if (bonusSpeed == true)
       {
               timerSpeed = timerBaseBonusSpeedTime;//восстановили работу таймера 
               forceScale = curSpeed;
               bonusSpeed = false;
       }
       if (bonusDamage == true)
       {
               timerDamage = timerBaseBonusDamageTime;//восстановили работу таймера 
               damageLevel = curDamage;
               bonusDamage = false;
       }
       GameObject Key = GameObject.Find("Key").gameObject as GameObject;
       if (Key.transform.IsChildOf(transform))
       {
           Key.collider.enabled = true;
           Key.renderer.enabled = true;
           Key.transform.parent = null;
       }
       print("sethp after " + curHealth.ToString());
       // Network.Destroy(gameObject);
      //collider.enabled = false;
		   ourSpawnPoints = GameObject.FindGameObjectsWithTag("SpawnPoints");
       	   GameObject randomSpawn = ourSpawnPoints[Random.Range(0, ourSpawnPoints.Length)];
           Vector3 pos = randomSpawn .transform.position;
           transform.position = pos;
      //collider.enabled = true;
       curHealth = 100;
   }

}
