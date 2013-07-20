using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {
	
	public GameObject door;
	public paramHero playerOwn;
	private int colliderCount = 0;
	
	void OnTriggerEnter(Collider other)
	{
		if(colliderCount==0){
		//door.transform.localPosition += new Vector3(0,0,0.4f);
			//playerOwn.AddjustCurrentHealth += 10;
		door.SetActive(false);
		Debug.Log("HELLO!!!!");
		transform.localPosition += new Vector3(0,0,0.25f);
		renderer.material.color = new Color(1,0,0);
		}
		colliderCount++;
	}
	
	 void OnTriggerExit(Collider other)
    {
		colliderCount--;
		if(colliderCount==0){
		//door.transform.localPosition -= new Vector3(0,0,0.4f);
		door.SetActive(true);
		transform.localPosition -= new Vector3(0,0,0.25f);
		renderer.material.color = new Color(0,1,0);
		Debug.Log("Good buy!!!!");
		}
    }
	
}
