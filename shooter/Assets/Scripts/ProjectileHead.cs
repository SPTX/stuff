using UnityEngine;
using System.Collections;

public class ProjectileHead : Turret {

	// Use this for initialization
	override protected void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	override protected void Update () {
		transform.Rotate (Vector3.forward * 360 * Time.deltaTime);
	}
}
