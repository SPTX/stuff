using UnityEngine;
using System.Collections;

public class ProjectileHead : Turret {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Vector3.forward * 180 * Time.deltaTime);
	}
}
