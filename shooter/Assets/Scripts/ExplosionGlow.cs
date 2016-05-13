using UnityEngine;
using System.Collections;

public class ExplosionGlow : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > 15)
			Destroy(gameObject);
		transform.localScale += Vector3.one * 30 * Time.deltaTime;
		GetComponent<SpriteRenderer> ().color -= new Color (0, 0, 0, 1 * Time.deltaTime);
	}
}
