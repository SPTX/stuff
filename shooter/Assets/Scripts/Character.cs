using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Character : DamagingEntity {

	private float invincibility = 0;
	private float invincibilityTime = 0.5f;

	private float refire = 0;
	public List<GameObject> activeShots;
	public List<ShotType> equipedShotTypes;
	public int equipedShot = 0;
	public int power = 0;

	private float magicRingMaxSize = 2;
	private float magicRingMinSize = 0.4f;
	private float magicRingSize;

	public float comboTimerMax = 5;
	public float comboTimer = 0;
	public RawImage comboBar;

	private float suckRingSize = 30;


	public Image healthBar;

	// Use this for initialization
	void Start () {
		magicRingSize = magicRingMinSize;
		gameObject.AddComponent <ShotStraight>();
		gameObject.AddComponent <ShotWide>();
		equipedShotTypes.AddRange(GetComponents<ShotType>());
		MapManager.PlayerCharacter = this;
	}
	
	// Update is called once per frame
	void Update () {

		if (Time.timeScale == 0)
			return;
		Move ();
		ClearShots ();
		SolveCombo();
		if (invincibility > 0)
			invincibility -= Time.deltaTime;
		if (Input.GetMouseButton (0))
			Fire ();
		if (Input.GetMouseButtonUp (1)) {
			equipedShot ^= 1;
			SolveHealthBar();
		}

		////debug
		if (Input.GetKeyDown("o"))
			++power;
		if (Input.GetKeyDown("l"))
			--power;
		if (Input.GetKeyDown (KeyCode.D)) {
			Debug.Log(comboBar.rectTransform.localPosition + " loc pos\n" + 
			          comboBar.rectTransform.rect + " rect\n" +
			          comboBar.rectTransform.sizeDelta + " delta\n"
			          );
		}
	}

	void Move(){

		transform.Translate (Input.GetAxis ("Mouse X"), Input.GetAxis ("Mouse Y"), 0);
		Vector2 newPos = transform.position;
		
		if (transform.position.x < -8)
			newPos.x = -8;
		else if (transform.position.x > 8)
			newPos.x = 8;
		if (transform.position.y < -3.5f)
			newPos.y = -3.5f;
		else if (transform.position.y > 3.5f)
			newPos.y = 3.5f;
		transform.position = newPos;
	}

	void ClearShots(){
		for (int i = 0; i < activeShots.Count; ++i) {
			if (activeShots[i] == null)
				activeShots.RemoveAt(i);
		}
	}

	void Fire(){
		if (refire <= 0 && activeShots.Count < equipedShotTypes[equipedShot].maxBullets) {

			activeShots.Add(equipedShotTypes[equipedShot].Fire (transform.position + Vector3.right * 0.4f, power));
			refire = equipedShotTypes[equipedShot].firerate;
		} else
			refire = refire - Time.deltaTime;
	}

	void OnTriggerStay2D(Collider2D other)
	{
		if (invincibility <= 0) {
			equipedShotTypes[equipedShot].health -= other.GetComponent<DamagingEntity>().damage;
			invincibility = invincibilityTime;
			SolveHealthBar();
		}
	}	

	void SolveCombo(){
		if (comboTimer > 0) {
			comboTimer -= Time.deltaTime;
			Vector3 newBarSize = comboBar.transform.localScale;
			newBarSize.x = Mathf.Clamp01((comboTimer * 100 / comboTimerMax) / 100f);
			comboBar.transform.localScale = newBarSize;
			comboBar.rectTransform.anchoredPosition = Vector2.right * (comboBar.rectTransform.sizeDelta.x / 2 * newBarSize.x);
		}
	}

	void SolveHealthBar(){
		Vector3 newBarSize = healthBar.transform.localScale;
		newBarSize.x = Mathf.Clamp01 ((equipedShotTypes [equipedShot].health * 100f / equipedShotTypes [equipedShot].healthMax) / 100f);
		healthBar.transform.localScale = newBarSize;
		healthBar.rectTransform.anchoredPosition = Vector2.right * (healthBar.rectTransform.sizeDelta.x / 2 * newBarSize.x);
	}

}
