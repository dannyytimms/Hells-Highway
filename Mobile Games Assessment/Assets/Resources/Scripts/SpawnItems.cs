using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//Author: Daniel Timms T016546E

public class SpawnItems : MonoBehaviour 
{
	
	public float min;
	public float max;

	public Transform[] SpawnPoints;
	float spawnTime;

	public GameObject variant1;
	public GameObject variant2;
	public GameObject variant3;
	public GameObject variant4;
	GameObject pickedV;

	public float sphereRadius;

	public int maxZombies;
	public static int zombieCount;

	public static bool ready;

	void Start ()
	{
		spawnTime = Random.Range(min,max);
		InvokeRepeating ("Spawn", spawnTime, spawnTime);
	}

	void Update () 
	{
		spawnTime = Random.Range(min,max);

		if (variant3 == null) 
		{
			variant3 = variant1;
		}
		else if (variant4 == null) 
		{
			variant4 = variant2;
		}

		float variantPick = Random.Range(0,1000);

		if (variantPick <= 250) 
		{
			pickedV = variant1;
		} else if (variantPick >= 251 && variantPick <= 500)
		{
			pickedV = variant2;
		}	
		else if (variantPick >= 501 && variantPick <= 750) 
		{
			pickedV = variant3;
		}	
		else if (variantPick >= 751 && variantPick <= 1000) 
		{
			pickedV = variant4;
		}	
	}

	void Spawn()
	{
		if (ready) 
		{
			if (pickedV.gameObject.tag == "zombie") 
			{
			if (zombieCount < maxZombies) 
				{
				zombieCount++;
				if (!Physics.CheckSphere (transform.position, sphereRadius)) 
					{
					int spawnIndex = Random.Range (0, SpawnPoints.Length);
					Instantiate (pickedV, SpawnPoints [spawnIndex].position, pickedV.transform.rotation);
					ready = false;
					}
				}
				else 
				{
				Debug.Log ("Max Zombies!");
				}
			} 
			else 
			{
				if (!Physics.CheckSphere (transform.position, sphereRadius)) 
				{
					int spawnIndex = Random.Range (0, SpawnPoints.Length);
					Instantiate (pickedV, SpawnPoints [spawnIndex].position, pickedV.transform.rotation);
					ready = false;
				}
			}
		}
		StartCoroutine (Wait ());
	}

	IEnumerator Wait ()
	{
		yield return new WaitForSeconds (1.0f);
		ready = true;
	}
}
