using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingDamage : MonoBehaviour {

	public Text text;
	protected float staytime = 2;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((staytime -= Time.deltaTime) < 0)
			transform.localScale -= Vector3.up * 6 * Time.deltaTime;
		else {
			transform.Translate(Vector3.up * 0.25f * Time.deltaTime, Space.World);
			if (transform.localScale.y < 1) {
				transform.localScale += Vector3.one * 6 * Time.deltaTime;
				if (transform.localScale.y > 1)
					transform.localScale = Vector3.one;
			}
		}
		if (transform.localScale.y < 0)
			Destroy (gameObject);
	}

	public void SetUp(int damage)
	{
		text.text = "-" + damage.ToString ();
	}
}
