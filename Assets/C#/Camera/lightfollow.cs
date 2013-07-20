using UnityEngine;
using System.Collections;

/// <summary>
/// This script is direct a lightpot that find above a player
/// </summary>

public class lightfollow : MonoBehaviour {

	private GameObject target;
	
	void Start () {	
	}
	
	void Update () {
		target = GameObject.Find("Player(Clone)").gameObject;
		transform.position = new Vector3(target.transform.position.x,transform.position.y,target.transform.position.z);
	}
}
