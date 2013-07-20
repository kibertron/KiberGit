using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NetworkView))]
public class PlayerScript : MonoBehaviour
{
	private float forceScale = 200;
	public paramHero param;

	public void Awake ()
	{
		if (!networkView.isMine) {
			// Если не мы владельцы данного объекта, то выключаем данный скрипт.
			// Но помним, что RPC и OnSerializeNetworkView работают в любом случае
			enabled = false;
		}
	}

	public void FixedUpdate ()
	{
		if (networkView.isMine) {			         
			Vector3 moveDirection = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
			rigidbody.velocity = moveDirection * param.forceScale;
		}
	}
	//триггер для ключа
	void OnTriggerEnter (Collider other)
	{
		print ("voshel v trigger");
		string a = other.transform.tag;
		if (a.Equals ("Key")) {
			print ("voshelv Key");
			networkView.RPC ("playerGettingKey", RPCMode.AllBuffered);
			param.iskey = true;
		}
	}

	public void OnSerializeNetworkView (BitStream stream, NetworkMessageInfo info)
	{
		if (stream.isWriting) {
			// Выполняется у владельца networkview; 
			// Сервер рассылает позицию по сети

			Vector3 pos = transform.position;
			stream.Serialize (ref pos);//"Кодирование" и рассылка
			NetworkViewID vi = gameObject.GetComponent<NetworkView> ().viewID;
			stream.Serialize (ref vi);
		} else {
			// Выполняется у всех остальных; 
			// Клиенты получают позицию и устанавливают ее

			Vector3 posReceive = Vector3.zero;
			stream.Serialize (ref posReceive); //"Декодирование" и прием
			transform.position = posReceive;

		}
	}

	[RPC]
	void playerGettingKey ()
	{
		GameObject buf = GameObject.Find ("Key").gameObject as GameObject;
		buf.transform.parent = transform;
		buf.transform.position = transform.position;
	}
		
}