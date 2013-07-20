using UnityEngine;
using System.Collections;

public class Minimap : MonoBehaviour
{
	private GameObject target;
	
	void Start () {	
	}
	
	void LateUpdate ()
	{
		target = GameObject.Find("Player(Clone)").gameObject;
		transform.position = new Vector3 (target.transform.position.x, transform.position.y, target.transform.position.z);
	}
}