using UnityEngine;
using System.Collections;

public class Route : MonoBehaviour {

	public Route nextRoute;
	public float waitBeforeNext = 0;
	public float useRotationTowardsThis = 0;
	public float speedTowards = 4;
	public float minDistance;
	public bool damageableUntilReached = true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
