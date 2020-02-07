using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParentToGround : MonoBehaviour {


    private readonly string Ground = "Ground";
    [SerializeField] GameObject ground;
    [SerializeField] Vector3 raycastOffset;


    private void Awake()
    {
     
    }


    private void Update()
    {
        RaycastHit hit;
        Physics.Raycast(new Ray(transform.position, Vector3.down), out hit,3f);

        
      

        if (hit.collider)
        {
            if (hit.collider.tag == Ground)
            {
                ground = hit.collider.gameObject;
            }

        }
        else
        {
            ground = null;
        }

        transform.SetParent(ground.transform);

        if (transform.parent)
        {
            Vector3 parentScale = transform.parent.lossyScale;
            transform.localScale = new Vector3(1 / parentScale.x, 1 / parentScale.y, 1 / parentScale.z);

        }
        else
        {
            transform.localScale = Vector3.one;
        }



    }


    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Ray(transform.position+raycastOffset, Vector3.down));
    }


}
