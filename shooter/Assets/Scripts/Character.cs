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

	private int pulse = 1;
	private bool fading;
	private float comboTimerMax = 3;
	private float comboTimer = 0;
	public int comboCount = 0;
	public Text comboCountUI;
	public Text comboText;
	public RawImage comboBar;

	private float magicRingMaxSize = 6;
	private Vector3 magicRingInitialSize;
	public GameObject ringSprite;

	public Image healthBar;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent <ShotStraight>();
		gameObject.AddComponent <ShotWide>();
		equipedShotTypes.AddRange(GetComponents<ShotType>());
		magicRingInitialSize = ringSprite.transform.localScale;
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
	}

	void Move() {

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
		ringSprite.transform.position = newPos;
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
			MapManager.Manager.AddLove(-1);
			comboCount = 0;
			SolveHealthBar();
		}
	}	

	void SolveHealthBar(){
		Vector3 newBarSize = healthBar.transform.localScale;
		newBarSize.x = Mathf.Clamp01 ((equipedShotTypes [equipedShot].health * 100f / equipedShotTypes [equipedShot].healthMax) / 100f);
		healthBar.transform.localScale = newBarSize;
		healthBar.rectTransform.anchoredPosition = Vector2.right * (healthBar.rectTransform.sizeDelta.x / 2 * newBarSize.x);
	}

	void SolveCombo(){
		if (comboTimer > 0) {
			comboTimer -= Time.deltaTime;
			Vector3 newBarSize = comboBar.transform.localScale;
			newBarSize.x = Mathf.Clamp01 ((comboTimer * 100 / comboTimerMax) / 100f);
			comboBar.transform.localScale = newBarSize;
			comboBar.rectTransform.anchoredPosition = Vector2.right * (comboBar.rectTransform.sizeDelta.x / 2 * newBarSize.x);
			//set magic ring size
			ringSprite.transform.localScale = magicRingInitialSize * Mathf.Clamp ((comboTimer * 100 / comboTimerMax) / 100f * magicRingMaxSize, 1f, magicRingMaxSize);

			if (comboCountUI.transform.localScale.y <= 1.5f) {
				Vector3 newScale = comboCountUI.transform.localScale + (Vector3.up * Time.deltaTime * pulse);
				newScale.x = newScale.z = 1;
				comboText.transform.localScale = comboCountUI.transform.localScale = newScale;
				if (comboText.transform.localScale.y <= 1 || comboText.transform.localScale.y >= 1.5f)
					pulse = -pulse;
			} else
				comboCountUI.transform.localScale -= Vector3.one * 2 * Time.deltaTime;

		} else if (!fading) {
			fading = true;
			comboCountUI.color = comboText.color = Color.red;
		} else {
			comboCountUI.color = comboText.color -= new Color (0, 0, 0, 1 * Time.deltaTime);
			comboCount = 0;
		}
	}

	public void ComboAdd(int value = 0){
		comboCountUI.color = comboText.color = new Color(1, 0.5f, 0, 1);
		if (value > 0) {
			comboCount += value;
			comboCountUI.text = comboCount.ToString ();
			comboText.text = "combo!";
			if (comboCount > 2000 && MapManager.Manager.difficulty < MapManager.Difficulty.death)
				comboCount = 2000;
			comboTimer = comboTimerMax;
		} else if ((comboTimer += 0.05f) > comboTimerMax) {
			comboTimer = comboTimerMax;
			comboCountUI.transform.localScale = Vector3.one * 1.75f;
		}
	}
}
