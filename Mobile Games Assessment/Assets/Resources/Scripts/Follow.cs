using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Author: Daniel Timms T016546E

public class Follow : MonoBehaviour 
{

	public GameObject obj; 
	private Vector3 distance;  

	void Start () 
	{
		
		distance = transform.position - obj.transform.position;

	}

	void LateUpdate () 
	{
		
		transform.position = obj.transform.position + distance;

	}
}