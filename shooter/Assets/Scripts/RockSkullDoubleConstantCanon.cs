using UnityEngine;
using System.Collections;

public class RockSkullDoubleConstantCanon : RockSkullPattern {

	public float canonSpacing;
	public float spreadAngle;
	protected float nextSpread;
	protected int spreadDirection = -1;
	protected float canonNumber;
	protected int canonDirection = -1;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		canonNumber = canonSpacing / 2;
		nextSpread = spreadAngle;
		if (MapManager.Manager.difficulty < MapManager.Difficulty.death) {
			fireRate *= Mathf.Clamp(4 - (float)MapManager.Manager.difficulty, 1.25f, 10);
			nextShot = fireRate;
		}
	}
	
	// Update is called once per frame
	protected override void Update () {
		if (shootingDuration > maxShootingDuration)
			canonNumber = canonSpacing / 2;
		base.Update ();
	}

	protected override void Fire ()
	{
		base.Fire ();
		if ((nextSpread += 120 * spreadDirection * Time.deltaTime) < -spreadAngle || nextSpread > spreadAngle) {
			spreadDirection *= -1;
			if (nextSpread > spreadAngle)
				nextSpread = spreadAngle;
			if (nextSpread < -spreadAngle)
				nextSpread = -spreadAngle;
		}
		MapManager.Manager.onScreenEntities.Add(
			((GameObject)Instantiate(Resources.Load("ProjectileHead"),
		                         transform.position + Vector3.up * canonNumber,
		                         LookAtPlayer(transform.position + Vector3.up * canonNumber) * Quaternion.AngleAxis(nextSpread, Vector3.forward)))
			.GetComponent<DamagingEntity>()
			);

		if ((canonNumber += canonDirection * 0.15f) > canonSpacing || canonNumber < -canonSpacing)
			canonDirection = -canonDirection;
	}
}
