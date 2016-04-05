using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {

	private float decreasedDelay = 0;
	private float nextSpawnSpacing = 0;

	public enum patternType{Line, Square};
	public float startTime;
	public int quantity = 0;
	public float delay;
	public GameObject enemyType;
	public float spacing;
	public float timeToFlee = 12;
	public Vector3 fleeingDirection = Vector3.up;
	public Route route;

	// Use this for initialization
	void Start () {
		if (enemyType == null)
			Destroy (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		if (quantity <= 0) {
			Destroy (gameObject);
			return;
		}
		if (startTime > 0)
			startTime -= Time.deltaTime;
		else {
			///start spwaning
			if ((decreasedDelay -= Time.deltaTime) <= 0){
				decreasedDelay = delay;
				Spawn ();
			}
		}
	}

	void Spawn(){
		--quantity;
		Enemy newEnemy = ((GameObject)Instantiate (enemyType, transform.position - transform.right * nextSpawnSpacing, Quaternion.identity)).GetComponent<Enemy> ();
		newEnemy.timeToFlee = timeToFlee + delay * (quantity > 0 ? quantity : 1);
		newEnemy.fleeingDirection = fleeingDirection;
		if (route) {
			newEnemy.AddRoute((Route)Instantiate (route, newEnemy.transform.position + route.transform.localPosition, route.transform.rotation));
		}
		MapManager.Manager.onScreenEntities.Add (newEnemy);
		nextSpawnSpacing += spacing;
	}
}
