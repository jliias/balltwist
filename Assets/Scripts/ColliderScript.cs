using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderScript : MonoBehaviour
{

    // Collider object will destroy all "enemy" objects
    // when exiting to the visible scene
	void OnTriggerEnter (Collider other)
	{
		if (other.transform.tag == "enemy") {
			Destroy (other);
		}
	}

}


