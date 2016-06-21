using UnityEngine;
using System.Collections;

public class RockSkullWave : RockSkullPattern {

	public float numberOfSkulls;
	public float angle;
	protected float angleBetweenSkulls;

	// Use this for initialization
	protected override void Start () {
		base.Start();
		if (MapManager.Manager.difficulty < MapManager.Difficulty.death)
			numberOfSkulls *= Mathf.Clamp ((int)MapManager.Manager.difficulty / 2f, 0.25f, 0.6f);
		angleBetweenSkulls = angle / (int)numberOfSkulls;
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void Fire ()
	{
		base.Fire ();
		float nextangle = -(angle / 2);
		for (int i = 0; i < numberOfSkulls; ++i) {
			MapManager.Manager.onScreenEntities.Add(
				((GameObject)Instantiate(Resources.Load("ProjectileHead"), transform.position, LookAtPlayer(transform.position) * Quaternion.AngleAxis(nextangle ,Vector3.forward)))
				.GetComponent<DamagingEntity>()
				);
			nextangle += angleBetweenSkulls;
		}
	}
}
