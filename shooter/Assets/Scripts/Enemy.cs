using UnityEngine;
using System.Collections;

public class Enemy : DamagingEntity {

	protected int health = 300;
	public string element = "Fire";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void TakeDamage(int DamageTaken, string DamageElement)
	{
		
	}
}
