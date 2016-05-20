using UnityEngine;
using System.Collections;

public class ExplodingCarcass : MonoBehaviour {

	public GameObject graphic;
	public GameObject graphicSilhouette;
	protected Vector3 silhouetteDefaultScale;
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

	public GameObject StarDirection;
	protected float delayBetweenStars = 0.01f;
	protected float untilNextStar;
	protected int remainingStars = 21;
	protected float starDistance = 1;

	// Use this for initialization
	void Start () {
		Instantiate (Resources.Load ("ExplosionGlow"), transform.position, Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {

		if ((carcassDuration -= Time.deltaTime) < 0) {
			if ((untilNextStar -= Time.deltaTime) < 0)
			{
				Instantiate(Resources.Load("Star" + (boss ? "Big" : "Small" )),
				            (boss ? Vector3.zero : transform.position) + StarDirection.transform.up * starDistance * Random.Range(0.6f, 1.4f),
				            Quaternion.identity);
				Instantiate(Resources.Load("Star" + (boss ? "Big" : "Small" )),
				            (boss ? Vector3.zero : transform.position) - StarDirection.transform.up * starDistance  * Random.Range(0.6f, 1.4f) * 2,
				            Quaternion.identity);
				StarDirection.transform.rotation *= Quaternion.AngleAxis(17.14f, Vector3.forward);
				untilNextStar = delayBetweenStars;
				if (--remainingStars <= 0)
					Destroy (gameObject);
//				else if (remainingStars == 21)
//					starDistance *= 2;
			}
			return;
		}

		if ((colorChangeDelay -= Time.deltaTime) <= 0) {
			graphic.GetComponent<SpriteRenderer> ().color = altColor [altColorSelect ^= 1];
			colorChangeDelay = delayToChangeColor;
		}
		transform.Translate (xSpeed * Time.deltaTime, ySpeed * Time.deltaTime, 0, Space.World);
		transform.rotation *= Quaternion.AngleAxis (rotationSpeed * Time.deltaTime, Vector3.back);
		if ((nextShake -= Time.deltaTime) <= 0) {
			graphic.transform.localPosition = new Vector3 (Random.Range (-shakingAmplitude, shakingAmplitude), Random.Range (-shakingAmplitude, shakingAmplitude), 0);
			nextShake = shakingFrequency;
		}

		graphicSilhouette.transform.localScale += Vector3.one * 12 * Time.deltaTime;
		if (graphicSilhouette.transform.localScale.x > silhouetteDefaultScale.x * 1.25f)
			graphicSilhouette.transform.localScale = silhouetteDefaultScale;
	}

	public void SetUp(Sprite newSprite, Vector3 newSpriteScale, bool isBoss = false)
	{
		graphic.GetComponent<SpriteRenderer> ().sprite = newSprite;
		graphic.transform.localScale = newSpriteScale;
		graphicSilhouette.transform.localScale = newSpriteScale;
		graphicSilhouette.GetComponent<SpriteRenderer> ().sprite = newSprite;
		silhouetteDefaultScale = newSpriteScale;
		if (boss = isBoss)
			starDistance = 2;
	}
}
