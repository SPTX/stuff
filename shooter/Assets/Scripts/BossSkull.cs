using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BossSkull : DamagingEntity {

	protected int health;
	protected int HealthMax;
	protected float moveSpeed = 5;
	public Vector3 direction = Vector3.left;

	protected Vector3 initialBarPos;
	public GameObject LockRing;
	public Image healthBar;
	public Canvas can;
	public Turret turret;
	
	public Elements element = Elements.fire;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
