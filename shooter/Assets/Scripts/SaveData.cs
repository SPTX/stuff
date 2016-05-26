using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData : MonoBehaviour {

	public List<int> highscore = new List<int> ();
	public List<string> tsukaiMaType = new List<string>();
	public List<int> tsukaiMaElement = new List<int>();
	public List<string> tsukaiMaCharacter = new List<string>();

	// Use this for initialization
	void Start () {
		tsukaiMaElement.Add (0);
		tsukaiMaElement.Add (1);
		tsukaiMaElement.Add (2);
		tsukaiMaElement.Add (3);
		tsukaiMaElement.Add (4);

		tsukaiMaType.Add ("ShotStraight");
		tsukaiMaType.Add ("ShotWide");
		tsukaiMaType.Add ("Bomb");
		tsukaiMaType.Add ("Bomb");
		tsukaiMaType.Add ("Bomb");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

}