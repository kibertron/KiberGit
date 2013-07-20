using UnityEngine;
using System.Collections;

public class Animation : MonoBehaviour
{
	public GameObject Player;
    public void Update()
    {
		if (networkView.isMine)
		 {			 
             //Стрельба 
			//Управление анимациями "WalkForward" и "Idle" используя RPC
			if(Input.GetAxis("Vertical")!= 0){
			  networkView.RPC("Walk",RPCMode.All,Player.name);
			}
			else{  
			 networkView.RPC("Idle", RPCMode.All, Player.name);
			}
			//Управление анимацией "ShootStraight" используя  RPC
			//При стрельбе, аватар останавливается
			if(Input.GetKey("t")){
			  networkView.RPC("Shoot", RPCMode.All, Player.name);
			}
		
		
		
		
		
		}
    }
			[RPC]
			void Walk(string playername){
			GameObject Player = GameObject.Find(playername );
			Player.animation.CrossFade("WalkForward");
			Player.animation.wrapMode = WrapMode.Loop;
			}
			  
			[RPC]
			void Shoot(string playername){
			 GameObject Player = GameObject.Find(playername );
			 Player.animation.CrossFade("ShootStraight");
			 Player.animation.wrapMode = WrapMode.Once;
			}
			   
			   
			[RPC]
			void Idle(string playername){
			 GameObject Player = GameObject.Find(playername );
			 Player.animation.CrossFade("Idle");
			 Player.animation.wrapMode = WrapMode.Once;
			}	
}