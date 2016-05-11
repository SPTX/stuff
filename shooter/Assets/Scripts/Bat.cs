using UnityEngine;
using System.Collections;

public class Bat : Enemy {

	public Turret headTurret;
	public float timeToFireHeads;
	public float FireDuration;
	protected bool setUp;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	override protected void Update () {
		if (!setUp && turret.waitToFire > 0) {
			timeToFireHeads = turret.waitToFire;
			setUp = true;
			Debug.Log(headTurret.waitToFire);
		}

		if (((timeToFireHeads -= Time.deltaTime) < 0 || timeToFlee <= 0) && (FireDuration -= Time.deltaTime) > 0)
			headTurret.Fire ();
		base.Update ();
	}

	protected override void Die (float elementMultiplier)
	{
		//difficulty condition here
		turret.LookAtPlayer ();
		turret.Fire ();
		base.Die (elementMultiplier);
	}
}
