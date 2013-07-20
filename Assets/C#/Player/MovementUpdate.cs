using UnityEngine;
using System.Collections;

/// <summary>
/// This script is attached to the player and it 
/// ensures that every players position, rotation, and scale,
/// are kept up to date across the network.
/// 
/// This script is closely based on a script written by M2H.
/// </summary>


public class MovementUpdate : MonoBehaviour {
	private Vector3 lastPosition;
	private Quaternion lastRotation;
	private Transform myTransform;
	void Start () 
	{
		if(networkView.isMine == true)
		{
			myTransform = transform;
			networkView.RPC("updateMovement", RPCMode.OthersBuffered,
			                myTransform.position, myTransform.rotation);
		}
		else
		{
			enabled = false;	
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Vector3.Distance(myTransform.position, lastPosition) >= 0.1)
		{
			lastPosition = myTransform.position;
			networkView.RPC("updateMovement", RPCMode.OthersBuffered,
			                myTransform.position, myTransform.rotation);
		}
		if(Quaternion.Angle(myTransform.rotation, lastRotation) >= 1)
		{
			lastRotation = myTransform.rotation;
			networkView.RPC("updateMovement", RPCMode.OthersBuffered,
			                myTransform.position, myTransform.rotation);
		}
	}
	[RPC]
	void updateMovement (Vector3 newPosition, Quaternion newRotation)
	{
		transform.position = newPosition;
		transform.rotation = newRotation;
	}
	
	
	
	
	
	
	
	
	
	
	
	
}
