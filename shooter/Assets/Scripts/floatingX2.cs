using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class floatingX2 : MonoBehaviour {

	protected float lifeTime = 1;
	public Text text;
	protected Vector3 scaleMultiplier;
	protected float normalSize = 1;
	protected bool shrinking = true;

	// Use this for initialization
	void Start () {
		SetUp ();
	}
	
	// Update is called once per frame
	void Update () {
		if (shrinking) {
			text.transform.localScale += scaleMultiplier * Time.deltaTime;
			if (text.transform.localScale.x <= normalSize){
				shrinking = false;
				text.transform.localScale = Vector3.one * normalSize;
				scaleMultiplier.x = -2;
				scaleMultiplier.y = 6;
			}
		}
		else if ((lifeTime -= Time.deltaTime) < 0) {
			text.transform.localScale += scaleMultiplier * Time.deltaTime;
			if (text.transform.localScale.x <= 0)
				Destroy(gameObject);
		}

	}

	public void SetUp(bool big = false){
		if (big) {
			normalSize = 2;
			text.transform.localScale *= 2;
		}
		scaleMultiplier = new Vector3 (-32, 2, 0);
	}

}
