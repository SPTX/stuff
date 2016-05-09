using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FloatingComboText : MonoBehaviour {

	public Text text;
	public Vector3 direction;
	protected float lifetime = 1;
	protected Color newCol;

	// Use this for initialization
	void Start () {
		text.text = ((int)MapManager.PlayerCharacter.comboCount).ToString ();
		newCol = text.color;
	}
	
	// Update is called once per frame
	void Update () {
		if ((lifetime -= Time.deltaTime) < 0) {
			text.transform.localScale += Vector3.left * 2 * Time.deltaTime;
			if (text.transform.localScale.x <= 0)
				Destroy(gameObject);
			text.transform.position += Vector3.left * 2 * Time.deltaTime;
		}
		else
			text.transform.position += direction * Time.deltaTime;
	}
}
