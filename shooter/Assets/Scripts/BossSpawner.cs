using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour {

	public float startTime = 0;
	protected float timeUntilSpawn = 4;
	public bool final;
	public int health = 6500;
	public Elements element;
	public BossSkull.Pattern[] patterns = {(BossSkull.Pattern)0, (BossSkull.Pattern)1, (BossSkull.Pattern)2};
	public float bossTimer = 240;
	protected Boss theBoss;
	protected bool warned;
	protected GameObject warning;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((startTime -= Time.deltaTime) <= 0) {
			//violin sound;
			if (!warned){
				warning = (GameObject)Instantiate(Resources.Load("Warning"), Vector3.zero, Quaternion.identity);
				warned = true;
			}


			MapManager.PlayerCharacter.comboLock = true;
			MapManager.Manager.bossTime = bossTimer;
			//if ((timeUntilSpawn -= Time.deltaTime) <= 0) {
				if (warning == null) {
				theBoss = ((GameObject)Instantiate(Resources.Load("Boss" + (final ? "": "Sub")), new Vector3(12,0), Quaternion.identity)).GetComponent<Boss>();
				theBoss.patterns = patterns;
				theBoss.health = health;
				theBoss.element = element;
				theBoss.bossSprite.sprite = GetComponent<SpriteRenderer>().sprite;
				theBoss.bossSprite.gameObject.transform.localScale = transform.localScale;
				theBoss.final = final;
				MapManager.Manager.onScreenEntities.Add(theBoss);
				//start boss music
				Destroy(gameObject);
			}
		}
	}
}
