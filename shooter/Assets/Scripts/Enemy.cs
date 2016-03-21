using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : DamagingEntity {

	protected int health;
	public int HealthMax = 200;
	protected Vector3 initialBarPos;
	public Image healthBar;

	public string element = "Fire";

	// Use this for initialization
	void Start () {
		health = HealthMax;
		initialBarPos = healthBar.rectTransform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	if (Input.GetKeyDown (KeyCode.End)) {
			Debug.Log("local pos = " +  healthBar.rectTransform.localPosition);
			Debug.Log("pos = " +  healthBar.transform.localPosition);

		}
	}

	public void TakeDamage(int DamageTaken, string DamageElement)
	{
		if (DamageElement == "Fire") {
			if (element == "Water")
				health -= DamageTaken / 2;
			else if (element == "Wind")
				health -= DamageTaken * 2;

		} else if (DamageElement == "Water") {
			if (element == "Wind")
				health -= DamageTaken / 2;
			else if (element == "Fire")
				health -= DamageTaken * 2;

		} else if (DamageElement == "Wind") {
			if (element == "Fire")
				health -= DamageTaken / 2;
			else if (element == "Water")
				health -= DamageTaken * 2;

		} else if (DamageElement == "Light") {
			if (element == "Dark")
				health -= DamageTaken * 2;

		} else if (DamageElement == "Dark") {
			if (element == "Light")
				health -= DamageTaken * 2;
		}

		if (health <= 0)
			Destroy (gameObject);
		else {
//			GetComponentInChildren<Canvas>().enabled = true;
			healthBar.transform.localScale = new Vector3((health * 100 / HealthMax) / 100f, 1, 1);
			Vector3 newpos = healthBar.transform.localPosition;
			newpos.x = healthBar.transform.localScale.x / 2 - 0.5f;
			healthBar.transform.localPosition = newpos;
		}
	}
}
