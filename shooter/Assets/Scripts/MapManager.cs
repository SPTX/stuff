﻿using UnityEngine;
using System.Collections;

public class MapManager : MonoBehaviour {

	public static Character PlayerCharacter;

	// Use this for initialization
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer> ().material.mainTextureOffset += -Vector2.left * 0.1f * Time.deltaTime;﻿
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Cursor.lockState = (Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked);
				Cursor.visible = !Cursor.visible;
			Time.timeScale = (Time.timeScale == 1f ? 0 : 1f);
		}
	}
}
