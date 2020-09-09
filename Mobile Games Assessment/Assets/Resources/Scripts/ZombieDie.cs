using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Daniel Timms T016546E
public class ZombieDie : MonoBehaviour
{
	
	public Animator anim;
	public Transform center;
	public bool dead = false;

	float zSpeed = GameManager.zombieSpeed;
	public int zHealth = GameManager.zombieHealth;

	public bool rotationZombie;

	public Transform target;

	void Start () 
	{
		anim = GetComponentInParent <Animator> ();	
	}
		
	public GameObject InteractEnd()
	{
		//End combat
		return gameObject;
	}
		
	void Update ()
	{
		if(dead)
		{
			StartCoroutine ("increaseScore");
			dead = false;
		}
			
		if (zHealth <= 0) 
		{
			anim.SetTrigger ("Dead");
			dead = true;
			Debug.Log ("Zombie == dead");
			Destroy (this.gameObject, 1.0f);

		}
	}

	IEnumerator increaseScore()
	{
		bool action = false;

		while(!action)
		{
			GameManager.scoreIncrease ();
			action = true;
			yield return new WaitForSeconds (0.1f);
		}
	}
}
