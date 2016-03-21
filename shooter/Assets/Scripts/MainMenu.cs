using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	public List<Image> SelectedTsukaiMa;
	public Image selector;

	public int selectorPosition = 0;

	private Vector3 shotStraightPos;
	private Vector3 shotWidePos;

	private bool selectorMoving;
	private Vector3 destination;
	
	private List<string> availableTsukaiMa = new List<string>();
	private List<string> usedTsukaiMa = new List<string>();

	// Use this for initialization
	void Start () {
		shotStraightPos = SelectedTsukaiMa[0].transform.position;
		shotWidePos = SelectedTsukaiMa[1].transform.position;
		availableTsukaiMa.Add ("Wind");
		availableTsukaiMa.Add ("Light");
		availableTsukaiMa.Add ("Dark");

		usedTsukaiMa.Add ("Fire");
		usedTsukaiMa.Add ("Water");
	}
	
	// Update is called once per frame
	void Update () {
		if (selectorMoving) {
			selector.transform.position = Vector3.MoveTowards (
				selector.transform.position,
				destination,
				100 * Time.deltaTime);
			if (selector.transform.position == destination)
				selectorMoving = false;
		}
		else {
			if (Input.GetKeyDown (KeyCode.LeftArrow) && selector.transform.position != shotStraightPos) {
				selectorMoving = true;
				destination = shotStraightPos;
				selectorPosition = 0;
			}
			else
				destination = shotWidePos;
			if (Input.GetKeyDown (KeyCode.RightArrow) && selector.transform.position != shotWidePos) {
				selectorMoving = true;
				destination = shotWidePos;
				selectorPosition = 1;
			}
			else
				destination = shotStraightPos;
		}

		if (Input.GetKeyDown (KeyCode.UpArrow)) {
		}
		else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			string nextMa = availableTsukaiMa[0];
			availableTsukaiMa.Add (ColorToMagic(SelectedTsukaiMa[selectorPosition].color));
			usedTsukaiMa.Add(availableTsukaiMa[0]);
			availableTsukaiMa.RemoveAt(0);
			SelectedTsukaiMa[selectorPosition].color = MagicToColor(usedTsukaiMa[0]);
		}

		if (Input.GetKeyDown (KeyCode.Return))
			LoadMap (1);
	}

	public void LoadMap(int i)
	{
		Application.LoadLevel (Mathf.Clamp(i, 1, i));
	}

	Color MagicToColor(string magic)
	{
		if (magic == "Fire")
			return Color.red;
		else if (magic == "Wind")
			return Color.green;
		else if (magic == "Water")
			return Color.blue;
		else if (magic == "Light")
			return Color.gray;
		else if (magic == "Dark")
			return Color.red + Color.blue;
		return Color.gray;
	}

	string ColorToMagic(Color inColor)
	{
		if (inColor == Color.red)
			return "Fire";
		else if (inColor == Color.green)
			return "Wind";
		else if (inColor == Color.blue)
			return "Water";
		else if (inColor == Color.gray)
			return "Light";
		else if (inColor == Color.red + Color.blue)
			return "Dark";
		else return "Light";
	}

}
