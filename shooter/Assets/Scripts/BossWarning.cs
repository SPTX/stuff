using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossWarning : MonoBehaviour {

	public GameObject background;
	public SpriteRenderer warningText;
	public SpriteRenderer warningTextJP1;
	public SpriteRenderer warningTextJP2;
	protected int direction = -1;
	protected int blinkCount = 4;
	protected float stillTime = 3;
	protected Color newColour;
	protected bool arrived;
	protected float batFrequency = 0.075f;
	protected float nextBat;

	// Use this for initialization
	void Start () {
		nextBat = batFrequency;
	}
	
	// Update is called once per frame
	void Update () {

		if (blinkCount > 0 && background.transform.localScale.y < 3) {
			if ((background.transform.localScale += Vector3.up * 5 * Time.deltaTime).y > 3)
				background.transform.localScale = new Vector3(background.transform.localScale.x, 3, 1);
		}

		if (!arrived) {
			warningText.gameObject.transform.Translate(Vector3.right * 60 * Time.deltaTime);
			warningTextJP1.gameObject.transform.Translate(Vector3.left * 60 * Time.deltaTime);
			warningTextJP2.gameObject.transform.Translate(Vector3.left * 60 * Time.deltaTime);
			if (warningText.gameObject.transform.position.x > 0){
				warningText.transform.position = new Vector3(0, warningText.transform.position.y, 0);
				warningTextJP1.transform.position = new Vector3(0, warningTextJP1.transform.position.y, 0);
				warningTextJP2.transform.position = new Vector3(0, warningTextJP2.transform.position.y, 0);
				arrived = true;
			}
			return;
		}

		if (blinkCount > 0) {
			newColour = warningText.color;
			newColour.a += direction * 2.5f * Time.deltaTime;
			if (newColour.a <= 0 || (newColour.a >= 1 && --blinkCount > 0))
				direction = -direction;
			warningTextJP1.color = warningTextJP2.color = warningText.color = newColour;
		} else if ((stillTime -= Time.deltaTime) < 2) {
			newColour = warningTextJP2.color;
			newColour.a -= Time.deltaTime;
			warningTextJP2.color = newColour;

			if (stillTime < 1) {
				if (background.transform.localScale.y > 0) {
					if ((background.transform.localScale += Vector3.down * 2.5f * Time.deltaTime).y < 0){
						background.transform.localScale = Vector3.zero;
						Destroy(gameObject);
					}
				}

				newColour = warningTextJP1.color;
				newColour.a -= Time.deltaTime;
				warningText.color = warningTextJP1.color = newColour;
			}

			if (warningText.color.a > 0 && (nextBat -= Time.deltaTime) < 0){
				nextBat = batFrequency;
				Instantiate(Resources.Load("WarningBat"), new Vector3(Random.Range(-4f, 4f), Random.Range(-1f, 1f), 0), Quaternion.identity);
			}
		}
	}
}
