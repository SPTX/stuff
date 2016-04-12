using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossSkull : DamagingEntity {

	public enum Pattern	{straight, round, seeking};

	protected int healthMax = 3000;
	protected int health;

	protected float moveSpeed = 3;
	protected float moveSpeedMax;
	protected float rotatingSpeed = 0;
	protected bool seeking;

	protected Vector3 initialBarPos;
	public GameObject LockRing;
	public Image healthBar;
	public Canvas can;

	public Turret turret;

	protected bool ringActive;
	protected Vector3 ringSize;
	
	public Elements element = Elements.fire;

	// Use this for initialization
	void Start () {
		health = healthMax;
		moveSpeedMax = moveSpeed;
		turret.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Skull-" + element);
	}

	// Update is called once per frame
	void Update () {
		if (!MapManager.Manager.WithinBounds (transform.position, 15, 15))
			Destroy (gameObject);

		if (ringActive && LockRing.transform.localScale.x > ringSize.x) {
			Color newcol = LockRing.GetComponent<SpriteRenderer>().color;
			newcol.a += 1 * Time.deltaTime;
			if ((LockRing.transform.localScale -= Vector3.one * Time.deltaTime).x <= ringSize.x)
				newcol.a = 1;
			LockRing.GetComponent<SpriteRenderer>().color = newcol;
		}

		if (moveSpeed < moveSpeedMax && (moveSpeed += 4 * Time.deltaTime) > moveSpeedMax)
			moveSpeed = moveSpeedMax;
		LockRing.transform.Rotate (Vector3.forward * 45 * Time.deltaTime);

		transform.Translate (transform.right * moveSpeed * Time.deltaTime, Space.World);

		if (rotatingSpeed > 0)
			transform.Rotate (Vector3.forward * rotatingSpeed * Time.deltaTime);
		else if (seeking && !MapManager.Manager.WithinBounds (transform.position, 15, 3.5f)) {

		}

		can.transform.rotation = Quaternion.identity;
	}

	override public void TakeDamage(int DamageTaken, Elements DamageElement)
	{
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

	public void SetUp(Elements setElem, Pattern pattern = Pattern.straight)
	{
		element = setElem;
		turret.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprites/Skull-" + element);
		if (pattern == Pattern.round)
			rotatingSpeed = 24;
		else if (pattern == Pattern.seeking)
			seeking = true;
		SetRing ();
	}

	public void SetRing()
	{
		if (MapManager.Manager.SolveElement (
			MapManager.PlayerCharacter.equipedShotTypes [MapManager.PlayerCharacter.equipedShot].element, element) == 2) {
			ringActive = true;
			if (ringSize.x <= 0)
				ringSize = LockRing.transform.localScale;
			LockRing.transform.localScale = ringSize * 2;
		}
		else{
			ringActive = false;
			LockRing.GetComponent<SpriteRenderer>().color = new Color(1,1,1,0);
		}
	}

	protected override void Die (float elementMultiplier)
	{
		Destroy (gameObject);
	}
}
