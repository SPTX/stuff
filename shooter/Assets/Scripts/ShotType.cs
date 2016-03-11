using UnityEngine;
using System.Collections;

public class ShotType : MonoBehaviour {

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
