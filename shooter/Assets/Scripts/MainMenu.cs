using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class MainMenu : MonoBehaviour {

	public Pannel scrollView;
	public Pannel ShotSelectorStraight;
	public Pannel ShotSelectorWide;
	public Pannel ShotSelectorSupport;
	protected List<Pannel> pannelSelector = new List<Pannel>();
	protected int TargetShot;
	protected Elements selectedElement;

	// Use this for initialization
	void Start () {
		pannelSelector.Add(scrollView);
		pannelSelector.Add(ShotSelectorStraight);
		pannelSelector.Add(ShotSelectorWide);
		pannelSelector.Add(ShotSelectorSupport);
		pannelSelector.Add(ShotSelectorSupport);
		pannelSelector.Add(ShotSelectorSupport);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Return))
			LoadMap (1);
	}

	public void LoadMap(int i)
	{
		Application.LoadLevel (Mathf.Clamp(i, 1, i));
	}

	public void OpenTsukaiMaSelector(int i)
	{
		TargetShot = i;
		scrollView.opening = true;
	}

	void CloseTsukaiMaSelector()
	{
		scrollView.opening = false;
	}

	public void OpenPannel(int i)
	{
		pannelSelector[i + 1].opening = true;
	}
	
	public void ClosePannel(int i)
	{
		pannelSelector[i + 1].opening = false;
	}
	
	public void ClosePannels()
	{
		for (int i = 0; i < pannelSelector.Count;++i)
			pannelSelector[i].opening = false;
		RoamingData.AutoSave ();
	}
	
	public void ChangeTsukaiMaShot(string newType)
	{
		Destroy (RoamingData.self.tsukaiMaType [TargetShot]);
		RoamingData.self.gameObject.AddComponent(System.Type.GetType(newType));
		RoamingData.self.tsukaiMaType [TargetShot] = RoamingData.self.GetComponents <ShotType>()[RoamingData.self.GetComponents <ShotType>().Length - 1];
		RoamingData.self.tsukaiMaType [TargetShot].element = selectedElement;

//		RoamingData.data.tsukaiMaType [TargetShot] = RoamingData.self.tsukaiMaType [TargetShot].GetType ().ToString();
//		RoamingData.data.tsukaiMaElement [TargetShot] = (int)selectedElement;
		if (TargetShot == 0) {
			RoamingData.data.TsukaiMaStraight = RoamingData.self.tsukaiMaType [TargetShot].GetType ().ToString ();
			RoamingData.data.TsukaiMaStraightElement = (int)selectedElement;
		} else if (TargetShot == 1) {
			RoamingData.data.TsukaiMaWide = RoamingData.self.tsukaiMaType [TargetShot].GetType ().ToString ();
			RoamingData.data.TsukaiMaWideElement = (int)selectedElement;
		}
		//RoamingData.data.tsukaiMaElement[TargetShot] = (int)selectedElement;
		ClosePannels ();
	}
	
	public void ChangeTsukaiMaCharacter(int i)
	{
		selectedElement = (Elements)i;
		OpenPannel (TargetShot);
	}
	
}
