using UnityEngine;
using System.Collections;

public class BossSkullSpawner : MonoBehaviour {

	/* list of patterns:
	 * straight
	 * falling sides
	 * straight sides
	 * round
	 * seeking

	 * subboss patterns:
	 * straight snake line
	 * rotator
	*/
	BossSkull.Pattern pattern;
	private float speed = 5;
	private float refire = 0;
	private float frequency = 5;
	private float NextPatternDelay = 5;
	private float duration = 12;
	private float spacing = 2;
	private Vector3 newPosition;
	private bool setUp;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((duration -= Time.deltaTime) <= 0)
			Destroy (gameObject);

		if (!setUp)
			return;

		if ((refire -= Time.deltaTime) <= 0) {
			refire = frequency;

			if (pattern == BossSkull.Pattern.straight)
				StraightSpawn();
			else if (pattern == BossSkull.Pattern.fallingFromSides)
				FallingSides();
			else if (pattern == BossSkull.Pattern.side)
				Side ();
			else if (pattern == BossSkull.Pattern.round)
				Round();
			else if (pattern == BossSkull.Pattern.seeking)
				Seeking();
		}
	}

	public float SetUp(BossSkull.Pattern newPattern)
	{
		pattern = newPattern;
		if (pattern == BossSkull.Pattern.straight) {
			newPosition = transform.position + Vector3.up * 3.5f;
			frequency = 0.025f;
			duration = (frequency * 4 + 1) * 5;
		}
		else if (pattern == BossSkull.Pattern.side){
			newPosition = Vector3.right * 6.2f + Vector3.up * 3;
			frequency = 2;
			duration = frequency * 2 * 3 + (NextPatternDelay = 2);
		}
		else if (pattern == BossSkull.Pattern.round) {
			frequency = 7f;
			duration = frequency * 4 + (NextPatternDelay = 2);
		}
		else if (pattern == BossSkull.Pattern.seeking) {
			frequency = 0.5f;
			duration = frequency * 8 + NextPatternDelay;
		}
		setUp = true;
		return duration;
	}

	void StraightSpawn ()
	{
		BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), newPosition, transform.rotation)).GetComponent<BossSkull> ();
		newSkull.SetUp((Elements)Random.Range(0, 5), BossSkull.Pattern.straight);
		MapManager.Manager.bossSkulls.Add(newSkull);
		newPosition -= Vector3.up * spacing;
		if (newPosition.y > 2.5f || newPosition.y < -2.5f) {
			spacing = -spacing;
			newPosition -= Vector3.up * (spacing / 2);
			refire = 1;
		}
	}

	void FallingSides()
	{

	}

	void Side()
	{
		if (duration < NextPatternDelay)
			return;

		Quaternion nextRot = Quaternion.AngleAxis (90, Vector3.forward);

		for (int n = 0; n < 2; ++n) {
			for (int i = 0; i < 8; ++i) {
				BossSkull newSkull = ((GameObject)Instantiate (Resources.Load ("BossSkull"), newPosition, transform.rotation * nextRot)).GetComponent<BossSkull> ();
				newSkull.SetUp ((Elements)Random.Range (0, 5), BossSkull.Pattern.side, 1.5f);
				MapManager.Manager.bossSkulls.Add (newSkull);
				newPosition -= Vector3.right * spacing;
			}
			newPosition += Vector3.right * spacing;
			spacing = -spacing;
			nextRot *= Quaternion.AngleAxis (180, Vector3.forward);
			newPosition.y = -newPosition.y;
		}
		if (newPosition.x == 7.2f)
			newPosition = Vector3.right * 6.2f + Vector3.up * 3;
		else
			newPosition = Vector3.right * (6.2f + spacing / 2) + Vector3.up * 3;
		refire = 3;
	}

	void Round()
	{
		if (duration < NextPatternDelay)
			return;

		Quaternion nextRot = transform.rotation;
		for (int i = 0; i < 8;++i) {
			BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position, nextRot)).GetComponent<BossSkull> ();
			newSkull.SetUp((Elements)Random.Range(0, 5), BossSkull.Pattern.round);
			MapManager.Manager.bossSkulls.Add(newSkull);

			newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position + newSkull.transform.up * 23.8f, nextRot * Quaternion.AngleAxis(180, Vector3.forward))).GetComponent<BossSkull> ();
			newSkull.SetUp((Elements)Random.Range(0, 5), BossSkull.Pattern.round);
			MapManager.Manager.bossSkulls.Add(newSkull);
			nextRot *= Quaternion.AngleAxis(45, Vector3.forward);
		}
	}
	
	void Seeking()
	{
		if (duration < NextPatternDelay)
			return;
		BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position, transform.rotation * Quaternion.AngleAxis(Random.Range(-80f, 80f), Vector3.forward))).GetComponent<BossSkull> ();
		newSkull.SetUp((Elements)Random.Range(0, 5), BossSkull.Pattern.seeking, 3);
		MapManager.Manager.bossSkulls.Add(newSkull);
	}
}
