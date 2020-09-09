using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadCheck : MonoBehaviour 
{
	public Transform spawnPosition;

	void OnCollisionEnter(Collision col)
	{
		if (col.gameObject.tag == "road") 
		{
			col.gameObject.transform.position = spawnPosition.transform.position;

			foreach (Transform child in col.transform) 
			{
				if (child.tag != "road") 
				{
					if (child.tag == "zombie")
						SpawnItems.zombieCount--;
					Destroy (child.gameObject);
				}
			}
		}
	}
}
