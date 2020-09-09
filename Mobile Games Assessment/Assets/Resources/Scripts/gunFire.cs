using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Author: Daniel Timms T016546E
public class gunFire : MonoBehaviour 
{
	static int GunL;
	static int GunR;

	public static float shotSpeed;
	public static float DPS;
	public static float accuracy;
	public static float reloadSpeed;
	public static int magazine;
	int magazineMax;
	public static int reserves;
	int reservesMax;

	public static float shotSpeedR;
	public static float DPSR;
	public static float accuracyR;
	public static float reloadSpeedR;
	int magazineMaxR;
	public static int magazineR;
	public static int reservesR;
	int reservesMaxR;

	public GameObject [] spawnPoints;
	public GameObject bullet;
	public GameObject bulletR;

	public AudioClip pistol, rifle, auto;
	public AudioClip reload, reload2, reload3;
	public AudioClip empty;
	AudioClip reloadL, reloadR;
	public AudioSource audio;
	public AudioSource audioR;

	public Button btnL, btnR;

	GameManager gameManager = new GameManager ();

	void Awake()
	{
//		Scene scene = SceneManager.GetActiveScene ();
//		if (scene.buildIndex == 2 || scene.buildIndex == 3) { 
//			if (spawnPoints [0].gameObject.activeInHierarchy == false)
//				spawnPoints [0] = GameObject.Find ("bulletSpawn");
//			if (spawnPoints [1].gameObject.activeInHierarchy == false)
//				spawnPoints [1] = GameObject.Find ("bulletSpawnR");
		//}
		gameManager.LoadGame ();

		GunL = GameManager.gunL;
		GunR = GameManager.gunR;

		switch (GunL) 
		{

		case 0:
			//pistol
			shotSpeed = 2;
			DPS = 100;
			accuracy = 100;
			reloadSpeed = 1.2f;
			magazine = 15;
			reserves = 30;
			audio.clip = pistol;
			reloadL = reload;
			break;
		case 1:
			//rifle
			shotSpeed = 5;
			DPS = 100;
			accuracy = 96;
			reloadSpeed = 2.5f;
			magazine = 10;
			reserves = 20;
			audio.clip =  rifle;
			reloadL = reload2;
			break;
		case 2:
			//AR
			shotSpeed = 4;
			DPS = 40;
			accuracy = 70;
			reloadSpeed = 2.0f;
			magazine = 30;
			reserves = 60;
			audio.clip = auto;
			reloadL = reload3;
			break;
		default: 
			shotSpeed = 2;
			DPS = 100;
			accuracy = 100;
			reloadSpeed = 1.2f;
			magazine = 15;
			reserves = 30;
			audio.clip = pistol;
			reloadL = reload;
			break;
		}

		switch (GunR) 
		{

		case 0:
			//pistol
			shotSpeedR = 2;
			DPSR = 100;
			accuracyR = 0.5f;
			reloadSpeedR = 1.2f;
			magazineR = 15;
			reservesR = 30;
			audioR.clip = pistol;
			reloadR = reload;
			break;
		case 1:
			//rifle
			shotSpeedR = 5;
			DPSR = 100;
			accuracyR= 0.5f;
			reloadSpeedR = 2.5f;
			magazineR = 10;
			reservesR = 20;
			audioR.clip = rifle;
			reloadR = reload2;
			break;
		case 2:
			//AR
			shotSpeedR = 4;
			DPSR = 40;
			accuracyR = 0.005f;
			reloadSpeedR = 2.0f;
			magazineR = 30;
			reservesR = 60;
			audioR.clip = auto;
			reloadR = reload3;
			break;
		default: 
			shotSpeed = 2;
			DPS = 100;
			accuracy = 100;
			reloadSpeed = 1.2f;
			magazine = 15;
			reserves = 30;
			audioR.clip = pistol;
			reloadR = reload;
			break;
		}
			
		//Debug.Log (GunL);
		//Debug.Log (GunR);
		magazineMax = magazine;
		magazineMaxR = magazineR;
		reservesMax = reserves;
		reservesMaxR = reservesR;
	}

	//Inventory
	public void UseAmmoBox()
	{
		if (reserves < reservesMax || reservesR < reservesMaxR) 
		{
			if (GameManager.currentAmmoBoxes > 0) 
			{
				if (reserves < reservesMax) 
				{
					reserves += magazine;
				}
				if (reservesR < reservesMaxR) 
				{
					reservesR += magazineR;
				}

				GameManager.currentAmmoBoxes--;
				gameManager.SaveGame ();
				Debug.Log ("Ammo box used!");
			} 
			else
			{
				Debug.Log ("Error: no boxes!");
				return;
			}

		} 
		else 
		{
			Debug.Log ("Error: full ammo!");
			return;
		}
	}
//	public void UseMechBox()
//	{
//		if (reserves < reservesMax || reservesMaxR < reservesMaxR) {
//			if (GameManager.currentAmmoBoxes > 0) {
//				if (reserves < reservesMax) {
//					reserves += magazine;
//				}
//				if (reservesR < reservesMaxR) {
//					reservesR += magazineR;
//				}
//				GameManager.currentAmmoBoxes--;
//				Debug.Log ("Ammo box used!");
//			} else {
//				Debug.Log ("Error: no boxes!");
//				return;
//			}
//		} else {
//			Debug.Log ("Error: full ammo!");
//			return;
//		}
//	}

	public void fire()
	{
		//electGun ();
		Debug.Log (shotSpeed);
		if (magazine > 0) 
		{
			Instantiate (bullet, spawnPoints [0].transform.position, spawnPoints [0].transform.rotation);

			audio.PlayOneShot (audio.clip, 1.0f);
			GameManager.bulletCountL--;
			magazine--;
		} 
		if (magazine == 0 && reserves > 0)
		{
			btnL.interactable = false;
			StartCoroutine ("ReloadTime");
	
		}
		else if(magazine == 0 && reserves == 0){
			btnL.interactable = false;
			audio.clip = empty;
			audio.PlayOneShot (audio.clip, 1.0f);
		}
	}
	public void fireR()
	{

		if (magazineR > 0) 
		{
			Instantiate (bulletR, spawnPoints [1].transform.position, spawnPoints [1].transform.rotation);
			audioR.PlayOneShot (audioR.clip, 1.0f);
			GameManager.bulletCountR--;
			magazineR--;
		}
		else if(magazineR == 0 && reservesR > 0)
		{
			btnR.interactable = false;
			StartCoroutine ("ReloadTimeR");
		}
		else if(magazineR == 0 && reservesR == 0){
			btnR.interactable = false;
			audio.clip = empty;
			audio.PlayOneShot (audio.clip, 1.0f);
		}
	}

	IEnumerator ReloadTime()
	{
		AudioClip temp = audio.clip;
		audio.clip = reloadL;
		audio.PlayOneShot (audio.clip, 1.0f);

		yield return new WaitForSeconds (reloadSpeed);

		audio.clip = temp;
		magazine = magazineMax;
		reserves = reserves - magazine;
		btnL.interactable = true;
		Debug.Log("reload complete");
	}
	IEnumerator ReloadTimeR()
	{
		AudioClip temp = audioR.clip;
		audioR.clip = reloadR;
		audioR.PlayOneShot (audioR.clip, 1.0f);

		yield return new WaitForSeconds (reloadSpeedR);

		audioR.clip = temp;
		magazineR = magazineMaxR;
		reservesR = reservesR - magazineR;
		btnR.interactable = true;
		Debug.Log("reload complete");
	}
}



