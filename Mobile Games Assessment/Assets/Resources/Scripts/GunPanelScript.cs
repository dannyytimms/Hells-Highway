using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPanelScript : MonoBehaviour {

	public GameObject Rifle, Pistol, AR;
	public GameObject activePanel;
	GameObject currentWeapon;
	GameManager gameManager = new GameManager();

	void Awake () 
	{
		//gameManager = GetComponent<GameManager> ();
		currentWeapon = Pistol;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void setPistol()
	{
		if (currentWeapon != Pistol) {
			Pistol.gameObject.SetActive (true);
			currentWeapon.SetActive (false);
			currentWeapon = Pistol;
		}
	}
	public void setRifle()
	{
		if (currentWeapon != Rifle) {
			Rifle.SetActive (true);
			currentWeapon.SetActive (false);
			currentWeapon = Rifle;
		}
	}
	public void setAR()
	{
		if (currentWeapon != AR) {
			AR.SetActive (true);
			currentWeapon.SetActive (false);
			currentWeapon = AR;
		}
	}
	public void hideAll()
	{
		currentWeapon.SetActive (false);
	}

	public void isPistol()
	{
		if(activePanel.activeSelf == true)
		GameManager.gunL = 0;
		else
			GameManager.gunR = 0;
		gameManager.SaveGame ();
	}
	public void isRifle()
	{
		if (activePanel.activeSelf == true)
			GameManager.gunL = 1;
		else
			GameManager.gunR = 1;

		gameManager.SaveGame ();
	}
	public void isAR()
	{
		if (activePanel.activeSelf == true)
			GameManager.gunL = 2;
		else
			GameManager.gunR = 2;

		gameManager.SaveGame ();
	}
}
