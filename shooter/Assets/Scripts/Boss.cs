using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : DamagingEntity {

	/* list of patterns:
	 * straight
	 * falling sides
	 * straight sides
	 * round
	 * seeking

	 * subboss patterns:
	 * straight snake line
	 * rotator
	*/

	public int health;
	protected int HPMax;
	protected float healthActual;
	protected float lastTakenDamageType;

	public Text HPText;
	public Image healthBar;
	public Elements element = Elements.fire;
	public Image seal;

	public GameObject turret;
	public float turretRefireDelay = 4;
	protected float turretRefire;
	public float turretFireDuration = 2;
	protected float turretFiring;
	public float turretRotSpeed = 90;
	protected float nextShot = 0;
	protected int type = 0;
	protected string[] deathShotNames = {"DeathShot", "DeathShotSmall"};

	public float timer = 120;

	protected float respawnSkulls = 5;
	protected float respawnSkullsDelay = 5;

	// Use this for initialization
	void Start () {
		seal.sprite = Resources.Load<Sprite>("Sprites/Seal-" + element);
		healthActual = HPMax = health;
		damageable = false;
		MapManager.Manager.bossTime = timer;
		turretRefire = turretRefireDelay;
		turretFiring = turretFireDuration + turretRefireDelay;
	}
	
	// Update is called once per frame
	void Update () {
		if (!damageable) {
			transform.Translate (Vector3.left * 4 * Time.deltaTime);
			if (transform.position.x <= 4)
				damageable = true;
		}

		turret.transform.Rotate (Vector3.forward * turretRotSpeed * Time.deltaTime);
		if (MapManager.Manager.difficulty == MapManager.Difficulty.death && (turretRefire -= Time.deltaTime) <= 0 && (nextShot -= Time.deltaTime) <= 0) {
				MapManager.Manager.onScreenEntities.Add(
					((GameObject)Instantiate(Resources.Load(deathShotNames[type]), transform.position, turret.transform.rotation)).GetComponent<DeathShot> ());
				MapManager.Manager.onScreenEntities.Add(
					((GameObject)Instantiate(Resources.Load(deathShotNames[type]), transform.position, turret.transform.rotation * Quaternion.AngleAxis(120, Vector3.forward))).GetComponent<DeathShot> ());
				MapManager.Manager.onScreenEntities.Add(
					((GameObject)Instantiate(Resources.Load(deathShotNames[type]), transform.position, turret.transform.rotation * Quaternion.AngleAxis(240, Vector3.forward))).GetComponent<DeathShot> ());
				if ((type ^= 1) == 0)
					nextShot = 0.12f;
				else
					nextShot = 0.06f;
		}
		if ((turretFiring -= Time.deltaTime) <= 0) {
			turretRefire = turretRefireDelay;
			turretFiring = turretFireDuration + turretRefireDelay;
			turretRotSpeed = -turretRotSpeed;
		}

		if (health > healthActual && (health -= (int)((health - healthActual) * (Time.deltaTime * 2))) <= 0)
			Die (lastTakenDamageType);
		HealthBarProcessing ();

		if ((respawnSkulls -= Time.deltaTime) <= 0) {
			respawnSkulls = respawnSkullsDelay;
			BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position, Quaternion.AngleAxis(160, Vector3.forward))).GetComponent<BossSkull> ();
			newSkull.SetUp((Elements)Random.Range(0, 5), BossSkull.Pattern.seeking);
			MapManager.Manager.bossSkulls.Add(newSkull);
		}
	}

	override public void TakeDamage(int DamageTaken, Elements DamageElement){
		if (!damageable)
			return;
		//solve element returns 2, 0.5 or 1 (*2, /2, *1)
		lastTakenDamageType = MapManager.Manager.SolveElement (DamageElement, element);
		healthActual -= (DamageTaken * lastTakenDamageType);
		//add score
	}
	
	protected override void Die (float elementMultiplier)
	{
		MapManager.Manager.AddScore (0, lastTakenDamageType, false, 2);
		MapManager.Manager.bossTime = 0;
		Destroy (gameObject);
	}

	protected void HealthBarProcessing(){
		HPText.text = health.ToString ();
		Vector3 newBarSize = healthBar.transform.localScale;
		newBarSize.x = Mathf.Clamp01 ((health * 100f / HPMax) / 100f);
		healthBar.transform.localScale = newBarSize;
		healthBar.rectTransform.anchoredPosition = Vector2.right * (healthBar.rectTransform.sizeDelta.x / 2 * newBarSize.x);
	}
}

