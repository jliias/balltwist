using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    // Materials for red and blue
    public Material redMaterial;
    public Material blueMaterial;

	public float gameSpeed;

    // the color of this object
	public string treasureColor;

	// Use this for initialization
	void Start ()
	{
        // Select this color according to treasureColor
		Material bottomMaterial = this.transform.Find ("Bottom").GetComponent<Renderer> ().material;
		Debug.Log ("material: " + bottomMaterial);
		if (treasureColor == "red") {
            this.transform.Find("Bottom").GetComponent<Renderer>().material = redMaterial;
		} else {
            this.transform.Find("Bottom").GetComponent<Renderer>().material = blueMaterial;	
		}
	}

	// Update is called once per frame
	void Update ()
	{
        // Move coins towards camera
		Vector3 currentPos = this.transform.position;
		currentPos.z = currentPos.z - Time.deltaTime * gameSpeed;
		this.transform.position = currentPos;
	}

    // Destroy if hit to boundarycollider
	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("Trigger!");
		if (other.transform.tag == "boundaryCollider") {
			Destroy (this.gameObject);
		}
	}
}

