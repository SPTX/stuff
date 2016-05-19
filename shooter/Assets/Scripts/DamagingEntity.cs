using UnityEngine;
using System.Collections;

public class DamagingEntity : MonoBehaviour {

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

	virtual public int TakeDamage(int DamageTaken, Elements DamageElement){
		return 0;
	}
	
	virtual protected void Die(float elementMultiplier){

	}

	public Elements GetElement(){
		return element;
	}
}
