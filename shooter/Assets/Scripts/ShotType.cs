using UnityEngine;
using System.Collections;

public class ShotType : MonoBehaviour {

	public int damage = 100;
	public int maxBullets = 3;
	public float firerate = 0.04f;
	public Elements element = Elements.fire;
	protected string ShotElem;

	public int healthMax = 400;
	public int health;


	// Use this for initialization
	virtual protected void Start () {
		health = healthMax;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual GameObject Fire(Vector3 shotOrigin, int power){
		return new GameObject();
	}
}
