using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossSkull : DamagingEntity {

	public enum Pattern	{straight, fallingFromSides, side, round, seeking, snake, rotator};

	protected int healthMax = 250;
	protected int health;
	protected float lifeTime = 12;

	protected float moveSpeed = 0;
	protected float moveSpeedMax = 5;
	protected float rotatingSpeed = 0;
	protected Vector3 direction;
	protected Pattern pattern;
	protected bool accelerating;

	protected Vector3 initialBarPos;
	protected bool spawnEffect = true;
	public GameObject LockRing;
	public Image healthBar;
	public Canvas can;

	public Turret turret;
	private float refire;
	private float firerate = 7;

	protected bool ringActive;
	protected Vector3 ringSize;
	public GameObject Glow;

	// Use this for initialization
	void Start () {
		health = healthMax;
		transform.localScale = Vector3.zero;
		canBeHit = false;
	}

	// Update is called once per frame
	void Update () {
		if (!MapManager.WithinBounds (transform.position, 32, 32) || (lifeTime -= Time.deltaTime) < 0)
			Destroy (gameObject);
		if (!MapManager.WithinBounds (transform.position, 10, 6)) {
			canBeHit = false;
			ringActive = false;
			LockRing.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
		} else {
			canBeHit = true;
			SetRing();
		}

		if (ringActive && LockRing.transform.localScale.x > ringSize.x) {
			Color newcol = LockRing.GetComponent<SpriteRenderer>().color;
			newcol.a += 1 * Time.deltaTime;
			if ((LockRing.transform.localScale -= Vector3.one * Time.deltaTime).x <= ringSize.x)
				newcol.a = 1;
			LockRing.GetComponent<SpriteRenderer>().color = newcol;
		}

		if (spawnEffect && transform.localScale.x < 1) {

			Vector3 newScale = transform.localScale;
			newScale += Vector3.one * Time.deltaTime;
			transform.localScale = newScale;
			Glow.transform.localScale -= Vector3.one * 4f * Time.deltaTime;
			if (newScale.x >= 1)
			{
				transform.localScale = Vector3.one;
				spawnEffect = false;
				canBeHit = true;
				Destroy(Glow.gameObject);
			}
			return;
		}

		Move ();
		if (MapManager.Manager.difficulty > MapManager.Difficulty.easy)
			fire ();

		can.transform.rotation = Quaternion.identity;
		LockRing.transform.Rotate (Vector3.back * 45 * Time.deltaTime);
	}

	protected void Move()
	{
		if (pattern == Pattern.snake && !accelerating) {
			 if ((moveSpeed -= 6 * Time.deltaTime) <= 0)
				accelerating = true;
		}
		else if (moveSpeed < moveSpeedMax && (moveSpeed += 12 * Time.deltaTime) >= moveSpeedMax) {
			moveSpeed = moveSpeedMax;
			accelerating = false;
		}

		if (pattern == Pattern.fallingFromSides)
			transform.Translate (direction * moveSpeed * Time.deltaTime, Space.World);
		else
			transform.Translate (transform.right * moveSpeed * Time.deltaTime, Space.World);

		if (rotatingSpeed > 0)
			transform.Rotate (Vector3.forward * rotatingSpeed * Time.deltaTime);
		else if (pattern == Pattern.seeking && !MapManager.WithinBounds (transform.position, 15, 3.5f)) {
			if (transform.position.x < -14)
				Destroy(gameObject);
			transform.position -= Vector3.up * transform.position.normalized.y * 0.05f;
			transform.Rotate(Vector3.forward, Quaternion.Angle(transform.rotation, Quaternion.Euler(Vector3.right * transform.position.x)) * transform.position.normalized.y / 2);
		}
	}

	void fire ()
	{
		if (!MapManager.WithinBounds (transform.position, 7, 4))
		    return;
		if (pattern == Pattern.rotator || (refire -= Time.deltaTime) > 0)
			return;
		refire = firerate;
		Quaternion fireDirection = transform.rotation;
		if (pattern == Pattern.round || pattern == Pattern.side)
			fireDirection *= Quaternion.AngleAxis (180, Vector3.forward);
		else if (pattern == Pattern.seeking)
			fireDirection = turret.transform.rotation;
		else if (pattern == Pattern.fallingFromSides)
			fireDirection = Quaternion.identity * Quaternion.AngleAxis (180, Vector3.forward);
		Shot shot = ((GameObject)Instantiate (Resources.Load ("ShotEnemy"), transform.position, fireDirection)).GetComponent<Shot> ();
		if (pattern == Pattern.side)
			shot.speed = 1;
		MapManager.Manager.onScreenEntities.Add (shot);
	}
	
	override public int TakeDamage(int DamageTaken, Elements DamageElement)
	{
		if (DamageTaken < -1)
			Die (0.5f);
		else if (DamageTaken < 0)
			Die (1);
		if (pattern != Pattern.round && pattern != Pattern.snake)
			moveSpeed = 0;
		turret.HitEffect ();
		float damElem = MapManager.SolveElement (DamageElement, element);
		if (damElem != 2)
			return DamageTaken;
		if ((health -= DamageTaken) <= 0)
			Die(damElem);
		HealthBarProcessing ();
		return DamageTaken;
	}

	protected void HealthBarProcessing(){
		can.gameObject.SetActive(true);
		Vector3 newBarSize = healthBar.transform.localScale;
		newBarSize.x = Mathf.Clamp01 ((health * 100f / healthMax) / 100f);
		healthBar.transform.localScale = newBarSize;
		healthBar.rectTransform.anchoredPosition = Vector2.right * (healthBar.rectTransform.sizeDelta.x / 2 * newBarSize.x);
	}

	public void SetUp(Elements setElem, Pattern newPattern = Pattern.straight, float newMaxSpeed = 5)
	{
		element = setElem;
		pattern = newPattern;
		direction = transform.right;
		if (newPattern == Pattern.fallingFromSides)
			direction += Vector3.left * 0.5f;
		turret.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Skull-" + element);
		if (newPattern == Pattern.round) {
			rotatingSpeed = 24;
			turret.SetFireRate(firerate = 3.4f - (int)MapManager.Manager.difficulty, false);
			refire = 0;
		}
		else {
			turret.SetFireRate((firerate -= (int)MapManager.Manager.difficulty));
			refire = Random.Range (0, firerate);
		}

		if (newPattern == Pattern.snake)
			accelerating = true;
		moveSpeedMax = newMaxSpeed;
		Glow.GetComponent<SpriteRenderer> ().color = MapManager.elementColors [(int)element];
		SetRing ();
	}

	public void SetRing()
	{
		if (MapManager.SolveElement (
			MapManager.PlayerCharacter.equipedShotTypes [MapManager.PlayerCharacter.equipedShot].element, element) < 2) {
			ringActive = false;
			LockRing.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
		}
		else if (!ringActive) {
			ringActive = true;
			if (ringSize.x <= 0)
				ringSize = LockRing.transform.localScale;
			LockRing.transform.localScale = ringSize * 2;
		}
	}

	protected override void Die (float elementMultiplier)
	{
		MapManager.Manager.AddScore (scoreValue, elementMultiplier, false, 0);
		MapManager.PlayerCharacter.ComboAdd (1, transform.position);

		if (elementMultiplier == 0.5f) {
			((GameObject)Instantiate (Resources.Load ("BrokenSkull"), transform.position, turret.transform.rotation))
				.GetComponent<BrokenSkull>().SetUp(MapManager.IsSensibleTo(MapManager.Manager.onScreenEntities [0].GetElement()));
		}
		else if (elementMultiplier == 2) {
			if (MapManager.IsSensibleTo(MapManager.Manager.onScreenEntities [0].GetElement())
				== MapManager.IsSensibleTo(element))
				((GameObject)Instantiate(Resources.Load("BossSkullExplosion"), transform.position, Quaternion.identity)).
					GetComponent<BossSkullExplosion>().SetUp(element);
				((GameObject)Instantiate (Resources.Load ("BrokenSkull"), transform.position, turret.transform.rotation))
				.GetComponent<BrokenSkull>().SetUp(MapManager.IsSensibleTo(element));
		}
		if (MapManager.Manager.bossTime < 0) {
			MapManager.Manager.SpawnMaterial (1, 2, transform.position);
			Instantiate (Resources.Load ("StarBig"), transform.position, Quaternion.identity);
		}
		Destroy (gameObject);
	}
}
