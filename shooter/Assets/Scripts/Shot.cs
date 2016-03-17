using UnityEngine;
using System.Collections;

public class Shot : MonoBehaviour {

	protected float life = 30;
	protected float speed = 40f;
	public string element;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	protected void Update () {
		transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
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
