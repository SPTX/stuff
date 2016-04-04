using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Enemy : DamagingEntity {

	protected int health;
	public int HealthMax = 200;
	public bool big;
	public bool spawnEffect = false;
	protected Vector3 initialBarPos;
	public GameObject LockRing;
	public int materials = 0;
	public int materialSize = 1;
	public Image healthBar;
	public Canvas can;
	public Turret turret;

	public string element = "Fire";

	// Use this for initialization
	protected void Start () {
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
	protected void Update () {
		if (spawnEffect && transform.localScale.x < 1) {
			Vector3 newScale = transform.localScale;
			newScale += Vector3.one * 2 * Time.deltaTime;
			transform.localScale = newScale;
			if (newScale.x >= 1)
			{
				transform.localScale = Vector3.one;
				spawnEffect = false;
				canBeHit = true;
				damageable = true;
			}
		}

	}

	new public virtual void TakeDamage(int DamageTaken, string DamageElement)
	{
		if (!canBeHit)
			return;

		//do things for big enemies (combo add, spawn stars on "death" difficulty)
		if (big)
			MapManager.PlayerCharacter.ComboAdd (1);

		if (!damageable)
			return;
		//solve element returns 2, 0.5 or 1 (*2, /2, *1)
		float damMul = MapManager.Manager.SolveElement (DamageElement, element);
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

	new void Die(float elementMultiplier){
		//Spawn explosin effect or whatever
		MapManager.PlayerCharacter.ComboAdd (1);
		MapManager.Manager.AddScore(scoreValue, elementMultiplier, LockRing.activeSelf, (big ? 1 : 0));
		if (LockRing.activeSelf && MapManager.Manager.materialSpawned < 1000) {
			MapManager.Manager.SpawnMaterial (materials, materialSize, transform.position);
/*			while (materials-- > 0)
			{
				Vector3 randvector = Vector3.zero;
				randvector.x = Random.Range(-0.5f, 0.5f);
				randvector.y = Random.Range(-0.5f, 0.5f);
				Pickup mat = ((GameObject)Instantiate (Resources.Load ("Material"), transform.position + randvector, Quaternion.identity)).GetComponent<Pickup>();
				mat.value = Mathf.Clamp(materialSize, 1, 1000 - MapManager.Manager.material);
				mat.transform.localScale *= Mathf.Clamp(materialSize / 2, 0.75f, 2.5f);
				MapManager.Manager.materialSpawned += mat.value;
			}
			*/
		}
		Destroy (gameObject);
	}
}
