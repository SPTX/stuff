using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public enum Elements {fire, wind, water, light, dark};

public class MapManager : MonoBehaviour {

	public static Character PlayerCharacter;
	public static MapManager Manager;
	public static Color[] elementColors = {
		new Color(1,0.3f,0.24f),
		new Color(0.1f,0.84f,0.008f),
		new Color(0.02f,0.5f,0.82f),
		new Color(1,1,1),
		new Color(0.65f,0.25f,1)};
	

	
	private int bestScore;
	private int score = 0;
	private int material = 0;
	public int materialSpawned = 0;
	private int DisplayedMaterial = 0;
	public Text bestScoreUI;
	public Text scoreUI;
	public Text materialUI;

	public Image loveHeart;
	public RawImage loveMellow;
	private float love = 0;
	private float loveMax = 100;
	private bool loveDrain;
	private int pulse = 1;
	public int InLove() {return System.Convert.ToInt32 (loveDrain);}

	public enum Difficulty{easy, normal, hard, death};
	public Difficulty difficulty = Difficulty.easy;
	public List<DamagingEntity> onScreenEntities;
	public List<BossSkull> bossSkulls;

	public float bossTime = 0;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Manager = this;
		Random.seed = (int)System.DateTime.Now.Ticks;

		//debug
//		Time.timeScale = 0.1f;
	}
	
	// Update is called once per frame
	void Update () {
		//texture scrolling
		GetComponent<Renderer> ().material.mainTextureOffset += -Vector2.left * (loveDrain ? 0.25f : 0.1f) * Time.deltaTime;﻿
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked);
			Cursor.visible = !Cursor.visible;
			Time.timeScale = (Time.timeScale == 1f ? 0 : 1f);
		}

		ClearEntities ();

		if (DisplayedMaterial < material) {
			DisplayedMaterial += 1;
			materialUI.text = DisplayedMaterial.ToString ();
		}
		if (materialUI.transform.localScale.x > 1)
			materialUI.transform.localScale -= Vector3.one * 0.1f;

		if (loveHeart.transform.localScale.y <= 1.5f) {
			loveHeart.transform.localScale += (Vector3.one * Time.deltaTime * pulse);
			if (loveHeart.transform.localScale.y <= 1 || loveHeart.transform.localScale.y >= 1.5f)
				pulse = -pulse;
		} else
			loveHeart.transform.localScale += (Vector3.one * Time.deltaTime * pulse);
		loveHeart.rectTransform.anchoredPosition = Vector3.up * (240 * (((love * 100) / loveMax) / 100f));

		if (loveDrain) {
			Rect newRect = loveMellow.uvRect;
			newRect.x += 0.1f * Time.deltaTime;
			if ((love -= 10 * Time.deltaTime) <= 0) {
				love = 0;
				loveDrain = false;
				newRect.x = 0;
//				loveMellow.enabled = false;
			}
			loveMellow.uvRect = newRect;
			if (loveMellow.color.a < 0.2f)
				loveMellow.color = new Color (1, 1, 1, loveMellow.color.a + 0.01f);
		} else {
			if (loveMellow.color.a > 0)
				loveMellow.color = new Color (1, 1, 1, loveMellow.color.a - 0.01f);
			if (loveDrain && (love -= 1 * Time.deltaTime) <= 0) {
					loveDrain = false;
					love = 0;
			}
		}

		if (bossTime > 0)
			bossTime -= Time.deltaTime;
	}

	public int AddScore(float value, float element, bool ring, int type)
	{
		/*type
		 * 0 = normal enemy,
		 * 1 = big enemy (sub boss type),
		 * 2 = boss (inluding as sub boss),
		 * 3 = item */
		float scoreToAdd = 0;
		int maRyoku = Mathf.Clamp (material, 500, 1000);

		if (type < 2) {
			scoreToAdd = value * maRyoku * (0.75f * element) * (ring ? 2 : 1) * (loveDrain ? 1.1f : 1) /* * awakenedSkill ? 1.5f : 1  */ * (type == 1 ? (1 + 0.01f * PlayerCharacter.comboCount) : 1);
		} else if (type == 3)
			scoreToAdd = value * maRyoku * (loveDrain ? 1.2f : 1) * (0.1f + 0.001f * PlayerCharacter.comboCount);
		else //boss or type error
			scoreToAdd = maRyoku * Mathf.Clamp((int)PlayerCharacter.comboCount, 1, PlayerCharacter.comboCount) * 0.1f;

		score += (maRyoku = Mathf.RoundToInt(scoreToAdd));//lol recycling
		scoreUI.text = score.ToString ("n0");
		return maRyoku;//lol recycling
	}

	public void AddMaterial(int value){
		AddScore(value, 1, false, 3);
		material += value;
		materialUI.transform.localScale = Vector3.one * 2;
		if (material > 499)
			PlayerCharacter.power = 2;
		else if (material > 249)
			PlayerCharacter.power = 1;
	}

	public void SpawnMaterial(int materialNum, int materialSize, Vector3 spawnPosition)
	{
		if (materialSpawned < 1000) {
			while (materialNum-- > 0) {
				Vector3 randvector = Vector3.zero;
				randvector.x = Random.Range (-0.5f, 0.5f);
				randvector.y = Random.Range (-0.5f, 0.5f);
				Pickup mat = ((GameObject)Instantiate (Resources.Load ("Material"), spawnPosition + randvector, Quaternion.identity)).GetComponent<Pickup> ();
				mat.value = Mathf.Clamp (materialSize, 1, 1000 - material);
				mat.transform.localScale *= Mathf.Clamp (materialSize / 2, 0.75f, 2.5f);
				materialSpawned += mat.value;
			}
		}
		else
			Instantiate (Resources.Load ("StarBig"), spawnPosition, Quaternion.identity);
	}

	public void AddLove(float value)
	{
		//debug
//		value = 100;

		if (value < 0)
			love = 0;
		else if (!loveDrain && (love += value) >= loveMax) {
			love = loveMax;
			loveDrain = true;
			Instantiate(Resources.Load("LoveMax"), Vector3.zero, Quaternion.identity);
//			loveMellow.enabled = true;
		}
	}

	static public float SolveElement(Elements attacker, Elements target)
	{
		if (attacker == Elements.wind && target == Elements.water ||
		    attacker == Elements.water && target == Elements.fire || 
		    attacker == Elements.fire && target == Elements.wind ||
		    attacker == Elements.light && target == Elements.dark || 
		    attacker == Elements.dark && target == Elements.light)
			return 2; // *2
		else if (attacker == Elements.water && target == Elements.wind ||
		         attacker == Elements.fire && target == Elements.water || 
		         attacker == Elements.wind && target == Elements.fire)
			return 0.5f;// /2
		return 1;
	}
	
	static public bool WithinBounds(Vector3 objectPos, Vector2 bounds)
	{
		if (objectPos.x < -bounds.x)
			return false;
		else if (objectPos.x > bounds.x)
			return false;
		if (objectPos.y < -bounds.y)
			return false;
		else if (objectPos.y > bounds.y)
			return false;
		return true;
	}
	
	static public bool WithinBounds(Vector3 objectPos, float x, float y)
	{
		if (objectPos.x < -x)
			return false;
		else if (objectPos.x > x)
			return false;
		if (objectPos.y < -y)
			return false;
		else if (objectPos.y > y)
			return false;
		return true;
	}

	static public Elements IsSensibleTo (Elements target){
		if (target == Elements.fire)
			return Elements.water;
		if (target == Elements.water)
			return Elements.wind;
		if (target == Elements.wind)
			return Elements.fire;
		if (target == Elements.light)
			return Elements.dark;
		return Elements.light;
	}
	
	void ClearEntities(){
		for (int i = 0; i < onScreenEntities.Count; ++i) {
			if (onScreenEntities[i] == null)
				onScreenEntities.RemoveAt(i);
		}

		for (int i = 0; i < bossSkulls.Count; ++i) {
			if (bossSkulls[i] == null)
				bossSkulls.RemoveAt(i);
		}
	}

	public void DamageEntities(int damage, Elements DamElement = Elements.fire, bool ring = false){
		for (int i = 0; i < onScreenEntities.Count;++i)
		{
			if (onScreenEntities[i])
				onScreenEntities[i].TakeDamage(damage, Elements.fire);
		}
	}
	
	public void ExpodeNearbySkulls (Vector3 explodingSkull){
		for (int i = 0; i < bossSkulls.Count; ++i) {
			if (bossSkulls[i] != null && Vector3.Distance(explodingSkull, bossSkulls[i].transform.position) < 3)
				bossSkulls[i].TakeDamage(-2, Elements.fire);
		}
	}
		
	public void KillSkulls()
	{
		for (int i = 0; i < bossSkulls.Count; ++i) {
			if (bossSkulls[i] != null)
				bossSkulls[i].TakeDamage(-1, Elements.fire);
		}
	}

}
