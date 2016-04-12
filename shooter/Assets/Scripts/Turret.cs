using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	protected float firerate = 0.5f;
	protected float refire = 0;

	public bool follow = true;
	public bool triggersHitEffect = true;

	private Vector3 originalSize;
	
	// Use this for initialization
	void Start () {
		originalSize = transform.localScale;
	}
	
	// Update is called once per frame
	void Update () {
		if (follow)
			LookAtPlayer ();

		if (refire > 0)
			refire = refire - Time.deltaTime;

		if (transform.localScale.x < originalSize.x)
			transform.localScale *= 1.2f;

		//debug
		if (Input.GetKeyDown (KeyCode.P))
			Shoot ();
	}

	void LookAtPlayer(){
		Vector3 lookPos = MapManager.PlayerCharacter.transform.position - transform.position;
		float angle = Mathf.Atan2(lookPos.y, lookPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void Shoot()
	{
		if (refire <= 0) {
			MapManager.Manager.onScreenEntities.Add (
				((GameObject)Instantiate(Resources.Load ("ShotEnemy"), transform.position, transform.rotation)).GetComponent<DamagingEntity>());
			refire = firerate;
		}
	}

	public void HitEffect()
	{
		if (triggersHitEffect)
			transform.localScale = originalSize / 1.25f;
	}
}
