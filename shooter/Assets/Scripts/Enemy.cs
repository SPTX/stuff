using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : DamagingEntity {

	protected int health;
	public int HealthMax = 200;
	protected Vector3 initialBarPos;
	public Image healthBar;
	public Canvas can;

	private Vector3 originalSize;

	public string element = "Fire";

	// Use this for initialization
	void Start () {
		health = HealthMax;
		initialBarPos = healthBar.rectTransform.localPosition;
		originalSize = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < originalSize.x)
			transform.localScale *= 1.2f;
	}

	public void TakeDamage(int DamageTaken, string DamageElement)
	{
		//debug
		MapManager.PlayerCharacter.comboTimer = MapManager.PlayerCharacter.comboTimerMax;
		//

		if (DamageElement == "Wind" && element == "Water" ||
			DamageElement == "Water" && element == "fire" || 
			DamageElement == "Fire" && element == "Wind" ||
			DamageElement == "Light" && element == "Dark" || 
			DamageElement == "Dark" && element == "Light")
			health -= DamageTaken * 2;
		else if (DamageElement == "Water" && element == "Wind" ||
			DamageElement == "Fire" && element == "Water" || 
			DamageElement == "Wind" && element == "Fire" ||
			DamageElement == "Dark" && element == "Light" || 
			DamageElement == "Light" && element == "Dark")
			health -= DamageTaken / 2;
		else
			health -= DamageTaken;

		if (health <= 0)
			Die();

		else {
			can.gameObject.SetActive(true);
			transform.localScale = originalSize / 1.5f;
			healthBar.transform.localScale = new Vector3((health * 100 / HealthMax) / 100f, 1, 1);
			Vector3 newpos = healthBar.transform.localPosition;
			newpos.x = healthBar.transform.localScale.x / 2 - 0.5f;
			healthBar.transform.localPosition = newpos;
		}
	}

	void Die(){
		//Spawn explosin effect or whatever
		Destroy (gameObject);
	}
}
