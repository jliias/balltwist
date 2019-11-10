using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class ConsentLauncher : MonoBehaviour
{
    private void Start()
    {
        LaunchConsent();
    }

    public void LaunchConsent() {
        this.GetComponentInParent<EasyMobile.WSConsent.WSPrivacy>().ShowConsentDialog();
    }
}
