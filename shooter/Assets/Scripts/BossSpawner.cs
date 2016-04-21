using UnityEngine;
using System.Collections;

public class BossSpawner : MonoBehaviour {

	public float startTime = 0;
	public bool subboss;
	public int health = 6500;
	public BossSkull.Pattern[] patterns = {(BossSkull.Pattern)0, (BossSkull.Pattern)1, (BossSkull.Pattern)2};
	protected Boss theBoss;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if ((startTime -= Time.deltaTime) <= 0) {
			theBoss = ((GameObject)Instantiate(Resources.Load("Boss"), new Vector3(12,0), Quaternion.identity)).GetComponent<Boss>();
			theBoss.patterns = patterns;
			theBoss.health = health;
			theBoss.bossSprite.sprite = GetComponent<SpriteRenderer>().sprite;
			theBoss.bossSprite.gameObject.transform.localScale = transform.localScale;
			MapManager.Manager.onScreenEntities.Add(theBoss);
			Destroy(gameObject);
		}
	}
}
