using UnityEngine;
using System.Collections;

public class Shot : DamagingEntity {

	public int damage;
	protected float life = 30;
	public float speed = 4;

	// Use this for initialization
	void Start () {
		damage = MapManager.PlayerCharacter.equipedShotTypes[MapManager.PlayerCharacter.equipedShot].damage;
	}
	
	// Update is called once per frame
	protected virtual void Update () {
		transform.Translate (transform.right * speed * Time.deltaTime, Space.World);
		if (transform.position.x < -10)
			Destroy (gameObject);
		else if (transform.position.x > 10)
			Destroy (gameObject);
		if (transform.position.y < -6)
			Destroy (gameObject);
		else if (transform.position.y > 6)
			Destroy (gameObject);

		if ((life = life - Time.deltaTime) < 0)
			Destroy (gameObject);
	}

	override public int TakeDamage(int DamageTaken, Elements DamageElement)
	{
		Instantiate (Resources.Load ("StarSmall"), transform.position, Quaternion.identity);
		Destroy (gameObject);
		return DamageTaken;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<DamagingEntity> ().TakeDamage (damage, element) == 0)
			return;
		MapManager.Manager.AddLove(0.25f);
		Destroy (gameObject);
	}

}
