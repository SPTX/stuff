using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapManager : MonoBehaviour {

	public static Character PlayerCharacter;
	public static MapManager Manager;

	private int bestScore;
	private int score = 0;
	public int material = 0;
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

	public string Difficulty = "easy";

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Manager = this;

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
			if (loveDrain) {
				if ((love -= 1 * Time.deltaTime) <= 0) {
					loveDrain = false;
					love = 0;
				}
			}
		}
	}

	public void AddScore(int value)
	{
		score += value;
		scoreUI.text = score.ToString ("n0");
	}

	public void AddMaterial(int value){
		AddScore(100 * value);
		material += value;
		materialUI.transform.localScale = Vector3.one * 2;
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
}
