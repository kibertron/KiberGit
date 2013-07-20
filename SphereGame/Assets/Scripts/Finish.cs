using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour {
	
	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Player")
			if(Application.loadedLevel + 1!=Application.levelCount)
				Application.LoadLevel(Application.loadedLevel+1);
		else
			Application.LoadLevel(0);
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
