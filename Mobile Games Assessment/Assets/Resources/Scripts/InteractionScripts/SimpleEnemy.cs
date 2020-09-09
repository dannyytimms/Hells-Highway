using UnityEngine;
using System.Collections;

public class SimpleEnemy : Enemy,IPlayerInteractable {

	public void Highlight(bool highlighted)
	{
		if (highlighted)
		{
			//Debug.Log (enemyType);
			if (enemyType == EnemyType.obstacle) {
				meshRenderer.material.color = Color.red;
			} else if (enemyType == EnemyType.powerup) {
				meshRenderer.material.color = Color.green;
			}
//			if (!takingDamage)
//			{
//				meshRenderer.material.color = Color.green;
//					
//			}
		}
		else
		{
			meshRenderer.material.color = Color.white;
		}
	}

	public GameObject InteractBegin()
	{
		takingDamage = true;
		meshRenderer.material.color = Color.red;
		return gameObject;
	}

	public GameObject InteractEnd()
	{
		takingDamage = false;
		meshRenderer.material.color = Color.white;
		return gameObject;
	}

	public Vector3 GetPosition()
	{
		return transform.position;
	}


	public InteractableType GetInteractableType()
	{
		return InteractableType.ENEMY_ZOMBIE;
	}

}
