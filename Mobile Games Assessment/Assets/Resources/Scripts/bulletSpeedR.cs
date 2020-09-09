using System.Collections;
using System.Collections.Generic;
using UnityEngine;

///////////////////////////////////////
/////////
////// Author: Daniel Timms (c) 2018.
/////////
/////////////////////////////////////// 

public class bulletSpeedR : MonoBehaviour 
{

	float bSpeed =  gunFire.shotSpeedR;
	float bDamage = gunFire.DPSR;
	float bAccuracy = gunFire.accuracyR;


	ZombieDie zombieDie;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "zombie") 
		{
			zombieDie = col.gameObject.GetComponentInParent<ZombieDie> ();
			zombieDie.zHealth = zombieDie.zHealth - (int)bDamage;
		} 
		else if (col.gameObject.tag == "powerup") 
		{
			Destroy (col.gameObject);
		}
		Destroy (gameObject);
	}

	void Update () 
	{
		gameObject.transform.Translate (0, 0, bSpeed);
		Destroy (gameObject, 3.0f);
	}
}
