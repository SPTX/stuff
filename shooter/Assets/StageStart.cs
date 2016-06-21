using UnityEngine;
using System.Collections;

public class StageStart : MonoBehaviour {

	protected float speed = 20;
	protected float accel = 4;
	protected float maxspeed;

	// Use this for initialization
	void Start () {
		maxspeed = speed;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.left * speed * Time.deltaTime);
		if (transform.position.x < -0.4f && speed < maxspeed)
			speed += accel * 10 * Time.deltaTime;
		else if (transform.position.x < 1 && speed > 0.1f) {
			if ((speed -= accel) <= 0)
				speed = 0.1f;
		}
	}
}
