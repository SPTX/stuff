using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public GameObject projectileType;
	public float firerate = 0.5f;
	public bool shootsAccelProjectile;
	public bool altFire;
	public float waitToFire;
	protected float refire = 0;

	public bool follow = true;
	public bool staysStraight;
	public bool triggersHitEffect = true;

	private Vector3 originalSize;
	
	// Use this for initialization
	virtual protected void Start () {
		originalSize = transform.localScale;
	}
	
	// Update is called once per frame
	virtual protected void Update () {
		if (follow)
			LookAtPlayer ();
		else if (staysStraight)
			transform.rotation = Quaternion.identity;

		if (refire > 0)
			refire -= Time.deltaTime;

		if (transform.localScale.x < originalSize.x)
			transform.localScale *= 1.2f;
	}

	public void LookAtPlayer(){
		Vector3 lookPos = MapManager.PlayerCharacter.transform.position - transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	public void Fire()
	{
		if (projectileType == null)
			return;
		if (refire <= 0) {
			MapManager.Manager.onScreenEntities.Add (
				((GameObject)Instantiate(projectileType, transform.position, transform.rotation)).GetComponent<DamagingEntity>());
			refire = firerate;
		}
	}

	public void HitEffect()
	{
		if (triggersHitEffect)
			transform.localScale = originalSize / 1.25f;
	}

	public void SetFireRate(float newRate)
	{
		firerate = newRate;
		refire = Random.Range (0, firerate);
	}
}
