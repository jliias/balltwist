﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {

	private float rotationSpeed = 90f;

    // Update will rotate coins
	void Update() {
		transform.Rotate (Vector3.up * Time.deltaTime * rotationSpeed, Space.World);
	}
}
