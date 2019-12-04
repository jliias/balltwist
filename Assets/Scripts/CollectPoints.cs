using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectPoints : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Destroy this after 1s
        Object.Destroy(this.gameObject, 1.0f);
    }
}
