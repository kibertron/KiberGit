using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
    public bool bonusHealth;
    public int Health = 30;
    public bool bonusSpeed;
    public int  Speed = 10;
    public bool bonusDamage;
    public int Damage = 10;
    void OnTriggerEnter(Collider other)
    {
        string a = other.transform.tag;
        if (collider.enabled == true)
        {
            if (bonusHealth)
            {
                if (a.Equals("Enemy"))
                {
                    other.collider.SendMessage("ApplyCurrentHealth", Health, SendMessageOptions.DontRequireReceiver);
                    networkView.RPC("HiddenBonus", RPCMode.AllBuffered);
                }
            }
            if (bonusSpeed)
            {
                if (a.Equals("Enemy"))
                {
                    other.collider.SendMessage("ApplyCurrentSpeed", Speed, SendMessageOptions.DontRequireReceiver);
                    networkView.RPC("HiddenBonus", RPCMode.AllBuffered);
                }
            }
            if (bonusDamage)
            {
                if (a.Equals("Enemy"))
                {
                    other.collider.SendMessage("changeCurrentDamage", Damage, SendMessageOptions.DontRequireReceiver);
                    networkView.RPC("HiddenBonus", RPCMode.AllBuffered);
                }
            }
        } 
    }
	    [RPC]
    void HiddenBonus()
    {
		collider.enabled = false;
        renderer.enabled = false;
    }
}
