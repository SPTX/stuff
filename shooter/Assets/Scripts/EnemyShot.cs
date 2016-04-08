using UnityEngine;
using System.Collections;

public class EnemyShot : Shot {

	protected float launchTime = 0.5f;
	protected float speedFinal = 14;
	protected float accelInit = 1f;
	protected float accel = 0.4f;

	// Use this for initialization
	void Start () {
		speed = 0;
	}
	
	// Update is called once per frame
	override protected void Update () {
		if (launchTime > 0)
		{
			launchTime -= Time.deltaTime;
			return;
		}
		if (speed < speedFinal)
			speed += (accelInit += accel) * Time.deltaTime;
		base.Update ();
	}

	void OnTriggerEnter2D(Collider2D other){
		//Destroy (gameObject);
	}

}
