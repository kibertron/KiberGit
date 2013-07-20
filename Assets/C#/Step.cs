using UnityEngine;
using System.Collections;

public class Step : MonoBehaviour {
	
	public AudioClip [] step;
	public float speed;

	// Use this for initialization
	IEnumerator Start () {
		while (true)
		{
	if(Input.GetKey(KeyCode.W)||Input.GetKey(KeyCode.S)||Input.GetKey(KeyCode.A)||Input.GetKey(KeyCode.D))
	{
				
	audio.PlayOneShot(step[Random.Range(0,step.Length)],0.5F);
	yield return new WaitForSeconds(speed);
					
	}
				else 
				{
				yield return 0;
				}
			}
		}
	}

