using System.Collections;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public static EventManager instance = null;

	public delegate void EnemyInteraction(GameObject sender, EnemyEventArgs args);
	public static event EnemyInteraction enemyDestroyed;

	void Awake()
	{
		if (instance == null) {
			instance = this;
		} else {
			Destroy (gameObject);
		}
	}

	public static void EnemyDestroyed(GameObject sender,EnemyEventArgs args)
	{
		if (enemyDestroyed != null) {
			enemyDestroyed (sender, args);
		}
	}

}

public class EventArgs
{
	public static readonly EventArgs Empty;
	public EventArgs(){}
}

public class EnemyEventArgs
{
	public Enemy enemy;
	public EnemyType typeOfEnemy;

	public EnemyEventArgs(EnemyType type, Enemy enemy)
	{
		typeOfEnemy = type;
		this.enemy = enemy;
	}
}

public class TargetingEventArgs
{
	public InteractableType typeOfInteractable;
	public TargetingEventArgs(InteractableType type)
	{
		typeOfInteractable = type;
	}
}
