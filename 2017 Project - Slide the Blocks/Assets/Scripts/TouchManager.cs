using System.Collections;
using UnityEngine;

public class TouchManager: MonoBehaviour {
	//Vector3 dist;
	//float posX;
	//float posY;
	//float posZ;

	Vector3 startPos;
	float mDown;
	float moveAmount = 10;

	void Start(){
		//posZ = GetComponent<Transform>().position.z;
		//posY = GetComponent<Transform>().position.y;

	}

	void OnMouseDown(){
		//dist = Camera.main.WorldToScreenPoint (transform.position);
		//posX = Input.mousePosition.x - dist.x;
		//posY = Input.mousePosition.y - dist.y;

		mDown = Input.mousePosition.x / Screen.width;
		startPos = transform.position;


		//print (mDown);
	}

	void OnMouseDrag(){
		//Vector3 curPos = new Vector3(Input.mousePosition.x - posX, Input.mousePosition.y - posY, posZ);
		//Vector3 worldPos = Camera.main.ScreenToWorldPoint (curPos);
		//worldPos.z = posZ;
		//worldPos.y = posY;
		//Debug.Log (posZ);
		//transform.position = worldPos;

		float mCurrent = Input.mousePosition.x / Screen.width;

		float mChange = mDown - mCurrent;

		Vector3 newPos = startPos;
		newPos.x -= mChange * moveAmount;

		transform.position = newPos;

	}
}
			




/*using System.Collections;
using UnityEngine;

public class TouchManager: MonoBehaviour {
	float distance = 10;
	void OnMouseDrag(){
		
		Vector3 mousePosition = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, distance);
		Vector3 objPosition = Camera.main.ScreenToWorldPoint (mousePosition);
		transform.position = objPosition;

	}
}
*/








/*
using UnityEngine;
using System.Collections;

public class Touch : MonoBehaviour {
	public GameObject player;

	void Update () {

		if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Stationary)
		{
			Vector2 touchPosition = Input.GetTouch(0).position;
			double halfScreen = Screen.width / 2.0;

			//Check if it is left or right?
			if(touchPosition.x < halfScreen){
				player.transform.Translate(Vector3.left * 5 * Time.deltaTime);
			} else if (touchPosition.x > halfScreen) {
				player.transform.Translate(Vector3.right * 5 * Time.deltaTime);
			}

		}

	}
}






/*


/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchManager : MonoBehaviour {

    GameObject gObj = null;
    Vector3 mO;   

    Ray GenerateMouseRay()
    {
        Vector3 mousePosFar = new Vector3 (Input.mousePosition.x,
                                  Input.mousePosition.y,
                                  Camera.main.farClipPlane);
        Vector3 mousePosNear = new Vector3 (Input.mousePosition.x,
            Input.mousePosition.y,
            Camera.main.farClipPlane);
        Vector3 mousePosF = Camera.main.ScreenToWorldPoint (mousePosFar);
        Vector3 mousePosN = Camera.main.ScreenToWorldPoint (mousePosNear);
          
        Ray mr = new Ray (mousePosN, mousePosF - mousePosN);
        return mr;

    }
        

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetMouseButtonDown (0)) {
            Ray mouseRay = GenerateMouseRay ();
            RaycastHit hit;

            if (Physics.Raycast (mouseRay.origin, mouseRay.direction, out hit)) {
                {
                    gObj = hit.transform.gameObject;
                    mO = hit.transform.position - hit.point;
                }
            } else if (Input.GetMouseButton (0) && gObj) {
                Vector3 newPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
                gObj.transform.position = new Vector3 (newPos.x + mO.x, newPos.y + mO.y, gObj.transform.position.z);
            } else if (Input.GetMouseButtonUp (0) && gObj) {
                gObj = null;
            }
        }
    }
}
*/