using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointLight_Follow : MonoBehaviour {

	public GameObject FollowThis;
	public float DistanceBetween_Y = 0.4f;
	private Vector3 FollowPosition;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		FollowPosition = FollowThis.transform.position;
		this.gameObject.transform.position = new Vector3 (this.gameObject.transform.position.x, FollowPosition.y - DistanceBetween_Y, FollowPosition.z);
	}
}
