using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	public GameObject projectileType;
	public float firerate = 0.5f;
	public bool altFire;
	public float waitToFire;
	protected float refire = 0;

	public bool autoFire = true;
	public bool ignoreShotLimit;
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
		waitToFire -= Time.deltaTime;

		if (transform.localScale.x < originalSize.x)
			transform.localScale *= 1.2f;
		if (waitToFire < 0 && refire <= 0 && autoFire)
			Fire ();
	}

	public void LookAtPlayer(){
		Vector3 lookPos = MapManager.PlayerCharacter.transform.position - transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	virtual public bool Fire()
	{
		if (MapManager.ShotCounter++ % (ignoreShotLimit ? 1 : MapManager.Manager.shotEvery) != 0 ||
			projectileType == null || MapManager.Manager.difficulty == MapManager.Difficulty.easy ||
		    !MapManager.WithinBounds(transform.position, 7, 4)) {
			refire = firerate;
			return false;
		}
		if (follow)
			LookAtPlayer ();
		if (refire <= 0) {
			MapManager.Manager.onScreenEntities.Add (
				((GameObject)Instantiate(projectileType, transform.position, transform.rotation)).GetComponent<DamagingEntity>());
			refire = firerate;
			return true;
		}
		return false;
	}

	public void HitEffect()
	{
		if (triggersHitEffect)
			transform.localScale = originalSize / 1.25f;
	}

	public void SetFireRate(float newRate, bool randomized = true)
	{
		firerate = newRate;
		if (randomized)
			refire = Random.Range (0, firerate);
	}
}
