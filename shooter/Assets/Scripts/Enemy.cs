﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : DamagingEntity {

	protected int health;
	public int HealthMax = 200;
	public bool big;
	public bool spawnEffect = false;
	protected Vector3 initialBarPos;
	public GameObject LockRing;
	public int materials = 0;
	public int materialSize = 1;
	public int value = 100;
	public Image healthBar;
	public Canvas can;
	public Turret turret;

	public string element = "Fire";

	// Use this for initialization
	protected void Start () {
		health = HealthMax;
		initialBarPos = healthBar.rectTransform.localPosition;
		if (spawnEffect)
			transform.localScale = Vector3.zero;
	}
	
	// Update is called once per frame
	protected void Update () {
		if (spawnEffect && transform.localScale.x < 1) {
			Vector3 newScale = transform.localScale;
			newScale += Vector3.one * 2 * Time.deltaTime;
			transform.localScale = newScale;
			if (newScale.x >= 1)
			{
				transform.localScale = Vector3.one;
				spawnEffect = false;
			}
		}

	}

	new public virtual void TakeDamage(int DamageTaken, string DamageElement)
	{
		if (big)
			MapManager.PlayerCharacter.ComboAdd (1);

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

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.name == "ring")
			LockRing.SetActive (true);
	}

	new void Die(){
		//Spawn explosin effect or whatever
		MapManager.PlayerCharacter.ComboAdd (1);
		MapManager.Manager.AddScore(value);
		if (LockRing.activeSelf && MapManager.Manager.materialSpawned < 1000) {
			while (materials-- > 0)
			{
				Vector3 randvector = Vector3.zero;
				randvector.x = Random.Range(-0.5f, 0.5f);
				randvector.y = Random.Range(-0.5f, 0.5f);
				Pickup mat = ((GameObject)Instantiate (Resources.Load ("Material"), transform.position + randvector, Quaternion.identity)).GetComponent<Pickup>();
				mat.value = Mathf.Clamp(materialSize, 1, 1000 - MapManager.Manager.material);
				mat.transform.localScale *= Mathf.Clamp(materialSize / 2, 0.75f, 2.5f);
				MapManager.Manager.materialSpawned += mat.value;
			}
		}
		Destroy (gameObject);
	}
}
