using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	private float decreasedDelay = 0;
	private float nextSpawnSpacing = 0;

	public enum patternType{Line, Square};
	public float startTime;
	public int quantity;
	public float delay;
	public GameObject enemyType;
//	public patternType pattern;
	public float spacing;

	// Use this for initialization
	void Start () {
		if (enemyType == null)
			Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (quantity == 0)	
			Destroy (gameObject);
		if (startTime > 0)
			startTime -= Time.deltaTime;
		else {
			///start spwaning
			if ((decreasedDelay -= Time.deltaTime) <= 0){
				Spawn ();
				decreasedDelay = delay;
			}
		}
	}

	void Spawn(){
		MapManager.Manager.onScreenEntities.Add (
			((GameObject)Instantiate(enemyType, transform.position - transform.right * nextSpawnSpacing, Quaternion.identity)).GetComponent<DamagingEntity>());

		nextSpawnSpacing += spacing;
		if (quantity > 0) {
			quantity -= 1;
			if (delay <= 0)
				Spawn ();
		}
	}
}
