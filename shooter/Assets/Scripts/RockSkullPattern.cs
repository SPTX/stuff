using UnityEngine;
using System.Collections;

public class RockSkullPattern : MonoBehaviour {

	public float shootingDuration;
	protected float maxShootingDuration;
	public float waitForNextSalve;
	public float fireRate;
	protected float nextShot;

	// Use this for initialization
	protected virtual void Start () {
		maxShootingDuration = shootingDuration;
		nextShot = fireRate;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		shootingDuration -= Time.deltaTime;
		if (shootingDuration > 0 && shootingDuration < maxShootingDuration && (nextShot -= Time.deltaTime) < 0) {
			Fire ();
			nextShot = fireRate;
		} else if (shootingDuration <= 0)
			shootingDuration = maxShootingDuration + waitForNextSalve;
	}

	protected virtual void Fire(){}

	public Quaternion LookAtPlayer(Vector3 lookPos){
		lookPos = MapManager.PlayerCharacter.transform.position - transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		return Quaternion.AngleAxis(angle, Vector3.forward);
	}
}
