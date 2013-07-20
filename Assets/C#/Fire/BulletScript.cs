using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class BulletScript : MonoBehaviour {
	public GameObject bul;
	public paramHero targetHero;
	public GameObject particledestroy; 
	private Transform myTransform;
    private float expireTime = 2;
	//создатель снарядов
	public string myOriginator;

    void Start()
    {
        myTransform = transform;
        StartCoroutine(DestroyMyselfAfterSomeTime());
    }
	void OnTriggerEnter(Collider other)
	{
		if(other.transform.tag.Equals("Enemy"))
		{
		//	other.collider.SendMessage("ApplyDamage", targetHero.damageLevel, SendMessageOptions.DontRequireReceiver);		
			paramHero PHscript = other.transform.GetComponent<paramHero>(); //Здесь находится тот игрок, которого атаковали
			PHscript.iWasJustAttacked = true;
			PHscript.myAttacker = myOriginator; // Здесь имя того кто атаковал
			PHscript.hitByBullet = true;		// Констатация факта попадания
		}
	}
	
    IEnumerator DestroyMyselfAfterSomeTime()
    {
        yield return new WaitForSeconds(expireTime);

        Destroy(myTransform.gameObject);
    }
} 


