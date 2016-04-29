using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : DamagingEntity {

	protected int health;
	public int HealthMax = 200;
	public bool big;

	public bool spawnEffect = false;
	public float moveSpeed = 5;
	public float timeToFlee = 12;
	public Vector3 fleeingDirection = Vector3.up;
	public Route route;
	public Route nextRoute;
	public bool seeking;

	protected Vector3 initialBarPos;
	public GameObject LockRing;
	public int materials = 0;
	public int materialSize = 1;
	public Image healthBar;
	public Canvas can;
	public Turret turret;

	// Use this for initialization
	protected virtual void Start () {
		health = HealthMax;
		initialBarPos = healthBar.rectTransform.localPosition;
		if (spawnEffect) {
			transform.localScale = Vector3.zero;
			damageable = false;
			if (!big)
				canBeHit = false;
		}
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		if (spawnEffect && transform.localScale.x < 1) {
			Vector3 newScale = transform.localScale;
			newScale += Vector3.one * 2 * Time.deltaTime;
			transform.localScale = newScale;
			if (newScale.x >= 1)
			{
				transform.localScale = Vector3.one;
				spawnEffect = false;
				canBeHit = true;
				if (nextRoute)
					damageable = nextRoute.damageableUntilReached;
				else
					damageable = true;
			}
		}

		if ((timeToFlee -= Time.deltaTime) <= 0) {
			transform.Translate (fleeingDirection * moveSpeed * Time.deltaTime, Space.World);
		} else if (route)
			MoveToRoute ();
		else if (seeking) {
			SeekPlayer();
		}

		if (!MapManager.WithinBounds (transform.position, 10, 6)) {
			canBeHit = false;
		}
		if (!MapManager.WithinBounds (transform.position, 14, 10)) {
			Destroy (gameObject);
			if (route)
				Destroy(route.gameObject);
		}
		can.transform.rotation = Quaternion.identity;



	}

	override public void TakeDamage(int DamageTaken, Elements DamageElement)
	{
		if (!canBeHit)
			return;

		//do things for big enemies (combo add, spawn stars on "death" difficulty)
		if (big) {
			MapManager.PlayerCharacter.ComboAdd (0.4f);
			turret.HitEffect();
		}

		if (!damageable)
			return;
		//solve element returns 2, 0.5 or 1 (*2, /2, *1)
		float damMul = MapManager.SolveElement (DamageElement, element);
		health -= (int)(DamageTaken * damMul);

		if (health <= 0)
			Die(damMul);

		else {
			can.gameObject.SetActive(true);
			turret.HitEffect();
			healthBar.transform.localScale = new Vector3((health * 100 / HealthMax) / 100f, 1, 1);
			Vector3 newpos = healthBar.transform.localPosition;
			newpos.x = healthBar.transform.localScale.x / 2 - 0.5f;
			healthBar.transform.localPosition = newpos;
		}
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (other.name == "ring")
			LockRing.SetActive (true);
	}

	override protected void Die(float elementMultiplier){
		//Spawn explosin effect or whatever
		MapManager.PlayerCharacter.ComboAdd (1);
		MapManager.Manager.AddScore(scoreValue, elementMultiplier, LockRing.activeSelf, (big ? 1 : 0));
		if (LockRing.activeSelf && MapManager.Manager.materialSpawned < 1000) {
			MapManager.Manager.SpawnMaterial (materials, materialSize, transform.position);
		}
		Destroy (gameObject);
		if (route)
			Destroy(route.gameObject);
	}

	public void AddRoute(Route newRoute){
		route = newRoute;
		nextRoute = newRoute;
		moveSpeed = newRoute.speedTowards;
		damageable = nextRoute.damageableUntilReached;
	}

	void MoveToRoute(){
		if (!nextRoute)
			return;
		if (nextRoute.useRotationTowardsThis != 0) {
			transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
			transform.Rotate(Vector3.forward, nextRoute.useRotationTowardsThis * Time.deltaTime, Space.World);
		} else {
			transform.position = Vector3.MoveTowards (transform.position, nextRoute.transform.position, moveSpeed * Time.deltaTime);
		}
		if (Vector3.Distance (nextRoute.transform.position, transform.position) <= nextRoute.minDistance){
			Vector3 newRot = transform.rotation.eulerAngles;
			newRot.z = nextRoute.transform.rotation.eulerAngles.z;
			transform.rotation = Quaternion.Euler(newRot);
			if (nextRoute = nextRoute.nextRoute)
			{
				damageable = nextRoute.damageableUntilReached;
				moveSpeed = nextRoute.speedTowards;
			}
			else
			{
				moveSpeed = 4;
				damageable = true;
			}
		}
	}

	protected void SeekPlayer()
	{
		transform.Translate(transform.right * moveSpeed * Time.deltaTime, Space.World);
		if (MapManager.PlayerCharacter.transform.position.x > transform.position.x)
			return;
		Vector2 dir = transform.InverseTransformPoint(MapManager.PlayerCharacter.transform.position);
		float angle = Vector2.Angle(Vector2.right, dir);
		angle = dir.y < 0 ? -angle : angle;
		//		if (angle > 75 || angle < -75)
		//			speed = 0;
		transform.Rotate(Vector3.forward, 4 * Time.deltaTime * angle);
	}

	protected void Fire(){
		turret.Fire ();
	}

}
