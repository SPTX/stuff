﻿using UnityEngine;
using System.Collections;

public class Shot : DamagingEntity {

	protected float life = 30;
	public float speed = 40f;

	// Use this for initialization
	void Start () {
		damage = MapManager.PlayerCharacter.equipedShotTypes[MapManager.PlayerCharacter.equipedShot].damage;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
		if (transform.position.x < -10)
			Destroy (gameObject);
		else if (transform.position.x > 10)
			Destroy (gameObject);
		if (transform.position.y < -6)
			Destroy (gameObject);
		else if (transform.position.y > 6)
			Destroy (gameObject);

		if ((life = life - Time.deltaTime) < 0)
			Destroy (gameObject);
	}

	override public void TakeDamage(int DamageTaken, Elements DamageElement)
	{
		Instantiate (Resources.Load ("StarSmall"), transform.position, Quaternion.identity);
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		MapManager.Manager.AddLove(0.25f);
		other.GetComponent<DamagingEntity> ().TakeDamage (damage, element);
		Destroy (gameObject);
	}

}
