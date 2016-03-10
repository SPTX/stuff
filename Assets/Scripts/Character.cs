using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

	private float firerate = 0.025f;
	private float refire = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		transform.Translate (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0);
		Vector2 newPos = transform.position;

		if (transform.position.x < -8)
			newPos.x = -8;
		else if (transform.position.x > 8)
			newPos.x = 8;
		if (transform.position.y < -3.5f)
			newPos.y = -3.5f;
		else if (transform.position.y > 3.5f)
			newPos.y = 3.5f;
		transform.position = newPos;

		if (Input.GetMouseButton (0))
			Fire ();
	}

	void Fire(){
		if (refire <= 0) {
			Instantiate (Resources.Load ("Shot"), transform.position, transform.rotation);
			//Instantiate (Resources.Load ("Shot"), transform.position, transform.rotation);
			//Instantiate (Resources.Load ("Shot"), transform.position, transform.rotation);
			refire = firerate;
		} else
			refire = refire - Time.deltaTime;
	}
}
