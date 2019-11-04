using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ConsentLauncher : MonoBehaviour
{

    public Action<string> FetchSuccess;
    public Action<string> FetchFailed;

    // Start is called before the first frame update
    void Start()
    {
        FetchSuccess += LoadURL;
        FetchFailed += ErrorMsg;
        UnityEngine.Analytics.DataPrivacy.FetchPrivacyUrl(FetchSuccess, FetchFailed);
        //ConsentDialogDisplayer.ShowDefaultConsentDialog();
    }

    void LoadURL(string url)
    {
        Debug.Log("URL: " + url);
        //Application.OpenURL(url);
    }

    void ErrorMsg(string fail)
    {
        Debug.Log("URL not found!");
    }

}
