using UnityEngine;
using System.Collections;

public class Pannel : MonoBehaviour {

	public bool opening;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (opening && transform.localScale.x < 1)
			transform.localScale += Vector3.one * 24 * Time.deltaTime;
		else if (transform.localScale.x > 0 && !opening)
			transform.localScale -= Vector3.one * 24 * Time.deltaTime;
		
		if (transform.localScale.x > 1)
			transform.localScale = Vector3.one;
		else if (transform.localScale.x < 0)
			transform.localScale = Vector3.zero;
	}
}
