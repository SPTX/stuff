using UnityEngine;
using System.Collections;

public class SubBoss : Boss {

	public GameObject turret2;
	protected int deathshots = 4;
	protected int remainingShots;

	// Use this for initialization
	new void Start () {
		base.Start ();
		remainingShots = deathshots;
	}
	
	// Update is called once per frame
	new void Update () {
		base.Update ();

		if (!damageable) {
			LookAtPlayer (turret);
			LookAtPlayer (turret2);
			return;
		}
		if (MapManager.Manager.difficulty != MapManager.Difficulty.death)
			return;

		if (turretFiring <= 0 && (turretRefire -= Time.deltaTime) <= 0) {
			LookAtPlayer (turret);
			LookAtPlayer (turret2);
			turretFiring = 3;
		}
		else if ((nextShot -= Time.deltaTime) <= 0) {
			MapManager.Manager.onScreenEntities.Add (
				((GameObject)Instantiate (Resources.Load ("DeathShotSmall"), turret.transform.position, turret.transform.rotation)).GetComponent<DeathShot> ());
			MapManager.Manager.onScreenEntities.Add (
				((GameObject)Instantiate (Resources.Load ("DeathShotSmall"), turret2.transform.position, turret2.transform.rotation)).GetComponent<DeathShot> ());
			nextShot = 0.25f;
			if (--remainingShots <= 0){
				remainingShots = deathshots;
				nextShot = 0.35f;
				if ((turretFiring -= 1) <= 0)
					turretRefire = turretRefireDelay;
				LookAtPlayer (turret);
				LookAtPlayer (turret2);
			}
		}
	}

	void LookAtPlayer(GameObject obj){
		Vector3 lookPos = MapManager.PlayerCharacter.transform.position - obj.transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		obj.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
