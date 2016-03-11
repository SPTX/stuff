using UnityEngine;
using System.Collections;

public class ShotStraight : ShotType {

	private string Shot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public GameObject Fire(Vector3 shotOrigin, int power, string element = ""){

		Shot = "Shot" + element;

		Instantiate (Resources.Load (Shot), shotOrigin, Quaternion.identity);
		Instantiate (Resources.Load (Shot), shotOrigin, Quaternion.identity);
		if (power > 0)
		{
			Instantiate (Resources.Load (Shot), shotOrigin + Vector3.up * 0.2f, Quaternion.identity);
			Instantiate (Resources.Load (Shot), shotOrigin - Vector3.up * 0.2f, Quaternion.identity);
		}
		if (power > 1)
		{
			Instantiate (Resources.Load (Shot), shotOrigin + Vector3.up * 0.4f,  Quaternion.identity);
			Instantiate (Resources.Load (Shot), shotOrigin - Vector3.up * 0.4f,  Quaternion.identity);
		}
		///lovemax only
		if (power > 2)
		{
			Instantiate (Resources.Load (Shot), shotOrigin + Vector3.up * 0.6f,  Quaternion.identity);
			Instantiate (Resources.Load (Shot), shotOrigin - Vector3.up * 0.6f,  Quaternion.identity);
		}
		return (GameObject)Instantiate (Resources.Load (Shot), shotOrigin, transform.rotation);
	}
}
