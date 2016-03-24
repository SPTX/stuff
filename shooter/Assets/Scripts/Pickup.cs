using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	private int scoreValue = 0;
	private bool hooked;
	private bool falling;
	private Vector3 velocity = new Vector3(0, 1, 0);

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		if (hooked) {
			Quaternion rotation = Quaternion.LookRotation
				(2 * (MapManager.PlayerCharacter.transform.position + Vector3.up * 0.4f - transform.position),
				 transform.TransformDirection (Vector3.up));
			rotation.x = rotation.y = 0;
			transform.rotation = rotation;
			transform.RotateAround (transform.position, transform.up, 180f);
		} else {
			if (velocity.x > -1){
				velocity.x -= 0.01f;
				if (velocity.y > -1)
					velocity.y -= 0.02f;
			}
			transform.Translate (velocity * Time.deltaTime, Space.World);
			transform.Rotate(0,0,5, Space.Self);
		}
	}
}
