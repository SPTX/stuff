﻿using UnityEngine;
using System.Collections;

public class ProjectileEnemy : Enemy {

	public float speed = 6;
	public bool trembles;
	public float launchTime = 0;
	protected float curSpeed = 0;
	protected float accelInit = 1f;
	protected float accel = 0.4f;

	// Use this for initialization
	new void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		if (launchTime > 0)
		{
			launchTime -= Time.deltaTime;
			return;
		}
		if (curSpeed < speed)
			curSpeed += (accelInit += accel) * Time.deltaTime;

		transform.Translate (transform.right * curSpeed * Time.deltaTime, Space.World);
		if (transform.position.x < -10)
			Destroy (gameObject);
		else if (transform.position.x > 10)
			Destroy (gameObject);
		if (transform.position.y < -6)
			Destroy (gameObject);
		else if (transform.position.y > 6)
			Destroy (gameObject);
	}

	public override void TakeDamage(int DamageTaken, string DamageElement)
	{
		if (trembles) {
			Vector3 randvector = Vector3.zero;
			randvector.x = Random.Range(-0.1f, 0.1f);
			randvector.y = Random.Range(-0.1f, 0.1f);
			turret.transform.localPosition = randvector;
		}
		base.TakeDamage (DamageTaken, DamageElement);
	}
}