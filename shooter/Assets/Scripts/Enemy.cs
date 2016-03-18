using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : DamagingEntity {

	protected int health = 200;
	protected int initialHealth;
	protected Vector3 initialBarPos;
	public string element = "Fire";
	public Image healthBar;

	// Use this for initialization
	void Start () {
		initialHealth = health;
		initialBarPos = healthBar.transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	
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
			healthBar.enabled = true;
			healthBar.transform.localScale = new Vector3((health * 100 / initialHealth) / 100f, 1, 1);
			// a faire, position bar vers la gauche
			healthBar.transform.localPosition = initialBarPos;
		}
	}
}
