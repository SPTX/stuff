using UnityEngine;
using System.Collections;

public class Gargoyle : Moth {

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	protected override void Update () {
		turretUpper.LookAtPlayer ();
		turretUpper.transform.rotation *= Quaternion.AngleAxis (-15, Vector3.forward);
		turretUnder.LookAtPlayer();
		turretUnder.transform.rotation *= Quaternion.AngleAxis (15, Vector3.forward);
		base.Update ();
	}
}
