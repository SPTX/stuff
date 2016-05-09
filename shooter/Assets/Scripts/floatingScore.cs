using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class floatingScore : MonoBehaviour {

	public Text text;
	public Vector3 destination;
	protected Color newAlpha;
	protected bool arrived;
	protected float lifetime = 1;

	// Use this for initialization
	void Start () {
		newAlpha = text.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (!arrived) {
			if (text.transform.localPosition.x > 0) {
				text.transform.localPosition += Vector3.left * 8f * Time.deltaTime;
			} else
				arrived = true;
			if (text.color.a < 1) {
				newAlpha.a += 4f * Time.deltaTime;
				text.color = newAlpha;
			}
		} else if ((lifetime -= Time.deltaTime) < 0) {
			newAlpha.a -= 1f * Time.deltaTime;
			text.color = newAlpha;
			if (text.transform.localScale.y > 0)
				text.transform.localScale -= Vector3.up * 0.05f * Time.deltaTime;
			if ((transform.position = Vector3.MoveTowards(transform.position, destination, 12 * Time.deltaTime)) == destination)
				Destroy(gameObject);
		}
	}

	public void SetUp(int score)
	{
		text.text = score.ToString ();
	}
}
