using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrashSound : MonoBehaviour {

	public AudioClip slidefloor;

	private AudioSource source;

	//Use this for initialization
	void Awake () {
		source = GetComponent<AudioSource> ();
	}

	void OnCollisionEnter (Collision coll)
	{
		source.PlayOneShot (slidefloor, 1F);
	}


}
