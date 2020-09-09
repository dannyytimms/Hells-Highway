using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///////////////////////////////////////
/////////
////// Author: Daniel Timms (c) 2018.
/////////
/////////////////////////////////////// 

public class CarCustom : MonoBehaviour 
{

	public GameObject carObject;

	public Material ColourMesh;
	public Slider R,G,B;
	public Color32 colour;
	public GameObject prefab;
	public GameObject R1, R2, R3, R4, R5;
	public GameObject S1, S2, S3, S4, S5;
	GameObject currentRim, currentSpoiler;
	CarChanges carChanges;
	GameManager gameManager = new GameManager();

	void Awake()
	{
		DontDestroyOnLoad (carObject);

		carChanges = GetComponent<CarChanges> ();

		colour.r = (byte)R.value;
		colour.g = (byte)G.value;
		colour.b = (byte)B.value;
	}
	void Start () 
	{
	currentRim = R5;
	currentSpoiler = S3;
	}

	public void SaveChanges()
	{
		Debug.Log ("Saving prefab changes");
		gameManager.SaveGame ();
	}
	public void ReturnToMenu()
	{
		carObject.transform.eulerAngles = new Vector3 (0, 180, 0);
		SceneManager.LoadScene (0);
	}


	//Colour
	void Update()
	{
		colour.r = (byte)R.value;
		colour.g = (byte)G.value;
		colour.b = (byte)B.value;
		ColourMesh.color = colour;
	}
	//Rims:
	public void rim1(){
		if (currentRim != R1) {
			R1.gameObject.SetActive (true);
			currentRim.SetActive (false);
			currentRim = R1;
			//carChanges.R1.SetActive (true);
		}
	}
	public void rim2(){
		if (currentRim != R2) {
			R2.gameObject.SetActive (true);
			currentRim.SetActive (false);
			currentRim = R2;
		//	carChanges.R1.SetActive (true);

		}
	}
	public void rim3(){
		if (currentRim != R3) {
			R3.gameObject.SetActive (true);
			currentRim.SetActive (false);
			currentRim = R3;
			//carChanges.R1.SetActive (true);

		}
	}
	public void rim4(){
		if (!currentRim != R4) {
			R4.gameObject.SetActive (true);
			currentRim.SetActive (false);
			currentRim = R4;
		}
	}
	public void rim5(){
		if (!currentRim != R5) {
			R5.gameObject.SetActive (true);
			currentRim.SetActive (false);
			currentRim = R5;
		}
	}


	//Spoilers:
	public void spoiler1(){
		if (currentSpoiler != S1) {
			S1.gameObject.SetActive (true);
			currentSpoiler.SetActive (false);
			currentSpoiler = S1;
		}
	}
	public void spoiler2(){
		if (currentSpoiler != S2) {
			S2.gameObject.SetActive (true);
			currentSpoiler.SetActive (false);
			currentSpoiler = S2;
		}
	}
	public void spoiler3(){
		if (currentSpoiler != S3) {
			S3.gameObject.SetActive (true);
			currentSpoiler.SetActive (false);
			currentSpoiler = S3;
		}
	}
	public void spoiler4(){
		if (currentSpoiler != S4) {
			S4.gameObject.SetActive (true);
			currentSpoiler.SetActive (false);
			currentSpoiler = S4;
		}
	}
	public void spoiler5(){
		if (currentSpoiler != S5) {
			S5.gameObject.SetActive (true);
			currentSpoiler.SetActive (false);
			currentSpoiler = S5;
		}
	}
}
