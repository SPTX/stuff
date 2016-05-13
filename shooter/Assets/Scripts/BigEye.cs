using UnityEngine;
using System.Collections;

public class BigEye : Enemy {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}

	public override int TakeDamage (int DamageTaken, Elements DamageElement)
	{
		if (turret.waitToFire > 0)
			DamageTaken /= 10;
		return base.TakeDamage (DamageTaken, DamageElement);
	}
}
