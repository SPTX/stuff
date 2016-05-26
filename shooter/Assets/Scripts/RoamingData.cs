using UnityEngine;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;

public class RoamingData : MonoBehaviour {

	public static RoamingData self;
	public static SaveData data;
	public List<ShotType> tsukaiMaType = new List<ShotType>();
	protected bool created;

	// Use this for initialization
	void Start () {
	}
	
	void Awake ()
	{
		if (!created)
		{
			DontDestroyOnLoad(gameObject);
			AutoLoad();
			self = this;
			created = true;
		}
		else
			Destroy(gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}

	public static void AutoSave()
	{
		if (!Directory.Exists("Save"))
			Directory.CreateDirectory("Save");

		BinaryFormatter formater = new BinaryFormatter();
		FileStream fs = File.Create("Save/PlayerData");
		formater.Serialize (fs, data);
		fs.Close ();
	}
	
	void AutoLoad()
	{
		if (!File.Exists ("Saves/PlayerData")) {
			gameObject.AddComponent<SaveData>();
			data = GetComponent<SaveData>();
			gameObject.AddComponent <ShotStraight>();
			gameObject.AddComponent <ShotWide>();
			tsukaiMaType.AddRange(GetComponents<ShotType>());
			tsukaiMaType[1].element = Elements.wind;
			return;
		}

		BinaryFormatter formatter = new BinaryFormatter();
		FileStream fs = File.Open("Saves/PlayerData", FileMode.Open);
		data = (SaveData)formatter.Deserialize(fs);
		fs.Close();

		gameObject.AddComponent (System.Type.GetType(data.tsukaiMaType[0]));
		gameObject.AddComponent (System.Type.GetType(data.tsukaiMaType[1]));
		tsukaiMaType.AddRange(GetComponents<ShotType>());
		tsukaiMaType [0].element = (Elements)data.tsukaiMaElement [0];
		tsukaiMaType [1].element = (Elements)data.tsukaiMaElement [1];
		tsukaiMaType [2].element = (Elements)data.tsukaiMaElement [2];
		tsukaiMaType [3].element = (Elements)data.tsukaiMaElement [3];
		tsukaiMaType [4].element = (Elements)data.tsukaiMaElement [4];
	}

	public void SaveHighScore(int map, int score)
	{
		data.highscore [map] = score;
	}
	
	public int LoadHighScore(int map)
	{
		return data.highscore [map];
	}

}
