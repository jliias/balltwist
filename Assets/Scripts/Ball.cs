using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	public string thisColor;

    // puff of smoke to indicate hitting to wrong color coins
	public GameObject wrongTreasurePuff;

    // +100 to tell that ball hit correct color coins
    public GameObject collector;

	public float gameSpeed;

	// We need to access methods and variables from
	// BallsBehavior class
	private BallsBehaviour ballParent;

	// Use this for initialization
	void Start ()
	{
		// Find BallsBehaviour type component from parent gameobject
		ballParent = this.GetComponentInParent<BallsBehaviour> ();
	}

	// In case of trigger event detected
	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("Trigger!");
		if (other.transform.tag == "boundaryCollider") {
			// boundaryCollider will destroy balls (method at ballParent)
			ballParent.DestroyBalls ();
		} else if (other.transform.tag == "enemy") {
			// enemy tagged object will also destroy ball
			ballParent.DestroyBalls ();
		} else if (other.transform.tag == "treasure") {
			if (thisColor == other.GetComponent<TreasureScript> ().treasureColor) {
                // If hit correct colour treasure -> get more points
                Instantiate(collector, other.transform.position, Quaternion.identity);
                Transform.FindObjectOfType<GameManager> ().CollectCoin ();
				Destroy (other.gameObject);
			} else {
				// Wrong color treasure hit -> puff of smoke instantiated
				GameObject wrongPuff = Instantiate (wrongTreasurePuff, other.transform.position, Quaternion.identity);
				wrongPuff.transform.SetParent (other.transform);
			}
		}
	}
}
