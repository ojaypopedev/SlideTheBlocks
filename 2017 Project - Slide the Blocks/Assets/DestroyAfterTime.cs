using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour {

    // Use this for initialization



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "KillZone")
        {

            Destroy(gameObject);
        }
    }


}
