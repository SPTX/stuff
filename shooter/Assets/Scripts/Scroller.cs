using UnityEngine;
using System.Collections;

public class Scroller : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Renderer> ().material.mainTextureOffset += -Vector2.left * 0.1f * Time.deltaTime;﻿
	}
}
