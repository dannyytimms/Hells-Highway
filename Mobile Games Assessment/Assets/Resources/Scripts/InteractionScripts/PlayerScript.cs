using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	public delegate void TargetInteraction(GameObject sender, TargetingEventArgs args);

	public event TargetInteraction E_TargetInRange; //check for sphere collision with player
	public event TargetInteraction E_InteractingTarget; // check if interaction button has been pressed

	List<IPlayerInteractable> interactables = new List<IPlayerInteractable>();
	IPlayerInteractable targetInteractable;

	GameObject objectToInteract;

	private void Awake()
	{
	//	EventManager.EnemyDestroyed += RemoveEnemyFromList;

	}
	void OnTriggerEnter(Collider col)
	{
		
		if(col.gameObject.tag == "obstacle" || col.gameObject.tag == "powerup")
		{
			
		IPlayerInteractable interactable = col.GetComponent<IPlayerInteractable> ();

		if (interactable != null) 
			{
			if (E_TargetInRange != null) 
			{
				E_TargetInRange (gameObject, new TargetingEventArgs (interactable.GetInteractableType ()));
			}

			interactable.Highlight (true);
			interactables.Add (interactable);
			}
		}
	}
	void OnTriggerExit(Collider col)
	{
		IPlayerInteractable interactable = col.GetComponent<IPlayerInteractable> ();

		if(interactable != null)
		{
			interactable.Highlight (false);
			interactable.InteractEnd ();
			interactables.Remove (interactable);
			Debug.Log (interactable);
		}
	}

	public void InteractButtonPressed(){
		if(targetInteractable != null)
		{
			//Debug.Log (interactables);
			//GameObject target = targetInteractable.InteractBegin ();

			if(E_InteractingTarget != null)
			{
				E_InteractingTarget (gameObject, new TargetingEventArgs (targetInteractable.GetInteractableType ()));
			}
		}
	}

	public void InteractButtonReleased(){
		if(targetInteractable != null)
		{
			targetInteractable.Highlight(true);
		}
	}
		

	void Update () {
		if (interactables.Count > 1) {
			targetInteractable = GetNearestInteractable ();

			foreach (IPlayerInteractable interactable in interactables) {
				if (interactable != targetInteractable) {
					interactable.Highlight (false);
				} else {
					interactable.Highlight (true);
				}
			}
		} else if (interactables.Count > 0) {
			targetInteractable = interactables [0];
		} else {
			targetInteractable = null;
		}

	}
	public IPlayerInteractable GetNearestInteractable()
	{
		float near = 9999;
		IPlayerInteractable nearestInteractable = interactables [0];
		foreach (IPlayerInteractable interactable in interactables) {
			float dist = Vector3.Distance (interactable.GetPosition (), transform.position);
			if (dist < near) {
				near = dist;
				nearestInteractable = interactable;
			}
		}

		return nearestInteractable;

	}

	public PlayerMovement GetPlayerController()
	{
		return GetComponent<PlayerMovement> ();
	}

	void RemoveEnemyFromList(GameObject sender, EnemyEventArgs args)
	{
		interactables.Remove (args.enemy.GetComponent<IPlayerInteractable> ());

	}



}
