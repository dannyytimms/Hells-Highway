using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parentObject : MonoBehaviour {

	void OnCollisionEnter(Collision col)
	{
		Debug.Log (col.gameObject.name);
		if (col.gameObject.tag == "road") 
		{
			gameObject.transform.SetParent (col.gameObject.transform);
		}
	}
}
