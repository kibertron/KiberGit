using UnityEngine;
using System.Collections;
 
public class LookAtMouse : MonoBehaviour
{
	public GameObject cam;
 
	// speed is the rate at which the object will rotate
	public float speed;
 
	void FixedUpdate () 
	{
		if (networkView.isMine){
			cam = GameObject.Find("cameraPrefab(Clone)").gameObject;
    		Ray ray = cam.camera.ScreenPointToRay (Input.mousePosition);
        	RaycastHit hit = new RaycastHit();
        	if (Physics.Raycast (ray, out hit))
        	{
            	Vector3 rot = transform.eulerAngles;
            	transform.LookAt(hit.point);
            	transform.eulerAngles = new Vector3(rot.x, transform.eulerAngles.y, rot.z);
        	}
		}
		else enabled = false;
    }

}