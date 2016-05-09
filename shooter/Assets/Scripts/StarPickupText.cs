using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarPickupText : MonoBehaviour {

	public Text text;
	protected float lifetime = 1;
	protected float speedFactor = 0.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		text.transform.position += Vector3.up * speedFactor * Time.deltaTime;
		if ((lifetime -= Time.deltaTime) < 0) {
			speedFactor = 8;
			text.transform.localScale -= Vector3.one * 2 * Time.deltaTime;
			if (text.transform.localScale.y <= 0)
				Destroy(gameObject);
		}
	}

	public void SetUp(int score)
	{
		text.text = score.ToString ();
	}
}
