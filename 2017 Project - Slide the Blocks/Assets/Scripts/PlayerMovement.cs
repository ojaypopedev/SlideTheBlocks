using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PlayerMovement : MonoBehaviour {

	//This is a reference to the Rigidbody component called "rb"
	public Rigidbody rb;
	public int SpeedUpTimer;

	public float forwardForce = 2000f;
	public float thrust;

	public float DistanceTravelled = 0;
	public static float howFarIGot = 0;
	public string playerState = "rolling";

	public bool DebugOn = false;
	public bool SpeedUp = true;

	public Text scoreText;
	public Text HighScoreText;

	public UnityEvent EveryTimeScoreIncreases;



	void Start(){
		if (DebugOn) {
			print (this.transform.position);
		}
	}


//	public float sidewaysForce = 500f;
	
	// Update is called once per frame "void Update ()"
	// I'm using "Fixed" because we are using it to mess with physics.
	void FixedUpdate ()
	{

		if(playerState == "rolling"){
			howFarIGot = this.transform.position.z;
			FollowPlayer.IfRolling = true;

			//spawning floor tiles is dependent on this
			if (Mathf.Round (howFarIGot) > DistanceTravelled) {
				DistanceTravelled = Mathf.Round (howFarIGot);
				EveryTimeScoreIncreases.Invoke ();
			}

			if(transform.position.y < 2){
				playerState = "falling";
				FollowPlayer.IfRolling = false;
				HighScoreText.text = (howFarIGot/2).ToString("0");
			}
		}

		if(playerState == "falling"){
			
		}


		scoreText.text = (howFarIGot/2).ToString("0");

		//speed management
		//----------------

		if (SpeedUp) {
			SpeedUpTimer++;
		}
		//Add a forward force
		//the bigger this number is the fastest the game  gets
		if (SpeedUpTimer >200 ) {
			SpeedUpTimer = 0;
			//this is how much the player speeds up after that 300 frames
			thrust += 2;
		}

        //----------------

        
		rb.velocity = new Vector3(0, rb.velocity.y-3, forwardForce);
		
	
		if (rb.position.y < -5f) {
			FindObjectOfType<GameManager> ().EndGame ();
		}
	}
}