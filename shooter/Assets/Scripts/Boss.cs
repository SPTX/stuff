using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Boss : DamagingEntity {

	public int health;
	public Text HPText;
	public string element = "fire";
	public GameObject turret;
	public Image seal;
	protected float healthActual;
	protected float lastTakenDamageType;
	public float turretRefireDelay = 3;
	protected float turretRefire;
	public float turretFireDuration = 2;
	protected float turretFiring;
	protected float nextShot = 0;
	protected int type = 1;

	protected string[] deathShotNames = {"DeathShot", "DeathShotSmall"};

	// Use this for initialization
	void Start () {
		seal.sprite = Resources.Load<Sprite>("Sprites/Seal-" + element);
		healthActual = health;
		damageable = false;
		MapManager.Manager.bossTime = true;
		turretRefire = turretRefireDelay;
		turretFiring = turretFireDuration;
	}
	
	// Update is called once per frame
	void Update () {
		if (!damageable) {
			transform.Translate (Vector3.left * 4 * Time.deltaTime);
			if (transform.position.x <= 6)
				damageable = true;
		}

		turret.transform.Rotate (Vector3.forward * 720 * Time.deltaTime);
		if (MapManager.Manager.difficulty == MapManager.Difficulty.death && (turretRefire -= Time.deltaTime) <= 0) {
			//issues with number of shots
			if ((turretFiring -= Time.deltaTime) >= 0 && (nextShot -= Time.deltaTime) <= 0){
				MapManager.Manager.onScreenEntities.Add(
				((GameObject)Instantiate(Resources.Load(deathShotNames[type ^= 1]), transform.position, turret.transform.rotation)).GetComponent<DeathShot> ());
				if (type == 0)
					nextShot = 0.01f;
				else
					nextShot = 0.02f;
			}
			else {
				turretRefire = turretRefireDelay;
				turretFiring = turretFireDuration;
			}
		}

		if (health > healthActual && (health -= (int)((health - healthActual) * (Time.deltaTime * 2))) <= 0)
			Die (lastTakenDamageType);
		HPText.text = health.ToString ();
	}

	override public void TakeDamage(int DamageTaken, string DamageElement){
		if (!damageable)
			return;
		//solve element returns 2, 0.5 or 1 (*2, /2, *1)
		lastTakenDamageType = MapManager.Manager.SolveElement (DamageElement, element);
		healthActual -= (DamageTaken * lastTakenDamageType);
	}

	protected override void Die (float elementMultiplier)
	{
		MapManager.Manager.AddScore (0, lastTakenDamageType, false, 2);
		MapManager.Manager.bossTime = false;
		Destroy (gameObject);
	}
}