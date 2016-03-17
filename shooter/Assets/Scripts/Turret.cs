using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour {

	protected float firerate = 0.5f;
	protected float refire = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		LookAtPlayer ();
		if (refire <= 0) {
			Instantiate (Resources.Load ("ShotEnemy"), transform.position, transform.localRotation);
			refire = firerate;
		}
		else
			refire = refire - Time.deltaTime;
	}

	void LookAtPlayer(){
		Quaternion rotation = Quaternion.LookRotation
			(2 * (MapManager.PlayerCharacter.transform.position + Vector3.up * 0.4f - transform.position),
			 transform.TransformDirection (Vector3.up));
		rotation.x = rotation.y = 0;
		transform.rotation = rotation;
		transform.RotateAround (transform.position, transform.up, 180f);
	}
}
