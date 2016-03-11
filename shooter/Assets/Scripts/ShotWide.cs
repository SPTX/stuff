using UnityEngine;
using System.Collections;

public class ShotWide : ShotType {

	private float shotAngle = 5f;
	private string Shot;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public GameObject Fire(Vector3 shotOrigin, int power, string element = ""){

		Shot = "Shot" + element;
		
		Instantiate (Resources.Load (Shot), shotOrigin,
		             Quaternion.Euler(0, 0, shotAngle));
		Instantiate (Resources.Load (Shot), shotOrigin,
		             Quaternion.Euler(0, 0, -shotAngle));
		if (power > 0)
		{
			Instantiate (Resources.Load (Shot), shotOrigin + Vector3.up * 0.3f, Quaternion.Euler(0, 0, shotAngle));
			Instantiate (Resources.Load (Shot), shotOrigin - Vector3.up * 0.3f, Quaternion.Euler(0, 0, -shotAngle));
		}
		if (power > 1)
		{
			Instantiate (Resources.Load (Shot), shotOrigin + Vector3.up * 0.35f, transform.rotation);
			Instantiate (Resources.Load (Shot), shotOrigin - Vector3.up * 0.35f, transform.rotation);
		}
		///lovemax only
		if (power > 2)
		{
			Instantiate (Resources.Load (Shot), shotOrigin + Vector3.up * 0.6f, Quaternion.Euler(0, 0, shotAngle + 4f));
			Instantiate (Resources.Load (Shot), shotOrigin - Vector3.up * 0.6f, Quaternion.Euler(0, 0, -shotAngle - 4f));
		}
		return (GameObject)Instantiate (Resources.Load (Shot), shotOrigin, transform.rotation);
	}
}
