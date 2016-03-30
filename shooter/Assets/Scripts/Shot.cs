using UnityEngine;
using System.Collections;

public class Shot : DamagingEntity {

	protected float life = 30;
	protected float speed = 40f;
	public string element;

	// Use this for initialization
	void Start () {
		damage = MapManager.PlayerCharacter.equipedShotTypes[MapManager.PlayerCharacter.equipedShot].damage;
	}
	
	// Update is called once per frame
	protected void Update () {
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

	void OnTriggerEnter2D(Collider2D other)
	{
		MapManager.Manager.AddLove(0.25f);
		other.GetComponent<Enemy> ().TakeDamage (damage, element);
		Destroy (gameObject);
	}
}
