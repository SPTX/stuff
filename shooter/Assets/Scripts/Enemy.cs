using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : DamagingEntity {

	protected int health;
	public int HealthMax = 200;
	protected Vector3 initialBarPos;
	public Image healthBar;
	public Canvas can;
	public Turret turret;

	public string element = "Fire";

	// Use this for initialization
	void Start () {
		health = HealthMax;
		initialBarPos = healthBar.rectTransform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void TakeDamage(int DamageTaken, string DamageElement)
	{
		//debug
		MapManager.PlayerCharacter.ComboAdd ();
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
			turret.HitEffect();
			healthBar.transform.localScale = new Vector3((health * 100 / HealthMax) / 100f, 1, 1);
			Vector3 newpos = healthBar.transform.localPosition;
			newpos.x = healthBar.transform.localScale.x / 2 - 0.5f;
			healthBar.transform.localPosition = newpos;
		}
	}

	void Die(){
		//Spawn explosin effect or whatever
		MapManager.PlayerCharacter.ComboAdd (1);
		Instantiate (Resources.Load ("Material"), transform.position, Quaternion.identity);
		Destroy (gameObject);
	}
}
