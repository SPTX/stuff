using UnityEngine;
using System.Collections;

public class LoveMaxGraphic : MonoBehaviour {

	public GameObject Love;
	public GameObject Max;
	public Vector3 centerLove;
	public Vector3 centerMax;
	public float speed = 4;
	public float accel = 0.025f;
	public float deccelDistance = 2;
	public float reaccelMultiplier = 10;
	protected bool arrived;

	public GameObject Heart;
	public float HeartVerticalGrowSpeed = 2;
	public float HeartHorizontalGrowSpeed = 2;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!arrived) {
			if (Vector3.Distance (Love.transform.position, centerLove) < deccelDistance)
			if ((speed -= accel) <= 0)
				speed = 0.1f;
			Love.transform.position = Vector3.MoveTowards (Love.transform.position, centerLove, speed * Time.deltaTime);
			Max.transform.position = Vector3.MoveTowards (Max.transform.position, centerMax, speed * Time.deltaTime);
			if (Love.transform.position == centerLove)
				arrived = true;
		} else {
			speed += accel * reaccelMultiplier;
			Love.transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
			Max.transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
			if (MapManager.Manager.InLove() == 0)
				Destroy(gameObject);
		}

		if (Heart.transform.localScale.y < 30)
			Heart.transform.localScale += Vector3.up * HeartVerticalGrowSpeed * Time.deltaTime;
		else if (Heart.transform.localScale.x < 40)
			Heart.transform.localScale += Vector3.right * HeartVerticalGrowSpeed * Time.deltaTime;
	}
}
