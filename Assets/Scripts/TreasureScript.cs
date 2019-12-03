using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureScript : MonoBehaviour
{
    public Material redMaterial;
    public Material blueMaterial;

	public float gameSpeed;

	public string treasureColor;

	// Use this for initialization
	void Start ()
	{
		Material bottomMaterial = this.transform.Find ("Bottom").GetComponent<Renderer> ().material;
		Debug.Log ("material: " + bottomMaterial);
		if (treasureColor == "red") {
            this.transform.Find("Bottom").GetComponent<Renderer>().material = redMaterial;
            //bottomMaterial.color = Color.red;
		} else {
            this.transform.Find("Bottom").GetComponent<Renderer>().material = blueMaterial;
            //bottomMaterial.color = Color.blue;		
		}
	}

	// Update is called once per frame
	void Update ()
	{

		Vector3 currentPos = this.transform.position;
		currentPos.z = currentPos.z - Time.deltaTime * gameSpeed;
		this.transform.position = currentPos;
	}

	void OnTriggerEnter (Collider other)
	{
		Debug.Log ("Trigger!");
		if (other.transform.tag == "boundaryCollider") {
			Destroy (this.gameObject);
		}
	}
}

