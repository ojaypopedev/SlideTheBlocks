using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class NewPlayerController : MonoBehaviour {



    public bool MOVE = false;

    private readonly Vector3 rayDirection = new Vector3(0, -1, 0);
    private readonly Vector3 forwardDirection = new Vector3(0, 0, .5f);

    [Header("Speed")]
    [SerializeField][Range(0,10)] private float Minimum;
    [SerializeField][Range(1,11)] private float Maximum;

    [SerializeField] private float maxSpeedAtScore = 100;
    [SerializeField] private AnimationCurve RateOverTime;

    private int score = -1;
    public int PlayerScore { get { return score; } }

    [SerializeField] float speedTest;

    public UnityEvent spawnTile;

    void Fall()
    {
        GetComponent<Collider>().isTrigger = true;
    }
	
	void Update ()
    {


        if(transform.parent != getParent())
        {
            transform.parent = getParent();
            addToScore(1);
            spawnTile.Invoke();
            
        }

        if(transform.parent == null)
        {
            Invoke("Fall", 0.2f);

        }

        speedTest = getSpeed();


        

       if (MOVE && transform.position.y > -2)
           doMovement();


        if(transform.position.y < -10)
        {
            FindObjectOfType<GameManager>().EndGame();
        }


    }


    void addToScore(int amount)
    {
        score += amount;
    }

    void doMovement()
    {
        transform.position += (forwardDirection.normalized * getSpeed());
        print("TEST " +(forwardDirection.normalized.y * getSpeed()));

       // print("FW: "+(forwardDirection.normalized * getSpeed()).ToString());
       // print("SP: "+ getSpeed().ToString());
        float radius = GetComponent<SphereCollider>().radius;
        print("RADIUS: " + radius);
        
        transform.Rotate((getSpeed()/(2*radius*Mathf.PI))*360, 0, 0);
    }


    float getSpeed()
    {

        float speed01 = RateOverTime.Evaluate(Mathf.Clamp01(score / maxSpeedAtScore));

        float speed = (Maximum - Minimum) * speed01 + Minimum;

        return speed * Time.deltaTime;
    }

    Transform getParent()
    {
        RaycastHit hitInfo;

        Physics.Raycast(getRay(), out hitInfo);

        if (hitInfo.collider)
        {
            return hitInfo.collider.transform;
        }

        return null;
    }
    

    private void OnDrawGizmos()
    {
        Debug.DrawRay(getRay().origin, getRay().direction);

        Debug.DrawRay(transform.position, transform.forward * 10);
    }


    Ray getRay()
    {
        return new Ray(transform.position + forwardDirection, rayDirection);
    }
}
