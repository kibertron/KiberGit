using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour {

    public GameObject GoTeleportPoint;

    void OnTriggerStay(Collider other)
    {
        print("voshel v teleport i obeshal vb|yti");
        if (other.transform.tag.Equals("Enemy"))
        {

            other.transform.position = GoTeleportPoint.transform.position;
        }
    }

}
