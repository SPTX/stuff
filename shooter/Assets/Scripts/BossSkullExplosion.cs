using UnityEngine;
using System.Collections;

public class BossSkullExplosion : MonoBehaviour {

	Elements element;
	protected bool expanding = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > 6)
			expanding = false;
		else if (transform.localScale.x <= 0) {
				Destroy (gameObject);
		}

		if (expanding)
			transform.localScale +=  Vector3.one * 12f * Time.deltaTime;
		else
			transform.localScale -=  Vector3.one * 24f * Time.deltaTime;
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
