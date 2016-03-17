using UnityEngine;
using System.Collections;

public class ShotType : MonoBehaviour {

	protected int damage = 100;
	public int maxBullets = 3;
	public float firerate = 0.04f;
	public string element = "Fire";
	protected string ShotElem;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual GameObject Fire(Vector3 shotOrigin, int power){
		return new GameObject();
	}
}
