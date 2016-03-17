using UnityEngine;
using System.Collections;

public class EnemyShot : Shot {

	protected float launchTime = 1;
	protected float speedFinal = 14;
	protected float accel = 1f;
	// Use this for initialization
	void Start () {
		speed = 0;
	}
	
	// Update is called once per frame
	new void Update () {
		if (launchTime > 0)
		{
			launchTime -= Time.deltaTime;
			return;
		}
		if (speed < speedFinal)
			speed += (accel += 0.25f) * Time.deltaTime;
		base.Update ();
	}
}
