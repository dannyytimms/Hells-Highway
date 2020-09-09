using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Podium : MonoBehaviour {

	public float speed;

	void Update () 
	{
		Scene scene = (SceneManager.GetActiveScene ());
		if (scene.name == "Garage") {
			Time.timeScale = 1.0f;
			gameObject.transform.Rotate (Vector3.up * (speed * Time.deltaTime));
			if (gameObject.transform.rotation.y == 360) 
			{
				gameObject.transform.eulerAngles = new Vector3 (0, 0, 0);
			}
		}
	}
}
