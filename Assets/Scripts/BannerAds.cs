// --------------------------------------------------------------------
// Class for AdMob actions
// Author: Juha Liias / WestSloth Games
// --------------------------------------------------------------------

using UnityEngine;
using System.Collections;
using EasyMobile;

public class BannerAds : MonoBehaviour
{

    //    private BannerView bannerView;

    private bool npaSelected = false;
    public ConsentStatus consent;

    //    private string adUnitId;
    //    private string appId;


    public void Start()
    {

        //npaSelected = false;
        //if (PlayerPrefs.HasKey("consentValue"))
        //{
        //    int thisValue = PlayerPrefs.GetInt("consentValue", 0);
        //    if (thisValue == 0)
        //    {
        //        Advertising.GrantDataPrivacyConsent();
        //        npaSelected = false;
        //        Debug.Log("personalized ads!");
        //    }
        //    else
        //    {
        //        Advertising.RevokeDataPrivacyConsent();
        //        npaSelected = true;
        //        Debug.Log("random ads!");
        //    }
        //}

        // Reads the current module-level consent of the Advertising module.
        ConsentStatus moduleConsent = Advertising.DataPrivacyConsent;

        Debug.Log("999 Consent status: " + moduleConsent);

        // Forward the consent to the Advertising module.
        //if (consent.advertisingConsent == ConsentStatus.Granted)
        //    Advertising.GrantDataPrivacyConsent();
        //else if (consent.advertisingConsent == ConsentStatus.Revoked)
        //    Advertising.RevokeDataPrivacyConsent();

        // Show banner ad

        Advertising.ShowBannerAd(BannerAdPosition.Bottom);
    }

    //    private void RequestBanner()
    //    {


    //        // Create a 320x50 banner at the top of the screen.
    //        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

    //        if (npaSelected)
    //        {
    //            // Create an non-personalized ad request.
    //            AdRequest request = new AdRequest.Builder().AddExtra("npa", "1").Build();
    //            bannerView.LoadAd(request);
    //        }
    //        else
    //        {
    //            // Create personalized ad request
    //            AdRequest request = new AdRequest.Builder().Build();
    //            bannerView.LoadAd(request);
    //        }

    //        // Load the banner with the request and show it
    //        //bannerView.LoadAd(request);
    //        bannerView.Show();
    //    }

    public void DismissBanner()
    {
        
            //        bannerView.Destroy();
            Advertising.DestroyBannerAd();
    }
}
