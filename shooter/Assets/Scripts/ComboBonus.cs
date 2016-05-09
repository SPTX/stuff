using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ComboBonus : MonoBehaviour {

	public Text comboText;
	public Text staticText;
	public GameObject shaker;
	public float shrinkingSpeed = 8;
	public float shakingDuration = 2;
	public float shakingAmplitude = 2;
	public float totalDuration = 4;
	protected Color comboCol;
	protected Color textCol;

	// Use this for initialization
	void Start () {
		comboCol = comboText.color;
		textCol = staticText.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale <= 0)
			return;
		if ((totalDuration -= Time.deltaTime) < 0) {
			comboCol.a = textCol.a -= 1.6f * Time.deltaTime;
			comboText.color = comboCol;
			staticText.color = textCol;
			if (comboCol.a <= 0)
				Destroy (gameObject);
		}
		if (shaker.transform.localScale.x > 1)
			shaker.transform.localScale -= Vector3.one * shrinkingSpeed * Time.deltaTime;
		if ((shakingDuration -= Time.deltaTime) > 0)
			shaker.transform.localPosition = new Vector3(Random.Range(-shakingAmplitude, shakingAmplitude), Random.Range(-shakingAmplitude, shakingAmplitude), 0);
	}

	public void SetUp(int score)
	{
		comboText.text = score.ToString ();
	}
}
