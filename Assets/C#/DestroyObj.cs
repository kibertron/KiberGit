using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class DestroyObj : MonoBehaviour {

	public int time;
	
	void Start()
	{
		networkView.RPC("DestroyObject",RPCMode.All,1);
	}
		
    [RPC]
    IEnumerator DestroyObject(int time)
    {
		yield return new WaitForSeconds(time);
		GameObject.Destroy(gameObject);
    }
}