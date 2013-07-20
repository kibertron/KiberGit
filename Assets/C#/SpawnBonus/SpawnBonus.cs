using UnityEngine;
using System.Collections;

public class SpawnBonus : MonoBehaviour
{
   // public Transform[] SpawnPoints;
 	//public GameObject[] BonusPrefabs;
 	public int yieldTimeMin = 2;  
 	public int yieldTimeMax = 5;  
   // private Transform BonusPosition;
   // private GameObject Bonus; 
	
	private GameObject[] bonusPoint;
	private GameObject[] bonusTypes;

 	void Update()
 	{
        if (GameObject.FindGameObjectsWithTag("bonusPoint") != null)
        {
            //print("NASHEL");
  		    //SpawBonus();
		    networkView.RPC("SpawBonus", RPCMode.AllBuffered);
		}
 	}
	
	[RPC]
    void SpawBonus(){

		Shuffle<GameObject>(bonusPoint);
		for(int i=0;i<bonusTypes.Length;i++)
		{
            print(bonusPoint[i]);
            bonusPoint = GameObject.FindGameObjectsWithTag("bonusPoint");
             bonusTypes = GameObject.FindGameObjectsWithTag("bonusTypes");
			 bonusTypes[i].transform.position = bonusPoint[i].transform.position;
			 bonusTypes[i].transform.rotation = bonusPoint[i].transform.rotation;
			 bonusTypes[i].collider.enabled = true;
       		 bonusTypes[i].renderer.enabled = true;
		}
		//yield return new WaitForSeconds(Random.Range(yieldTimeMin, yieldTimeMax));
	}	
  /*  void SpawBonus()
    {
      for (int i=0; i<BonusPrefabs.Length; i++) 
       {
        //yield return new WaitForSeconds(Random.Range(yieldTimeMin, yieldTimeMax));
  
 		Bonus =  BonusPrefabs[Random.Range(0, BonusPrefabs.Length)];
        BonusPosition = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
        Network.Instantiate(Bonus, BonusPosition.transform.position, BonusPosition.transform.rotation, 0);
       }
 
    }*/
	public  void Shuffle<T>(T[] array)
  	{
    	var rand = new System.Random();
    	for (int i = array.Length; i > 0; i--)
    	{
        	int j = rand.Next(i);
        	T tmp = array[j];
        	array[j] = array[i - 1];
        	array[i - 1] = tmp;
    	}
  	}
}