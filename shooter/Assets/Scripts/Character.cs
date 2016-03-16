using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

	private float refire = 0;
	private int maxBullets = 5;
	public List<GameObject> shots;
	public List<ShotType> equipedShotTypes;
	public int equipedShot = 0;
	public int power = 0;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent <ShotStraight>();
		gameObject.AddComponent <ShotWide>();
		equipedShotTypes.AddRange(GetComponents<ShotType>());
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.timeScale == 0)
			return;
		Move ();
		ClearShots ();
		if (Input.GetMouseButton (0))
			Fire ();
		if (Input.GetMouseButtonUp (1))
			equipedShot ^= 1;

		////debug
		if (Input.GetKeyDown("o"))
			++power;
		if (Input.GetKeyDown("l"))
			--power;
	}

	void Move(){

		transform.Translate (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0);
		Vector2 newPos = transform.position;
		
		if (transform.position.x < -8)
			newPos.x = -8;
		else if (transform.position.x > 8)
			newPos.x = 8;
		if (transform.position.y < -3.5f)
			newPos.y = -3.5f;
		else if (transform.position.y > 3.5f)
			newPos.y = 3.5f;
		transform.position = newPos;
	}

	void ClearShots(){
		for (int i = 0; i < shots.Count; ++i) {
			if (shots[i] == null)
				shots.RemoveAt(i);
		}
	}

	void Fire(){
		if (refire <= 0 && shots.Count < maxBullets) {

			shots.Add(equipedShotTypes[equipedShot].Fire (transform.position + Vector3.right * 0.4f, power));
			refire = equipedShotTypes[equipedShot].firerate;
		} else
			refire = refire - Time.deltaTime;
	}

}
