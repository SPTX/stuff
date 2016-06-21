using UnityEngine;
using System.Collections;

public class HugeEnemy : Enemy {

	public RockSkullPattern pattern;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	protected override void Die (float elementMultiplier)
	{
		base.Die (elementMultiplier);
		((GameObject)Instantiate (Resources.Load ("Carcass"), transform.position, Quaternion.identity)).GetComponent<ExplodingCarcass> ().
			SetUp (turret.GetComponent<SpriteRenderer>().sprite, turret.transform.localScale);
	}
}
