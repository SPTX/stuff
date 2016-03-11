using UnityEngine;
using System.Collections;

public class ShotWide : ShotType {

	private float shotAngle = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	override public GameObject Fire(Vector3 shotOrigin, int power){

		Instantiate (Resources.Load ("Shot"), shotOrigin,
		             Quaternion.Euler(0, 0, shotAngle));
		Instantiate (Resources.Load ("Shot"), shotOrigin,
		             Quaternion.Euler(0, 0, -shotAngle));
		if (power > 0)
		{
			Instantiate (Resources.Load ("Shot"), shotOrigin + Vector3.up * 0.4f,
			             Quaternion.Euler(0, 0, shotAngle));
			Instantiate (Resources.Load ("Shot"), shotOrigin - Vector3.up * 0.4f,
			             Quaternion.Euler(0, 0, -shotAngle));
		}
		if (power > 1)
		{
			Instantiate (Resources.Load ("Shot"), shotOrigin + Vector3.up * 0.2f, transform.rotation);
			Instantiate (Resources.Load ("Shot"), shotOrigin - Vector3.up * 0.2f, transform.rotation);
		}
		return (GameObject)Instantiate (Resources.Load ("Shot"), shotOrigin, transform.rotation);
	}
}
