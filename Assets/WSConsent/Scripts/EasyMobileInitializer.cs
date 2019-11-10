using UnityEngine;
using System.Collections;
using EasyMobile; // include the Easy Mobile namespace to use its scripting API

public class EasyMobileInitializer : MonoBehaviour
{
    // Checks if EM has been initialized and initialize it if not.
    // This must be done once before other EM APIs can be used.
    void Awake()
    {
        if (!RuntimeManager.IsInitialized())
            RuntimeManager.Init();
    }
}