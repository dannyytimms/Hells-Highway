using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
//Author: Daniel Timms T016546E

public class carMove : MonoBehaviour 
{

	public GameObject car;
	public GameObject dieMenu;
	public GameObject standardPanel;
	public AudioSource carAudio;
	gunFire gunfire;
	ZombieDie instance;
	GameManager gameManager = new GameManager ();

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "obstacle" || col.gameObject.tag == "zombie" || col.gameObject.tag == "HighwaySign") 
		{
			Debug.Log (col.gameObject.name);
			Destroy(car, 0.5f);
			Analytics.CustomEvent ("PlayerDeath", gameObject.transform.position);
			gameManager.SaveGame ();
			dieMenu.SetActive (true);
			standardPanel.SetActive (false);
			Time.timeScale = 0.0f;
			GameManager.score = 0;
		}
		else if (col.gameObject.tag == "powerup") 
		{
			for (int i = 0; i < 10; i++) 
			{
				Analytics.CustomEvent ("Ammo Powerup Received!");
				GameManager.bulletCountL++;		
				GameManager.bulletCountR++;	
				Destroy (col.gameObject);
			}
		}
	}

	public void Update()
	{
		Scene scene = SceneManager.GetActiveScene ();

		float accelX = 0;
		if (GameManager.useTime == true)
		if (scene.name == "StandardGameMode" || scene.name == "NightmareMode") 
		{
			accelX = Input.acceleration.x / 5;
		}

//		if (dieMenu == null)
//			dieMenu = GameObject.Find ("diePanel");
//		if (standardPanel == null)
//			standardPanel = GameObject.Find ("StandardPanel");
//		if (carAudio == null)
//			carAudio = GameObject.Find ("CarAudio");

//		Debug.Log (accelX);
//		if (carAudio.isPlaying == false) {
//			if (accelX > 0.01f || accelX < -0.01f) {
//				carAudio.PlayOneShot (carAudio.clip, 0.5f);
//				accelX = Input.acceleration.x / 5;
//			}
//		}
		gameObject.transform.Translate (-accelX, 0, 0);
	}
}
