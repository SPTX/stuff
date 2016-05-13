using UnityEngine;
using System.Collections;

public class ProjectileHeadContainer : Enemy {

	protected int potQuantity = 4;
	protected float angleBetweenPots;
	protected float nextAngle = 0;
	protected int shotQuantity = 0;
	protected float angleBetweenShots;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		potQuantity = Mathf.Clamp (potQuantity * (int)MapManager.Manager.difficulty, 3, 16);
		if (potQuantity > 0)
			angleBetweenPots = 360 / potQuantity;
		if (MapManager.Manager.difficulty != MapManager.Difficulty.easy) {
			shotQuantity = 2 * (int)MapManager.Manager.difficulty + 2;
			angleBetweenShots = 360 / shotQuantity;
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		turret.transform.rotation *= Quaternion.AngleAxis (360 * Time.deltaTime, Vector3.back);
	}

	protected override void Die (float elementMultiplier)
	{
		turret.LookAtPlayer ();
		for (int i = 0; potQuantity > i; --potQuantity) {
			MapManager.Manager.onScreenEntities.Add (
				((GameObject)Instantiate (Resources.Load ("ProjectileLittleHead"), transform.position, turret.transform.rotation * Quaternion.AngleAxis (nextAngle, Vector3.forward)))
				.GetComponent<DamagingEntity> ()
				);
			nextAngle += angleBetweenPots;
		}
		for (int i = 0; shotQuantity > i; --shotQuantity) {
			MapManager.Manager.onScreenEntities.Add (
				((GameObject)Instantiate (Resources.Load ("ShotEnemyAccel"), transform.position, transform.rotation * Quaternion.AngleAxis (nextAngle, Vector3.forward)))
				.GetComponent<DamagingEntity> ()
				);
			nextAngle += angleBetweenShots;
		}
		base.Die (elementMultiplier);
	}
}