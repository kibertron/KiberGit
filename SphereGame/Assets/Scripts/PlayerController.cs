using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	//добавили ускорение
	public int acceleration;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//создаем вектор движения и чтобы его приложить делаем prefab для объекта
	var direction = new Vector3(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical"),0).normalized;
	rigidbody.AddForce(direction*acceleration);
		if(Input.GetKey("q"))
		{
			transform.localPosition += new Vector3(0,0,0.05f);
		}
		if(Input.GetKey("e"))
		{
			transform.localPosition += new Vector3(0,0,-0.05f);
		}
	}
}
