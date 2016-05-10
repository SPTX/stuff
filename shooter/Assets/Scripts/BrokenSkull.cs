using UnityEngine;
using System.Collections;

public class BrokenSkull : MonoBehaviour {

	public Transform target;
	float maxSpeed = 6;
	float speed = 6;
	float deltatime = 0;
	public Elements element;
	public GameObject turret;

	// Use this for initialization
	void Start () {
		if (MapManager.Manager.onScreenEntities.Count > 0) {
			target = MapManager.Manager.onScreenEntities [0].transform;
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

	public void SetUp(Elements newElem){
		element = newElem;
		turret.GetComponent<SpriteRenderer> ().color = MapManager.elementColors [(int)element];
		GetComponent<TrailRenderer>().material.SetColor("_Color", MapManager.elementColors [(int)element]);
	}

	void RotateTowardsBoss()
	{
		Vector2 dir = transform.InverseTransformPoint(target.position);
		float angle = Vector2.Angle(Vector2.right, dir);
		angle = dir.y < 0 ? -angle : angle;
		if (angle > 75 || angle < -75)
			speed = 0;
		transform.Rotate(Vector3.forward, 2 * Time.deltaTime * angle);
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if ((deltatime += Time.deltaTime) >= 0.1f) {
			deltatime = 0;
			MapManager.PlayerCharacter.ComboAdd (0.25f);
			Elements damElem = MapManager.IsSensibleTo (other.gameObject.GetComponent<DamagingEntity> ().element);
			other.GetComponent<DamagingEntity> ().TakeDamage (10, damElem);
			speed = 1;
		}
	}
}
