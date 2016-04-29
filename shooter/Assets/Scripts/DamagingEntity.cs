using UnityEngine;
using System.Collections;

public class DamagingEntity : MonoBehaviour {

	public int damage = 100;
	public float scoreValue = 0.2f;
	public Elements element;
	protected bool damageable = true;
	protected bool canBeHit = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	virtual public void TakeDamage(int DamageTaken, Elements DamageElement){
		
	}
	
	virtual protected void Die(float elementMultiplier){

	}

	public Elements GetElement(){
		return element;
	}
}
