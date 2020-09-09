using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarChanges : MonoBehaviour 
{

	public GameObject car;
	public GameObject R1, R2, R3, R4, R5;
	public GameObject S1, S2, S3, S4, S5;

	CarCustom carCustom;

	void Awake ()
	{
		carCustom = GetComponent<CarCustom> ();
	}

	void FixedUpdate () 
	{
		if (car.transform.rotation.eulerAngles.y != 180)
		{
			car.transform.eulerAngles = new Vector3 (0, 180, 0);
		}
	}
}
