using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallsBehaviour : MonoBehaviour
{

	public float gameSpeed;

	public GameObject leftBallPrefab;
	public GameObject rightBallPrefab;

	private GameObject leftBall;
	private GameObject rightBall;

	public GameObject explosion;

	public bool gameRunning;
	public bool gameOver;

	public float leftMovement;
	public float rightMovement;
	public bool altSteering = false;

	private Vector3 leftBallpos;
	private Vector3 rightBallpos;

	// Use this for initialization
	void Start ()
	{
		gameOver = false;
		gameRunning = false;
	}
	
	// Update is called once per frame
	void Update ()
	{
        // Game steering logic
		if (!gameOver) {
			if (gameRunning) {	
				leftBallpos = this.leftBall.transform.position;
				rightBallpos = this.rightBall.transform.position;
				if (altSteering) {
					if (Input.GetMouseButtonDown (0)) {
						leftMovement = -leftMovement;
						rightMovement = -rightMovement;
					}
					leftBallpos.x += 0.75f * leftMovement * Time.deltaTime;
					rightBallpos.x += 0.75f * rightMovement * Time.deltaTime;
				} else {
					if (Input.GetMouseButton (0)) {
						//Debug.Log ("move in!");
						leftBallpos.x += 0.75f * gameSpeed * Time.deltaTime;
						rightBallpos.x -= 0.75f * gameSpeed * Time.deltaTime;
					} else {
						//Debug.Log ("move out!");
						leftBallpos.x -= 0.75f * gameSpeed * Time.deltaTime;
						rightBallpos.x += 0.75f * gameSpeed * Time.deltaTime;
					}
				}
			} 
			this.leftBall.transform.position = leftBallpos;
			this.rightBall.transform.position = rightBallpos;
		}
	}

    // Function to initialize balls to correct location
	public void initializeBalls ()
	{
		leftBall = Instantiate (leftBallPrefab, new Vector3 (-1.5f, 0f, 0f), Quaternion.identity);
		rightBall = Instantiate (rightBallPrefab, new Vector3 (1.5f, 0f, 0f), Quaternion.identity);
		leftBall.transform.parent = this.transform;
		rightBall.transform.parent = this.transform;
		leftBallpos = this.leftBall.transform.position;
		rightBallpos = this.rightBall.transform.position;
		leftBall.GetComponent<Ball> ().gameSpeed = gameSpeed;
		rightBall.GetComponent<Ball> ().gameSpeed = gameSpeed;
	}

    // set game to running state
	public void StartGame ()
	{
		leftMovement = gameSpeed;
		rightMovement = -gameSpeed;
		//Debug.Log ("gameSpeed: " + gameSpeed);
		gameRunning = true;
	}

    // Destroy balls
	public void DestroyBalls ()
	{
		gameOver = true;
		Instantiate (explosion, leftBall.transform.position, Quaternion.identity);
		Instantiate (explosion, rightBall.transform.position, Quaternion.identity);
		Destroy (leftBall.gameObject);
		Destroy (rightBall.gameObject);
	}
		
}
