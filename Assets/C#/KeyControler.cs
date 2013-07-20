using UnityEngine;
using System.Collections;

public class KeyControler : MonoBehaviour {
	
	public GameObject soundPlayObj;
	
    void OnTriggerEnter(Collider other)
    {
        print("voshel v trigger");
        string a = other.transform.tag;
        if (a.Equals("Enemy"))
        {
            networkView.RPC("HiddenKey", RPCMode.AllBuffered);
			soundPlayObj.audio.Play();
        }
    }
	    [RPC]
    void HiddenKey()
    {
		collider.enabled = false;
        renderer.enabled = false;
    }
}
