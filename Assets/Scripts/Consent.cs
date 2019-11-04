using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Consent : MonoBehaviour {

    public Text consentText;
    public GameObject buttons;

    public void PersonalizedAds() {
        PlayerPrefs.SetInt("consentValue", 0);
        ClearScreen();
        SceneManager.LoadScene("02_Play");
    }

    public void RandomAds() {
        PlayerPrefs.SetInt("consentValue", 1);
        ClearScreen();
        SceneManager.LoadScene("02_Play");
    }

    public void MoreInfo() {
        Application.OpenURL("http://www.westsloth.com/privacy_policy.html");
    }

    private void ClearScreen() {
        consentText.text = "\n\nSelection done, setting up game...\n\n\nPlease wait";
        buttons.SetActive(false);
    }
}
