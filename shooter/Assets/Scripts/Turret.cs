﻿using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	protected float firerate = 0.5f;
	protected float refire = 0;

	public bool follow = true;

	private Vector3 originalSize;
	
	// Use this for initialization
	void Start () {
		originalSize = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (follow)
			LookAtPlayer ();

		if (refire > 0)
			refire = refire - Time.deltaTime;

		if (transform.localScale.x < originalSize.x)
			transform.localScale *= 1.2f;

		//debug
		if (Input.GetKeyDown (KeyCode.P))
			Shoot ();
	}

	void LookAtPlayer(){
		Quaternion rotation = Quaternion.LookRotation
			(2 * (MapManager.PlayerCharacter.transform.position * 0.4f - transform.position),
			 transform.TransformDirection (Vector3.up));
		rotation.x = rotation.y = 0;
		transform.rotation = rotation;
		transform.RotateAround (transform.position, transform.up, 180f);
	}

	void Shoot()
	{
		if (refire <= 0) {
			Instantiate (Resources.Load ("ShotEnemy"), transform.position, transform.localRotation);
			refire = firerate;
		}
	}

	public void HitEffect()
	{
		transform.localScale = originalSize / 1.5f;
	}
}
