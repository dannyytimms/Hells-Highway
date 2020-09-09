using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType {obstacle,powerup, cube};

public class Enemy : MonoBehaviour {

	public EnemyType enemyType;

	protected MeshRenderer meshRenderer;

	protected bool takingDamage;

	void Start () {
		meshRenderer = GetComponent<MeshRenderer> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x > 0.25) {
			if (takingDamage) {
				float newScale = transform.localScale.x / (1 + Time.deltaTime * 2);
				transform.localScale = new Vector3 (newScale, newScale, newScale);
			}
		}
		else{
			EventManager.EnemyDestroyed (gameObject, new EnemyEventArgs (enemyType, this));
			//GameObject particleEmitter = Instantiate (Resources.Load ("Prefabs/ParticleEmitter"), transform.position, Quaternion.identity) as GameObject;
			gameObject.SetActive (false);

	}
}
}

