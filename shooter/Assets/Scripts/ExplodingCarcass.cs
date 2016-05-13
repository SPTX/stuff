using UnityEngine;
using System.Collections;

public class ExplodingCarcass : MonoBehaviour {

	public GameObject graphic;
	public float shakingAmplitude = 2;
	public float shakingFrequency;

	public float xSpeed = -0.1f;
	public float ySpeed = -0.1f;

	public float rotationSpeed = 6;

	protected float nextShake;
	protected float carcassDuration = 4;
	protected float smallExplosionsDuration = 3;
	protected bool boss;

	protected float delayToChangeColor = 0.1f;
	protected float colorChangeDelay = 0;
	protected int altColorSelect = 0;
	protected Color[] altColor = {
		Color.white,
		Color.red};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		if ((carcassDuration -= Time.deltaTime) < 0) //spawn all star/material shit
		    Destroy(gameObject);

		if ((colorChangeDelay -= Time.deltaTime) <= 0) {
			graphic.GetComponent<SpriteRenderer> ().color = altColor [altColorSelect ^= 1];
			colorChangeDelay = delayToChangeColor;
		}
		transform.Translate (xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0, Space.World);
		graphic.transform.rotation *= Quaternion.AngleAxis (rotationSpeed * Time.deltaTime, Vector3.back);
		if ((nextShake -= Time.deltaTime) <= 0) {
			graphic.transform.localPosition = new Vector3 (Random.Range (-shakingAmplitude, shakingAmplitude), Random.Range (-shakingAmplitude, shakingAmplitude), 0);
			nextShake = shakingFrequency;
		}
	}

	public void SetUp(Sprite newSprite, Vector3 newSpriteScale, bool isBoss = false)
	{
		graphic.GetComponent<SpriteRenderer> ().sprite = newSprite;
		graphic.transform.localScale = newSpriteScale;
		boss = isBoss;
	}
}
