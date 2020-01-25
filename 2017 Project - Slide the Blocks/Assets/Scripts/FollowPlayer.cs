using UnityEngine;

public class FollowPlayer : MonoBehaviour {

	public Transform player;
	public Vector3 offset;
	public static bool IfRolling = true;
    [SerializeField] float lerpStrength = .8f;
	// Update is called once per frame
	void Update () {
		if (IfRolling && player.transform.parent != null) {

            Vector3 playerPos = player.position;
            playerPos.y = 0;


            //transform.position = playerPos + offset;

            transform.position = Vector3.Lerp(transform.position, playerPos+offset, Time.deltaTime* lerpStrength);

        } else {

		}
	}


}
