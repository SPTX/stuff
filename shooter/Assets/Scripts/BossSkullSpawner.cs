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
	public enum SubBossPattern {none, snake, rotator};
	BossSkull.Pattern pattern;
	private float speed = 5;
	private float refire = 0;
	private float frequency = 5;
	private float respawnSkulls = 5;
	private float respawnSkullsDelay = 5;
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

	public void SetUp(BossSkull.Pattern newPattern, SubBossPattern sub = SubBossPattern.none, byte numberOfWaves = 5)
	{
		pattern = newPattern;
		if (numberOfWaves > 0)
			duration = frequency * numberOfWaves;
		if (pattern == BossSkull.Pattern.straight && sub == SubBossPattern.none) {
			transform.position += Vector3.up * 3.5f;
			newPosition = transform.position;
			frequency = 0.025f;
			duration = (frequency * 4 + 1) * numberOfWaves;
		}
		setUp = true;
	}

	void StraightSpawn ()
	{
		BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), newPosition, transform.rotation)).GetComponent<BossSkull> ();
		newSkull.SetUp((Elements)Random.Range(0, 5), BossSkull.Pattern.seeking);
		MapManager.Manager.bossSkulls.Add(newSkull);
		//spawn above
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
		
	}

	void Round()
	{
		
	}
	
	void Seeking()
	{
		BossSkull newSkull = ((GameObject)Instantiate(Resources.Load("BossSkull"), transform.position, transform.rotation * Quaternion.AngleAxis(Random.Range(-60f, 60f), Vector3.forward))).GetComponent<BossSkull> ();
		newSkull.SetUp((Elements)Random.Range(0, 5), BossSkull.Pattern.seeking);
		MapManager.Manager.bossSkulls.Add(newSkull);
	}
}
