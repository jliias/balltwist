using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

	public GameObject boxPrefab1;
	public GameObject boxPrefab2;
	public GameObject boxMovingPrefab;
	public GameObject fencePrefab1;
	public GameObject fencePrefab2;
	public GameObject treasure;

	public float leftLimit = -3f;
	public float rightLimit = 3f;

	public float gameSpeed;
	public int currentStage;

	private float startTime;
	private float triggerLimit;

	// Use this for initialization
	void Start () {
		startTime = Time.time;
		triggerLimit = 1f;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > triggerLimit) {
			int select = Random.Range (0, currentStage + 2);
			switch (select) {
			case 0:
				SpawnTreasure (Random.Range (-3f, 3f), this.transform.position.z);
				break;
			case 1:
				SpawnNormalBoxes ();	
				break;
			case 2:
				SpawnFence ();
				break;
			case 3:
				SpawnMovingBoxes ();
				break;
			default:
				SpawnNormalBoxes ();	
				break;
			}
			//triggerLimit = Random.Range (0.5f, 2f);
			triggerLimit = 1f;
			startTime = Time.time;
		}
	}

	void SpawnNormalBoxes() {
		Vector3 boxPos = this.transform.position;
		int select = Random.Range (0,5);
		if (select == 0) {
			// spawn two boxes (left and right)
			float deviationX = Random.Range (2f, 6f);
			boxPos.x = -deviationX;
//			if (Random.Range (0, 2) == 0) {
//				SpawnTreasure (Random.Range (-deviationX, deviationX), this.transform.position.z);
//				float boxOffset = Random.Range (10f, 20f);
//				boxPos.z += boxOffset;
//			}
			GameObject newBox1 = Instantiate (boxPrefab1, boxPos, Quaternion.identity);
			newBox1.GetComponent<EnemyScript> ().gameSpeed = gameSpeed;
			boxPos.x = deviationX;
			GameObject newBox2 = Instantiate (boxPrefab1, boxPos, Quaternion.identity);
			newBox2.GetComponent<EnemyScript> ().gameSpeed = gameSpeed;
		} else {
			// spawn single box to the middle of the track
//			if (Random.Range (0, 2) == 0) {
//				SpawnTreasure (Random.Range(-1f, 1f), this.transform.position.z);
//				float boxOffset = Random.Range (10f, 20f);
//				boxPos.z += boxOffset;
//			}
			GameObject newBox = Instantiate (boxPrefab2, boxPos, Quaternion.identity);
			newBox.GetComponent<EnemyScript> ().gameSpeed = gameSpeed;
		}
	}

	void SpawnMovingBoxes() {
		Vector3 boxPos = this.transform.position;
		//GameObject newTreasure = SpawnTreasure (this.transform.position.x, this.transform.position.z);
		//newTreasure.GetComponent<TreasureScript> ().gameSpeed = 0f;
		//float boxOffset = Random.Range (10f, 15f);
		//boxPos.z += boxOffset;
		GameObject newBox1 = Instantiate (boxMovingPrefab, boxPos, Quaternion.identity);
		newBox1.GetComponent<EnemyScript> ().gameSpeed = gameSpeed;
		newBox1.GetComponent<EnemyScript> ().moveDir = 1;
		newBox1.GetComponent<EnemyScript> ().currentStage = currentStage;
		GameObject newBox2 = Instantiate (boxMovingPrefab, boxPos, Quaternion.identity);
		newBox2.GetComponent<EnemyScript> ().gameSpeed = gameSpeed;
		newBox2.GetComponent<EnemyScript> ().moveDir = -1;
		newBox2.GetComponent<EnemyScript> ().currentStage = currentStage;
		if (currentStage > 2) {
			if (Random.Range (0, 2) == 0) {
				GameObject newBox3 = Instantiate (boxMovingPrefab, boxPos, Quaternion.identity);
				newBox3.transform.localScale = new Vector3 (2f, 4f, 3f);
				newBox3.GetComponent<EnemyScript> ().gameSpeed = gameSpeed;
				newBox3.GetComponent<EnemyScript> ().moveDir = 0;
				newBox3.GetComponent<EnemyScript> ().currentStage = currentStage;
			}
		}

//		int boxSelect = Random.Range (0,2);
//		if (boxSelect == 0) {
//			newTreasure.transform.parent = newBox1.transform;
//		} else {
//			newTreasure.transform.parent = newBox2.transform;
//		}
	}

	void SpawnFence() {
		Vector3 fencePos = this.transform.position;
		GameObject newFence;
		int select = Random.Range (0,2);
		if (select == 0) {
			newFence = Instantiate (fencePrefab1, fencePos, Quaternion.identity);
		} else {
			newFence = Instantiate (fencePrefab2, fencePos, Quaternion.identity);

		}
		newFence.GetComponent<EnemyScript> ().gameSpeed = gameSpeed;
	}

	GameObject SpawnTreasure(float xPosition, float zPosition) {
		Vector3 treasurePos = this.transform.position;
		treasurePos.x = xPosition;
		treasurePos.z = zPosition;
		GameObject newTreasure = Instantiate (treasure, treasurePos, Quaternion.identity);
		if (Random.Range (0, 2) == 0) {
			newTreasure.GetComponent<TreasureScript> ().treasureColor = "red";
		} else {
			newTreasure.GetComponent<TreasureScript> ().treasureColor = "blue";
		}
		newTreasure.GetComponent<TreasureScript> ().gameSpeed = gameSpeed;
		return newTreasure;
	}
}
