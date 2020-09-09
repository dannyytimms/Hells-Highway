using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class roadMovement : MonoBehaviour {

	float speed = 30.0f;

	void Update () 
	{
			gameObject.transform.Translate ((int)speed * Time.deltaTime, 0, 0);
	}
		
}
