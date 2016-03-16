using UnityEngine;
using System.Collections;

public class ShotType : MonoBehaviour {

	protected string element;
	public float firerate = 0.04f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public virtual GameObject Fire(Vector3 shotOrigin, int power, string element = "Fire"){
		return new GameObject();
	}
}
