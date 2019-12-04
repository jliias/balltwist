using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to implement moving enemies behavior
// 

public class EnemyScript : MonoBehaviour {

	public float gameSpeed;
	public bool isMoving;
	public int moveDir = 0;
	public int currentStage;

    // How far from spawner movement is triggered
	private float triggerDistance = 15f;
    // How far object is moved
	private float moveDistance = 4f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 currentPos = this.transform.position;
		currentPos.z = currentPos.z - Time.deltaTime * gameSpeed;
		if (moveDir != 0) {
			if (currentStage > 2) {
				triggerDistance = 30f;
				moveDistance = 5f;
			}
			if (currentPos.z < triggerDistance && currentPos.x < moveDistance && currentPos.x > -moveDistance) {
				currentPos.x = currentPos.x + moveDir * Time.deltaTime * gameSpeed / 2f;
			}
		}
		this.transform.position = currentPos;
	}

    // Destroy, if hit to boundaryCollider
	void OnTriggerEnter(Collider other) {
		if (other.transform.tag == "boundaryCollider") {
			if (this.transform.name == "Fence1" || this.transform.name == "Fence2") {
				Destroy (this.transform.parent);
			} else {
				Destroy (this.gameObject);
			}
		}
	}
}
