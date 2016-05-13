using UnityEngine;
using System.Collections;

public class BigEyeTurret : Turret {

	protected float potDelay = 0;
	protected Quaternion shootingDirection;
	protected int potQuantity = 4;
	protected float angleBetweenPots;
	protected float nextAngle = 0;

	// Use this for initialization
	override protected void Start () {
		base.Start ();
		potQuantity *= (int)MapManager.Manager.difficulty;
		if (potQuantity > 0)
			angleBetweenPots = 360 / potQuantity;
		if (altFire)
			potDelay = 0.05f;
	}

	// Update is called once per frame
	override protected void Update () {
		if (waitToFire > 0)
			shootingDirection = transform.rotation;
		if (waitToFire < 0 && refire <= 0)
			Fire ();
		base.Update ();
	}

	override public bool Fire(){
		if (potQuantity <= 0)
			return false;
		--potQuantity;

		if (altFire) {
			GameObject pot = (GameObject)Instantiate (Resources.Load ("Pot"), transform.position, shootingDirection * Quaternion.AngleAxis (nextAngle, Vector3.forward));
			nextAngle += angleBetweenPots;
			pot.transform.Translate (pot.transform.right * -0.75f, Space.World);
			MapManager.Manager.onScreenEntities.Add (pot.GetComponent<DamagingEntity> ());
		} else {
			MapManager.Manager.onScreenEntities.Add (
			((GameObject)Instantiate (Resources.Load ("Pot"), transform.position, shootingDirection)).GetComponent<DamagingEntity> ());
			refire = potDelay;
		}
		return true;
	}
}
