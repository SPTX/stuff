using UnityEngine;
using System.Collections;

public class LightStreak : MonoBehaviour {

	protected float finalSize;
	protected SpriteRenderer sprite;
	protected Color newCol;
	protected float startTime;
	// Use this for initialization
	void Start () {
		finalSize = transform.localScale.y;
		startTime = 1 - 0.2f * finalSize;
		transform.localScale = new Vector3 (transform.localScale.x, 0, transform.localScale.z);
		sprite = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (startTime > 0) {
			startTime -= Time.deltaTime;
			return;
		}

		if (transform.localScale.y < finalSize) {
			transform.localScale += Vector3.up * 2 * Time.deltaTime;
			newCol = sprite.color;
			newCol.a += 0.5f * Time.deltaTime;
			sprite.color = newCol;
			return;
		}

		newCol = sprite.color;
		newCol.a -= 0.75f * Time.deltaTime;
		sprite.color = newCol;
	}
}
