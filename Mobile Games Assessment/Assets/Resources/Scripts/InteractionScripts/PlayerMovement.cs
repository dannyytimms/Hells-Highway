using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(PlayerScript))]
[RequireComponent(typeof(Rigidbody))]


public class PlayerMovement : MonoBehaviour
{

	Rigidbody physics;
	PlayerScript playerScript;

	public float acceleration = 10.0f;
	float maxSpeed = 4;

	void Start () 
	{
		physics = GetComponent<Rigidbody> ();
		playerScript = GetComponent<PlayerScript>();
	}

	void Update () 
	{

		if (Input.GetKeyDown (KeyCode.E))
		{
			playerScript.InteractButtonPressed ();
		}
		if (Input.GetKeyUp (KeyCode.E))
		{
			playerScript.InteractButtonReleased ();
		}


		if (physics.velocity.magnitude < maxSpeed)
		{
			if (Input.GetKey(KeyCode.W))
			{
				physics.AddForce(Vector3.forward * acceleration);
			}
			if (Input.GetKey(KeyCode.S))
			{
				physics.AddForce(-Vector3.forward * acceleration);
			}
			if (Input.GetKey(KeyCode.D))
			{
				physics.AddForce(Vector3.right * acceleration);
			}
			if (Input.GetKey(KeyCode.A))
			{
				physics.AddForce(-Vector3.right * acceleration);
			}

		}
	}
}
