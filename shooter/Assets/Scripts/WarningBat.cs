using UnityEngine;
using System.Collections;

public class WarningBat : MonoBehaviour {

	protected float velocityY;
	protected float velocityX;
	protected float rotation;

	// Use this for initialization
	void Start () {
		transform.localScale = Vector3.one * Random.Range (1f, 5f);
		velocityX = Random.Range (-16f, 16f);
		velocityY = Random.Range (-8f, 1.5f);

		if (velocityX > 0)
			rotation = Random.Range (0f, 10f);
		else
			rotation = Random.Range (-10f, 0f);
	}
	
	// Update is called once per frame
	void Update () {

		transform.localScale += Vector3.one * 3 * Time.deltaTime;
		transform.Translate (velocityX * Time.deltaTime, velocityY * Time.deltaTime, 0, Space.World);
		velocityY += 20 * Time.deltaTime;
		transform.rotation *= Quaternion.AngleAxis (rotation * Time.deltaTime, Vector3.forward);

		if (transform.localScale.x > 12 || !MapManager.WithinBounds(transform.position, 10, 5))
			Destroy (gameObject);
	}
}
