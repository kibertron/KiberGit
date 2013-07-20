using UnityEngine;
using System.Collections;

public class Trap : MonoBehaviour {
	
	public GameObject particle;
	public float repeatRate;

	public int damage=2;

	void Start () 
	{
		particle.SetActive(false);
		collider.enabled=false;

		InvokeRepeating("Toggle",3,repeatRate);
	}
	
	
	void OnTriggerStay(Collider other)
	{
		print ("voshel v trap trigger");
		    if(other.transform.tag.Equals("Enemy")){
			
			other.collider.SendMessage("ApplyDamage", damage,SendMessageOptions.DontRequireReceiver);


		}
	}
	
	
	void OnTriggerExit(Collider other)
	{
		//CancelInvoke("Damage");
	}
	
	/*void Damage(Collider other)
	{

		//example.CurHealth -= 10;
		networkView.RPC("setHP",RPCMode.AllBuffered,example.CurHealth);
		
	}*/
	
	void Toggle()
	{
		particle.active = !particle.active;
		collider.enabled=!collider.enabled;
	}
}