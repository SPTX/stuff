using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MapManager : MonoBehaviour {

	public static Character PlayerCharacter;
	public static MapManager Manager;

	private int bestScore;
	private int score = 0;
	private int material = 0;
	public Text bestScoreUI;
	public Text scoreUI;
	public Text materialUI;

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
		GetComponent<Renderer> ().material.mainTextureOffset += -Vector2.left * 0.1f * Time.deltaTime;﻿

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked);
			Cursor.visible = !Cursor.visible;
			Time.timeScale = (Time.timeScale == 1f ? 0 : 1f);
		}
	}

	public void AddScore(int value)
	{
		score += value;
		scoreUI.text = score.ToString ();
	}

	public void AddMaterial(int value){
		AddScore(100 * value);
		material += value;
		materialUI.text = material.ToString ();
	}
}
