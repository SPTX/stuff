using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {

	public int value = 1;
	private bool hooked;
	private bool falling;
	private Vector3 velocity = new Vector3(0, 1, 0);
	public string itemName;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Vector3.Distance (MapManager.PlayerCharacter.transform.position, transform.position) < 0.5f)
			PickedUp ();
		else if (falling && Vector3.Distance (MapManager.PlayerCharacter.transform.position, transform.position) < 4)
			hooked = true;

		if (hooked) {
			Quaternion rotation = Quaternion.LookRotation
				(2 * (MapManager.PlayerCharacter.transform.position * 0.4f - transform.position),
				 transform.TransformDirection (Vector3.up));
			rotation.x = rotation.y = 0;
			transform.rotation = rotation;
			transform.RotateAround (transform.position, transform.up, 180f);
			transform.Translate ((MapManager.PlayerCharacter.transform.position - transform.position).normalized * 14 * Time.deltaTime, Space.World);
		} else {
			if (velocity.y > -4){
				velocity.y -= 0.01f;
				if (velocity.y <= 0.5f)
					falling = true;
				if (velocity.x > -1)
					velocity.x -= 0.04f;
			}
			transform.Translate (velocity * Time.deltaTime, Space.World);
			transform.Rotate(0,0,120 * Time.deltaTime, Space.Self);
		}

		if (transform.position.x < -10)
			Destroy (gameObject);
		else if (transform.position.x > 10)
			Destroy (gameObject);
		if (transform.position.y < -6)
			Destroy (gameObject);
		else if (transform.position.y > 6)
			Destroy (gameObject);
	}

	void PickedUp()
	{
		if (itemName == "StarBig") {
			MapManager.Manager.AddScore (2, 1, false, 3);
			//spawn floating text
		} else if (itemName == "StarSmall") {
			MapManager.Manager.AddScore (1, 1, false, 3);
			//spawn floating text
		}
		else {
			MapManager.Manager.AddMaterial(value);
		}
		Destroy (gameObject);
	}
}
