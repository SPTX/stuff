using UnityEngine;
using System.Collections;

public class ProjectileHeadDistributor : Enemy {

	public GameObject graphic;
	public GameObject[] projectile;
	protected byte projNum = 1;
	protected int shotQuantity = 0;
	protected float angleBetweenShots;
	protected float nextAngle = 0;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		if (MapManager.Manager.difficulty != MapManager.Difficulty.easy) {
			shotQuantity = Mathf.Clamp ((int)MapManager.Manager.difficulty + 1, 3, 4);
			angleBetweenShots = 360 / shotQuantity;
		}
		turret.firerate -= (int)MapManager.Manager.difficulty * 0.2f;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		graphic.transform.rotation *= Quaternion.AngleAxis (360 * Time.deltaTime, Vector3.back);
		if (turret.Fire () && turret.altFire) {
			turret.projectileType = projectile[projNum ^= 1];
		}
	}

	protected override void Die (float elementMultiplier)
	{
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
