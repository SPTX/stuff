using UnityEngine;
using System.Collections;

public class DeathShot : Shot {

	public GameObject graphic;

	// Use this for initialization
	void Start () {
		speed = 5;
	}
	
	// Update is called once per frame
	override protected void Update () {
		graphic.transform.Rotate (Vector3.forward * 360 * Time.deltaTime);
		base.Update ();
	}

	void OnTriggerEnter2D(Collider2D other){
		//Destroy (gameObject);
	}
	
}
