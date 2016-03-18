using UnityEngine;
using System.Collections;

public class ShotStraight : ShotType {

	// Use this for initialization
	new void Start () {
		base.Start ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public GameObject Fire(Vector3 shotOrigin, int power){

		ShotElem = "Shot" + element;

		if (power > 0)
		{
			Instantiate (Resources.Load (ShotElem), shotOrigin + Vector3.up * 0.1f, Quaternion.identity);
			Instantiate (Resources.Load (ShotElem), shotOrigin - Vector3.up * 0.1f, Quaternion.identity);
		}
		if (power > 1)
		{
			Instantiate (Resources.Load (ShotElem), shotOrigin + Vector3.up * 0.2f,  Quaternion.identity);
			Instantiate (Resources.Load (ShotElem), shotOrigin - Vector3.up * 0.2f,  Quaternion.identity);
		}
		///lovemax only
		if (power > 2)
		{
			Instantiate (Resources.Load (ShotElem), shotOrigin + Vector3.up * 0.3f,  Quaternion.identity);
			Instantiate (Resources.Load (ShotElem), shotOrigin - Vector3.up * 0.3f,  Quaternion.identity);
		}
		return (GameObject)Instantiate (Resources.Load (ShotElem), shotOrigin, transform.rotation);
	}
}
