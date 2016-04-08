﻿using UnityEngine;
using System.Collections;

public class ShotWide : ShotType {

	private float shotAngle = 6f;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
		damage = 40;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public GameObject Fire(Vector3 shotOrigin, int power){

		ShotElem = "Shot" + element;
		
		Instantiate (Resources.Load (ShotElem), shotOrigin,
		             Quaternion.Euler(0, 0, shotAngle));
		Instantiate (Resources.Load (ShotElem), shotOrigin,
		             Quaternion.Euler(0, 0, -shotAngle));
		if (power > 0)
		{
			Instantiate (Resources.Load (ShotElem), shotOrigin + Vector3.up * 0.3f, Quaternion.Euler(0, 0, shotAngle));
			Instantiate (Resources.Load (ShotElem), shotOrigin - Vector3.up * 0.3f, Quaternion.Euler(0, 0, -shotAngle));
		}
		if (power > 1)
		{
			Instantiate (Resources.Load (ShotElem), shotOrigin + Vector3.up * 0.2f, transform.rotation);
			Instantiate (Resources.Load (ShotElem), shotOrigin - Vector3.up * 0.2f, transform.rotation);
		}
		///lovemax only
		if (power > 2)
		{
			Instantiate (Resources.Load (ShotElem), shotOrigin + Vector3.up * 0.5f, Quaternion.Euler(0, 0, shotAngle + 6f));
			Instantiate (Resources.Load (ShotElem), shotOrigin - Vector3.up * 0.5f, Quaternion.Euler(0, 0, -shotAngle - 6f));
		}
		return (GameObject)Instantiate (Resources.Load (ShotElem), shotOrigin, transform.rotation);
	}
}
