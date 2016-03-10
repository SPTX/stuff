using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	private float life = 2.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (transform.right * 35f * Time.deltaTime);
		if (transform.position.x < -10)
			Destroy (gameObject);
		else if (transform.position.x > 10)
			Destroy (gameObject);
		if (transform.position.y < -6)
			Destroy (gameObject);
		else if (transform.position.y > 6)
			Destroy (gameObject);
		if ((life = life - Time.deltaTime) < 0)
			Destroy (gameObject);
	}
}
