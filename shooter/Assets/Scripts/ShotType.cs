using UnityEngine;
using System.Collections;

[System.Serializable]
public class ShotType : MonoBehaviour {

	public int damage = 10;
	public int maxBullets = 3;
	public float firerate = 0.04f;
	public Elements element = Elements.fire;
	public enum Type{Straight, Wide};
	public Type type;
	protected string ShotElem;

	public int healthMax = 900;
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
