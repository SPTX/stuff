using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossSkull : DamagingEntity {

	public enum Pattern	{straight, fallingFromSides, side, round, seeking, snake, rotator};

	protected int healthMax = 2500;
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

	protected bool ringActive;
	protected Vector3 ringSize;
	private Vector3 glowSize = Vector3.one * 4;
	public GameObject Glow;
	
	public Elements element = Elements.fire;

	private Color[] haloColors = {
		new Color(1,0.3f,0.24f),
		new Color(0.1f,0.84f,0.008f),
		new Color(0.02f,0.5f,0.82f),
		new Color(1,1,1),
		new Color(0.65f,0.25f,1)};
	
	// Use this for initialization
	void Start () {
		health = healthMax;
		turret.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Skull-" + element);
		turret.SetFireRate(2);
		transform.localScale = Vector3.zero;
		canBeHit = false;
	}

	// Update is called once per frame
	void Update () {
		if (!MapManager.Manager.WithinBounds (transform.position, 32, 32) || (lifeTime -= Time.deltaTime) < 0)
			Destroy (gameObject);
		if (!MapManager.Manager.WithinBounds (transform.position, 10, 6)) {
			canBeHit = false;
		} else
			canBeHit = true;

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
//			if ((Glow.transform.localScale += glowSize * Time.deltaTime).x > 2)
//			{
//				Glow.transform.localScale = Vector3.one * 2;
//				glowSize *= -2f * Time.deltaTime;
			Glow.transform.localScale -= Vector3.one * 4f * Time.deltaTime;
//			}
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

		can.transform.rotation = Quaternion.identity;
		LockRing.transform.Rotate (Vector3.back * 45 * Time.deltaTime);

		if (MapManager.Manager.difficulty > MapManager.Difficulty.easy)
			turret.Shoot ();
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

		transform.Translate (transform.right * moveSpeed * Time.deltaTime, Space.World);
		
		if (rotatingSpeed > 0)
			transform.Rotate (Vector3.forward * rotatingSpeed * Time.deltaTime);
		else if (pattern == Pattern.seeking && !MapManager.Manager.WithinBounds (transform.position, 15, 3.5f)) {
			if (transform.position.x < -14)
				Destroy(gameObject);
			transform.position -= Vector3.up * transform.position.normalized.y * 0.05f;
			transform.Rotate(Vector3.forward, Quaternion.Angle(transform.rotation, Quaternion.Euler(Vector3.right * transform.position.x)) * transform.position.normalized.y / 2);
		}
	}

	override public void TakeDamage(int DamageTaken, Elements DamageElement)
	{
		if (pattern != Pattern.round && pattern != Pattern.snake)
			moveSpeed = 0;
		turret.HitEffect ();
		float damElem = MapManager.Manager.SolveElement (DamageElement, element);
		if (damElem != 2)
			return;
		if ((health -= DamageTaken) <= 0)
			Die(damElem);
		HealthBarProcessing ();
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
		if (newPattern == Pattern.round)
			rotatingSpeed = 24;
		else if (newPattern == Pattern.snake)
			accelerating = true;
		moveSpeedMax = newMaxSpeed;
		Glow.GetComponent<SpriteRenderer> ().color = haloColors [(int)element];
		SetRing ();
	}

	public void SetRing()
	{
		if (MapManager.Manager.SolveElement (
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
		Destroy (gameObject);
	}
}
