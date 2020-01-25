using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScore : MonoBehaviour {

	public float oldHighScore;
	public float newHightScore;
	public PlayerMovement playerMovement;

	// Use this for initialization
	void Start () {
		//canvas.enabled = true;
		newHightScore = PlayerPrefs.GetFloat("Player Score");
		playerMovement = GetComponent<PlayerMovement> ();
	}

	void Update () {
		highScore ();
		print ("HAHAHAHAHHA "+ newHightScore);
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		//if (rb.position.y < -0.2f) {
		//	CanvasObject.SetActive (false);
			//FindObjectOfType<HighScore> ().EndGame ();
		//}
	}

	void highScore () {
		oldHighScore = PlayerMovement.howFarIGot;
		if (newHightScore > oldHighScore) {
			PlayerMovement.howFarIGot = newHightScore;
			PlayerPrefs.SetFloat("Player Score", newHightScore);
			print ("HAHAHAHAHHA "+ newHightScore);
		}
	}
}
