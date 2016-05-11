using UnityEngine;
using System.Collections;

public class Moth : Enemy {

	public Turret turretUpper;
	public Turret turretUnder;
	public Turret turret1;
	public Turret turret2;
	public Turret turret3;

	public bool LargeVersion;
	protected float shooting = 3;
	protected float refireDelay = 0;

	// Use this for initialization
	protected override void Start () {
		base.Start ();
		turret1.SetFireRate (turret1.firerate);
		turret2.SetFireRate (turret2.firerate);
		turret3.SetFireRate (turret3.firerate);
	}
	
	// Update is called once per frame
	protected override void Update () {
		base.Update ();
		if (nextRoute == null) {
			if ((refireDelay -= Time.deltaTime) <= 0) {
				if ((shooting -= Time.deltaTime) > 0) {
					if (LargeVersion) {
						turretUpper.Fire ();
						turretUnder.Fire ();
					}
					turret1.Fire ();
					turret2.Fire ();
					turret3.Fire ();
				} else {
					shooting = 3;
					refireDelay = 4;
				}
			}
		}
	}

	public override int TakeDamage (int DamageTaken, Elements DamageElement)
	{
		if (nextRoute != null)
			DamageTaken /= 10;
		return base.TakeDamage (DamageTaken, DamageElement);
	}
}
