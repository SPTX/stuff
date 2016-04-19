using UnityEngine;
using System.Collections;

public class BossSkullSpawner : MonoBehaviour {

	BossSkull.Pattern pattern;
	private float speed = 5;
	private float refire = 0;
	private float frequency = 5;
	private float NextPatternDelay = 5;
	private float duration = 12;
	private float spacing = 1.5f;
	private Vector3 newPosition;
	private bool setUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!setUp)
			return;
		if ((duration -= Time.deltaTime) <= 0)
			Destroy (gameObject);
		else if (duration < NextPatternDelay)
			return;

		if ((refire -= Time.deltaTime) <= 0) {
			refire = frequency;

			if (pattern == BossSkull.Pattern.straight || pattern == BossSkull.Pattern.snake)
				StraightSpawn();
			else if (pattern == BossSkull.Pattern.fallingFromSides)
				FallingSides();
			else if (pattern == BossSkull.Pattern.side)
				Side ();
			else if (pattern == BossSkull.Pattern.round)
				Round();
			else if (pattern == BossSkull.Pattern.seeking)
				Seeking();
			else if (pattern == BossSkull.Pattern.rotator)
				Rotator();
		}
	}

	public float SetUp(BossSkull.Pattern newPattern)
	{
		pattern = newPattern;
		if (pattern == BossSkull.Pattern.straight) {
			newPosition = transform.position + Vector3.up * 3.5f;
			frequency = 0.025f;
			duration = (frequency * 4 + 1) * 8 + (NextPatternDelay = 1.5f);
		} else if (pattern == BossSkull.Pattern.fallingFromSides) {
			newPosition = new Vector3(-2.5f, 6, 0);
			frequency = 1f;
			speed = 2;
			spacing = 2;
			duration = frequency * 4 + (NextPatternDelay = 8);
		} else if (pattern == BossSkull.Pattern.snake) {
			newPosition = transform.position + Vector3.up * 3.5f;
			frequency = 0.15f;
			speed = 4;
			duration = (frequency * 35 + 0.5f) + (NextPatternDelay = 4);
			spacing = 0.75f;
		} else if (pattern == BossSkull.Pattern.side) {
			newPosition = Vector3.left * (6.2f + spacing);
			frequency = 1.6f;
			speed = 1f;
			spacing = 2.2f;
			duration = frequency * 6 + (NextPatternDelay = 2);
		} else if (pattern == BossSkull.Pattern.round) {
			frequency = 7f;
			duration = frequency * 4 + (NextPatternDelay = 2);
		} else if (pattern == BossSkull.Pattern.seeking) {
			frequency = 0.5f;
			speed = 3;
			duration = frequency * 8 + NextPatternDelay;
		} else if (pattern == BossSkull.Pattern.rotator) {
			frequency = 0.1f;
			speed = 8;
			duration = frequency * 44 + (NextPatternDelay = 2);
			newPosition = (Random.Range(0,2) == 0 ? Vector3.forward : Vector3.back);
		}
		setUp = true;
		return duration;
	}

	void StraightSpawn ()
	{
		BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), newPosition, transform.rotation)).GetComponent<BossSkull> ();
		newSkull.SetUp((Elements)Random.Range(0f, 5f), pattern, speed);
		MapManager.Manager.bossSkulls.Add(newSkull);
		newPosition -= Vector3.up * spacing;
		if (newPosition.y > 3.5f || newPosition.y < -3.5f) {
			spacing = -spacing;
			if (pattern == BossSkull.Pattern.straight){
				newPosition -= Vector3.up * (spacing / 2);
				refire = 1;
			}
			else {
				newPosition -= Vector3.up * spacing;
				refire = 0.5f;
			}
		}
	}

	void FallingSides()
	{
		Vector3 nextPosition = newPosition;
		int normal = Mathf.RoundToInt(nextPosition.normalized.y);

		for (int i = 0; i < 5; ++i) {
			BossSkull newSkull = ((GameObject)Instantiate (Resources.Load ("BossSkull"), nextPosition, transform.rotation * Quaternion.AngleAxis (90 * normal, Vector3.forward))).GetComponent<BossSkull> ();
			newSkull.SetUp ((Elements)Random.Range (0f, 5f), pattern, speed);
			MapManager.Manager.bossSkulls.Add (newSkull);
			nextPosition.y += spacing * normal;
		}
		newPosition.y = -newPosition.y;
		newPosition.x += spacing * 2;
	}

	void Side()
	{
		for (int i = 0; i < 10; ++i) {
			BossSkull newSkull = ((GameObject)Instantiate (Resources.Load ("BossSkull"), newPosition + Vector3.up * 3, transform.rotation * Quaternion.AngleAxis (90, Vector3.forward))).GetComponent<BossSkull> ();
			newSkull.SetUp ((Elements)Random.Range (0f, 5f), pattern, speed);
			MapManager.Manager.bossSkulls.Add (newSkull);
			
			newSkull = ((GameObject)Instantiate (Resources.Load ("BossSkull"), newPosition - Vector3.up * 3, transform.rotation * Quaternion.AngleAxis (-90, Vector3.forward))).GetComponent<BossSkull> ();
			newSkull.SetUp ((Elements)Random.Range (0f, 5f), pattern, speed);
			MapManager.Manager.bossSkulls.Add (newSkull);

			newPosition.x += spacing;
		}
		newPosition.x -= spacing / 2;
		spacing = -spacing;
		refire = 3;
	}

	void Round()
	{
		Quaternion nextRot = transform.rotation;
		for (int i = 0; i < 8;++i) {
			BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position, nextRot)).GetComponent<BossSkull> ();
			newSkull.SetUp((Elements)Random.Range(0f, 5f), pattern);
			MapManager.Manager.bossSkulls.Add(newSkull);

			newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position + newSkull.transform.up * 23.8f, nextRot * Quaternion.AngleAxis(180, Vector3.forward))).GetComponent<BossSkull> ();
			newSkull.SetUp((Elements)Random.Range(0f, 5f), pattern);
			MapManager.Manager.bossSkulls.Add(newSkull);
			nextRot *= Quaternion.AngleAxis(45, Vector3.forward);
		}
	}
	
	void Seeking()
	{
		BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position, transform.rotation * Quaternion.AngleAxis(Random.Range(-80f, 80f), Vector3.forward))).GetComponent<BossSkull> ();
		newSkull.SetUp((Elements)Random.Range(0f, 5f), pattern, speed);
		MapManager.Manager.bossSkulls.Add(newSkull);
	}

	void Rotator()
	{
		BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position, transform.rotation)).GetComponent<BossSkull> ();
		newSkull.SetUp((Elements)Random.Range(0f, 5f), pattern, speed);
		MapManager.Manager.bossSkulls.Add(newSkull);
		newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position, transform.rotation * Quaternion.AngleAxis(180, Vector3.forward))).GetComponent<BossSkull> ();
		newSkull.SetUp((Elements)Random.Range(0f, 5f), pattern, speed);
		MapManager.Manager.bossSkulls.Add(newSkull);
		transform.Rotate (newPosition, 6);
	}
}
