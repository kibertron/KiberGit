using UnityEngine;
using System.Collections;

public class GenerateBullet : MonoBehaviour
{
	public GameObject bullet;
	
	private Transform myTransform;
	private Transform cameraHeadTransform;
	private Vector3 launchPosition = new Vector3();
	private float fireRate = 0.2f;
	private float nextFire = 0;
	
	public float forcebullet;
	float t1;
	float t2;
	public bool wasDelay = false;
	public float delayTime = 4;
	private float attackTimer;
	
	void Start ()
	{
		if(networkView.isMine == true)
		{	
			myTransform = transform;
		//	GameObject spawnManager = GameObject.Find("SpawnManager");	
		//	SpawnScript spawnScript = spawnManager.GetComponent<SpawnScript>();		
		}
		else
		{
			enabled = false;
		}	
	}

	void Update ()
	{
		if (networkView.isMine) 
		{
			if (attackTimer > 0)
				attackTimer -= Time.deltaTime;
			if (attackTimer < 0)
				attackTimer = 0;
			if (attackTimer == 0) 
			{	
				t1 = Time.time;
				if (Input.GetMouseButtonDown (0)) { // стреляем ЛКМ
					t2 = Time.time;
					if (!wasDelay) 
					{
						networkView.RPC ("generateBullet", RPCMode.All, myTransform.name);
						gameObject.GetComponent<paramHero> ().changeCountShot (1);
						gameObject.GetComponent<paramHero> ().changeCurrentOverheat (1);
						gameObject.GetComponent<paramHero> ().changeCurrentExp (100);
					}
					if (gameObject.GetComponent<paramHero> ().countShots >= gameObject.GetComponent<paramHero> ().maxOverheat) 
					{
						if (wasDelay) 
						{ 
							attackTimer = 0;
						} else 
						{
							attackTimer = delayTime;
						}
						wasDelay = true;
					}
				} else {
					if (wasDelay || t1 - t2 > 1.5)
					{
					
						gameObject.GetComponent<paramHero> ().changeCountShot (-1);
						gameObject.GetComponent<paramHero> ().changeCurrentOverheat (-1);
		
						if (gameObject.GetComponent<paramHero> ().countShots == 0 && gameObject.GetComponent<paramHero> ().curOverheat == 0) 
						{
							wasDelay = false;
						}
					}				
				}
			}
		}
	}
	
	[RPC]
	void generateBullet(string originatorName) 
	{
		GameObject temp = Instantiate (bullet, gameObject.transform.position, gameObject.transform.rotation) as GameObject;
		Physics.IgnoreCollision (temp.collider, collider);
		//получим доступ к BulletScript, чтобы знать имя
		BulletScript bulScript = temp.GetComponent<BulletScript>();
		bulScript.myOriginator = originatorName;
		temp.rigidbody.AddForce (gameObject.transform.forward * forcebullet, ForceMode.VelocityChange);
	} 
	
}