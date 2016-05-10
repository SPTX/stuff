using UnityEngine;
using System.Collections;

public class BossSkullExplosion : MonoBehaviour {

	Elements element;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > 6)
			Destroy (gameObject);
		transform.localScale +=  Vector3.one * 12f * Time.deltaTime;
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		other.GetComponent<DamagingEntity> ().TakeDamage (-2, element);
	}

	public void SetUp(Elements newElem)
	{
		element = newElem;
		GetComponent<SpriteRenderer> ().color = MapManager.elementColors [(int)element];
	}

}
