﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MapManager : MonoBehaviour {

	public static Character PlayerCharacter;
	public static MapManager Manager;

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

	public enum Difficulty{easy, normal, hard, death};
	public Difficulty difficulty = Difficulty.easy;
	public List<DamagingEntity> onScreenEntities;

	public bool bossTime;
	public float bossTimer = 120;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Manager = this;

		//debug
		Time.timeScale = 0.1f;
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
				PlayerCharacter.power -= 1;
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
	}

	public void AddScore(float value, float element, bool ring, int type)
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
				scoreToAdd = maRyoku * PlayerCharacter.comboCount * 0.1f;

		score += Mathf.RoundToInt(scoreToAdd);
		scoreUI.text = score.ToString ("n0");
	}

	public void AddMaterial(int value){
		AddScore(value, 1, false, 3);
		material += value;
		materialUI.transform.localScale = Vector3.one * 2;
	}

	public void SpawnMaterial(int materialNum, int materialSize, Vector3 spawnPosition)
	{
		while (materialNum-- > 0)
		{
			Vector3 randvector = Vector3.zero;
			randvector.x = Random.Range(-0.5f, 0.5f);
			randvector.y = Random.Range(-0.5f, 0.5f);
			Pickup mat = ((GameObject)Instantiate (Resources.Load ("Material"), spawnPosition + randvector, Quaternion.identity)).GetComponent<Pickup>();
			mat.value = Mathf.Clamp(materialSize, 1, 1000 - material);
			mat.transform.localScale *= Mathf.Clamp(materialSize / 2, 0.75f, 2.5f);
			materialSpawned += mat.value;
		}
		//spawn stars if max materials reached
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
			PlayerCharacter.power += 1;
//			loveMellow.enabled = true;
		}
	}

	public float SolveElement(string attacker, string target)
	{
		if (attacker == "Wind" && target == "Water" ||
		    attacker == "Water" && target == "fire" || 
		    attacker == "Fire" && target == "Wind" ||
		    attacker == "Light" && target == "Dark" || 
		    attacker == "Dark" && target == "Light")
			return 2; // *2
		else if (attacker == "Water" && target == "Wind" ||
		         attacker == "Fire" && target == "Water" || 
		         attacker == "Wind" && target == "Fire" ||
		         attacker == "Dark" && target == "Light" || 
		         attacker == "Light" && target == "Dark")
			return 0.5f;// /2
		return 1;
	}

	public bool WithinBounds(Vector3 objectPos, Vector2 bounds)
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
	
	public bool WithinBounds(Vector3 objectPos, float x, float y)
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
	
	void ClearEntities(){
		for (int i = 0; i < onScreenEntities.Count; ++i) {
			if (onScreenEntities[i] == null)
				onScreenEntities.RemoveAt(i);
		}
	}
}
