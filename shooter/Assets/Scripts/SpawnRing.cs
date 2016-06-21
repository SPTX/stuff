using UnityEngine;
using System.Collections;

public class SpawnRing : MonoBehaviour {

	protected bool expands;
	protected float rotateSpeed = 360;
	protected Transform parent;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (parent)
			transform.position = parent.position;
		else
			Destroy (gameObject);

		transform.rotation *= Quaternion.AngleAxis (rotateSpeed * Time.deltaTime, Vector3.forward);
		if (!expands && transform.localScale.x > 0) {
			if ((transform.localScale -= Vector3.one * 0.5f * Time.deltaTime).x < 0)
				Destroy (gameObject);
		}
		else if (expands && transform.localScale.x < 1)
			transform.localScale += Vector3.one * 0.5f * Time.deltaTime;
	}

	public void SetUp(bool newExpands, Transform newParent, Elements colour){

		GetComponentInChildren<SpriteRenderer> ().color = MapManager.elementColors [(int)colour];
		parent = newParent;
		if (!(expands = newExpands))
			return;
		transform.localScale = Vector3.zero;
		rotateSpeed = 90;
	}
}
