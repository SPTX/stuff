﻿using UnityEngine;
using System.Collections;

public class BrokenSkull : MonoBehaviour {

	Transform target;
	float maxSpeed = 6;
	float speed = 6;
	public Elements element;
	public GameObject turret;

	// Use this for initialization
	void Start () {
		if (MapManager.Manager.onScreenEntities.Count > 0) {
			target = MapManager.Manager.onScreenEntities [MapManager.Manager.onScreenEntities.Count - 1].transform;
			element = MapManager.IsSensibleTo(MapManager.Manager.onScreenEntities [MapManager.Manager.onScreenEntities.Count - 1].element);
			turret.GetComponent<SpriteRenderer> ().color = MapManager.elementColors [(int)element];
		}

	}

	// Update is called once per frame
	void Update () {
		turret.transform.Rotate (Vector3.back * 1000 * Time.deltaTime);
		if ((target && Vector3.Distance (transform.position, target.position) < 1) || !MapManager.WithinBounds (transform.position, 10, 10))
			Destroy (gameObject);

		if (target)
			RotateTowardsBoss ();
		if (speed < maxSpeed)
			speed += 30 * Time.deltaTime;
		transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
	}

	void RotateTowardsBoss()
	{
		Vector2 dir = transform.InverseTransformPoint(target.position);
		float angle = Vector2.Angle(Vector2.right, dir);
		angle = dir.y < 0 ? -angle : angle;
		if (angle > 75 || angle < -75)
			speed = 1;
		transform.Rotate(Vector3.forward, 2 * Time.deltaTime * angle);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		Elements damElem = MapManager.IsSensibleTo (other.gameObject.GetComponent<DamagingEntity>().element);
		other.GetComponent<DamagingEntity> ().TakeDamage (14, damElem);
			speed = 1;
	}
}
