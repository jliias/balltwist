using UnityEngine;
using EasyMobile;
using System.Collections.Generic;
using System;

public class ConsentDialogDisplayer : MonoBehaviour
{
    #region Privacy Policy URLs
    public const string westslothPolicyURL = "http://westsloth.com/privacy_policy.html";
    public const string adMobPolicyURL = "https://support.google.com/admob/answer/6128543?hl=en";
    public const string unityAdsPolicyURL = "https://unity3d.com/legal/privacy-policy";
    public const string googlePlayServicesURL = "https://policies.google.com/privacy";
    public const string unityAnalyticsOptOutURLPlaceholder = "UNITY_ANALYTICS_URL";

    private string unityAnalyticsOptOutURL;
    #endregion

    #region Consent Dialog - English

    public const string EnTitle = "Ball Twister Consent";

    public const string EnFirstParagraph = "<b>We hope that you are excited to play Ball Twister game!</b>\n\n";

    public const string EnSecondParagraph = "This application collects certain information about your use of our app. " +
                                      "We would like to get your permission to use device data for advertising, analytics "+
                                      "and notification purposes. We are following our " +
                                      "<a href=\"" + westslothPolicyURL + "\">Privacy Policy</a> to access and process your information. \n\n";

    public const string EnThirdParagraph = "Consent is optional and you may use the app without consent. However, please understand that " +
                                           "some features may not function properly if you deny our access. \n\n";

    // The title of the toggle for the Advertising module consent in English
    public const string EnAdsToggleTitle = "Advertising";

    // The On description of the toggle for the Advertising module consent in English
    public const string EnAdsToggleOnDesc = "You will receive relevant ads! " +
                                            "Our ad providers will collect data and use a unique identifier on your device. " +
                                            "You can review their policies: <a href=\"" + adMobPolicyURL + "\">AdMob</a> " +
                                            "and <a href=\"" + unityAdsPolicyURL + "\">Unity Ads</a>.";

    // The Off description of the toggle for the Advertising module consent in English
    public const string EnAdsToggleOffDesc = "Our ad providers will collect data and use a unique identifier on your device to show you relevant ads. " +
                                             "Here're their policies: <a href=\"" + adMobPolicyURL + "\">AdMob</a> " +
                                             "and <a href=\"" + unityAdsPolicyURL + "\">Unity Ads</a>.";

    public const string EnNotifsToggleTitle = "Notifications";

    //// The title of the toggle for the Notifications module consent in English
    //public const string EnNotifsToggleTitle = "Notifications";

    //// The common description (both On & Off states) of the toggle for the Notifications module consent in English
    //public const string EnNotifsToggleDesc = "Our service provider, OneSignal, will collect data and use a unique identifier on your device to send you push notifications. " +
    //                                         "You can learn about <a href=\"" + oneSignalPolicyURL + "\">One Signal Privacy Policy</a>.";

    // The title of the toggle for Unity Analytics consent in English
    // Note that this toggle is On by default and cannot be changed by the user, because we can't opt-out
    // Unity Analytics locally.
    // Instead we use the Unity Data Privacy Plugin to fetch an opt-out URL and present it to the user.
    // https://assetstore.unity.com/packages/add-ons/services/unity-data-privacy-plug-in-118922
    public const string EnAnalyticsToggleTitle = "Analytics*";

    // The common description (both On & Off states) of the toggle for Unity Analytics consent in English
    public const string EnAnalyticsToogleDesc = "We use Unity Analytics service to collect certain analytical information necessary for us to improve this app. " +
                                                "You can opt-out of this use by visiting <a href=\"" + unityAnalyticsOptOutURLPlaceholder + "\">this link</a>.";

    // The description of the toggle for Unity Analytics consent that is used if the opt-out URL can't be fetched, in English.
    public const string EnAnalyticsToggleUnavailDesc = "We use Unity Analytics service to collect certain analytical information necessary for us to improve this app. " +
                                                       "You can opt-out of this use by visiting an opt-out URL, which unfortunately <b>can't be fetched now</b>. But you can opt-out later in the \"Privacy\" page of this app.";

    // The second paragraph of the dialog content in English
    public const string EnFourthParagraph = "Click the below button to confirm your consent. You can change this consent at any time in the \"Privacy\" page of this app.";

    // The button title in English.
    public const string EnButtonTitle = "Accept";

    #endregion

    #region Toggle and Button IDs

    private const string AdsToggleId = "em-demo-consent-toggle-ads";
    private const string NotifsToggleId = "em-demo-consent-toggle-notifs";
    private const string UnityAnalyticsToggleId = "em-demo-consent-toggle-unity-analytics";
    private const string AcceptButtonId = "em-demo-consent-button-ok";

    #endregion

    #region Public Settings

    [Header("GDPR Settings")]
    [Tooltip("Whether we should request user consent for this app")]
    public bool shouldRequestConsent = true;

    [Header("Object References")]
    public GameObject isInEeaRegionDisplayer;
    //public DemoUtils demoUtils;

    #endregion


    public Action<string> FetchSuccess;
    public Action<string> FetchFailed;

    private bool hasSubscribedEvents = false;

    // Start is called before the first frame update
    void Start()
    {
        unityAnalyticsOptOutURL = "EMPTY";
        FetchSuccess += LoadURL;
        FetchFailed += ErrorMsg;
        UnityEngine.Analytics.DataPrivacy.FetchPrivacyUrl(FetchSuccess, FetchFailed);
        ShowDefaultConsentDialog();
    }

    // Grabs the default consent dialog, localizes and then shows it.
    public void ShowDefaultConsentDialog()
    {
        // Grab the default consent dialog that was built with the composer.
        ConsentDialog dialog = ConstructConsentDialog();

        // Replace placeholder texts in main content with localized texts. 
        // You may need to repeat this multiple times to replace all placeholders.
        dialog.Content = dialog.Content.Replace("PLACEHOLDER_TEXT", "LOCALIZED_TEXT");

        // Iterate through all toggles in the dialog and localize them.
        foreach (ConsentDialog.Toggle toggle in dialog.Toggles)
        {
            // Localize the toggle title.
            toggle.Title = toggle.Title.Replace("PLACEHOLDER_TOGGLE_TITLE", "LOCALIZED_TOGGLE_TITLE");

            // Localize the toggle on description.
            toggle.OnDescription = toggle.OnDescription.Replace("PLACEHOLDER_ON_DESCRIPTION", "LOCALIZED_ON_DESCRIPTION");

            // Localize the toggle off description if needed.
            if (toggle.ShouldToggleDescription)
            {
                toggle.OffDescription = toggle.OffDescription.Replace("PLACEHOLDER_OFF_DESCRIPTION", "LOCALIZED_OFF_DESCRIPTION");
            }
            // Here you can also set the toggle value according to the
            // stored consent (if any) to reflect the current consent status.
            // toggle.IsOn = TRUE_OR_FALSE; 
        }


        // Iterate through all buttons in the dialog and localize them.
        foreach (ConsentDialog.Button button in dialog.ActionButtons)
        {
            // Localize the button text.
            button.Title = button.Title.Replace("PLACEHOLDER_BUTTON_TITLE", "LOCALIZED_BUTTON_TITLE");
        }

        // Show the default consent dialog. If you want to allow the user to dismiss the dialog
        // without updating their consent, pass 'true' to the 'dismissible' argument. Otherwise
        // the dialog can only be closed with one of the action buttons.
        if (!ConsentDialog.IsShowingAnyDialog())
        {
            // Subscribe to the default consent dialog events.
            // Only do this once.
            if (!hasSubscribedEvents)
            {
                dialog.ToggleStateUpdated += DefaultDialog_ToggleStateUpdated;
                dialog.Dismissed += DefaultDialog_Dismissed;
                dialog.Completed += DefaultDialog_Completed;
                hasSubscribedEvents = true;
            }

            // Now shows the dialog and don't allow the user to dismiss it (must provide explicit consent).
            dialog.Show(false);
        }
        else
        {
            Debug.Log("Another consent dialog is being shown!");
        }
    }

    // Event handler to be invoked when the value of a toggle in the consent dialog is updated.
    void DefaultDialog_ToggleStateUpdated(ConsentDialog dialog, string toggleId, bool isOn)
    {
        Debug.Log("Toggle with ID " + toggleId + " now has value " + isOn);

        // If there's a service mandatory to the operation of your app,
        // you may disable all buttons until the toggle associated with that service
        // is turn on, so that the user can only close the dialog once they grant consent to that service.
        if (toggleId.Equals("MANDATORY_TOGGLE"))
            dialog.SetButtonInteractable("SOME_BUTTON_ID", true);   // make the button clickable
    }


    // Event handler to be invoked when the consent dialog is dismissed.
    void DefaultDialog_Dismissed(ConsentDialog dialog)
    {
        Debug.Log("The consent dialog has been dismissed!");
        //SceneManager.LoadScene("02_Play");
    }

    // Event handler to be invoked when the consent dialog completed.
    void DefaultDialog_Completed(ConsentDialog dialog, ConsentDialog.CompletedResults results)
    {
        // The 'results' argument contains the ID of clicked button.
        Debug.Log("The consent dialog has completed with button ID " + results.buttonId);

        // The 'results' argument also returns the values of the toggles in the dialog.
        foreach (KeyValuePair<string, bool> kvp in results.toggleValues)
        {
            Debug.Log("Toggle with ID " + kvp.Key + " has value " + kvp.Value);

            // Here you can perform relevant actions, e.g. update the consent
            // for individual services according to the toggle value...
        }
        //SceneManager.LoadScene("02_Play");
    }


    // Constructs a consent dialog from script.
    public ConsentDialog ConstructConsentDialog()
    {
        // First create a new consent dialog.
        ConsentDialog dialog = new ConsentDialog();

        // Set the title.
        dialog.Title = EnTitle;

        // Add the first paragraph.
        dialog.AppendText(EnFirstParagraph);
        dialog.AppendText(EnSecondParagraph);
        dialog.AppendText(EnThirdParagraph);


        // Build and append the toggle for advertising service consent.
        ConsentDialog.Toggle adsToggle = new ConsentDialog.Toggle(AdsToggleId);
        adsToggle.Title = EnAdsToggleTitle;
        adsToggle.OnDescription = EnAdsToggleOnDesc;
        adsToggle.OffDescription = EnAdsToggleOffDesc;
        adsToggle.ShouldToggleDescription = true;   // make the description change with the toggle state.
        adsToggle.IsOn = false;     // make the toggle off by default

        // Append the toggle after the 1st paragraph.
        dialog.AppendToggle(adsToggle);

        // Build and append the toggle for notifications service consent.
        ConsentDialog.Toggle notifsToggle = new ConsentDialog.Toggle(NotifsToggleId);
        notifsToggle.Title = EnNotifsToggleTitle;
        notifsToggle.OnDescription = "NOTIFS_TOGGLE_ON_DESCRIPTION";
        notifsToggle.ShouldToggleDescription = false;   // use same description for both on & off states.
        notifsToggle.IsOn = false; // make the toggle off by default

        // Append the toggle below the previous toggle.
        dialog.AppendToggle(notifsToggle);

        // Build and append the toggle for analytics service consent.
        ConsentDialog.Toggle uaToggle = new ConsentDialog.Toggle(UnityAnalyticsToggleId);
        uaToggle.Title = EnAnalyticsToggleTitle;
        uaToggle.OnDescription = "<a href=\"" + unityAnalyticsOptOutURL + "\">Unity Data privacy</a>";
        uaToggle.OffDescription = "ANALYTICS_TOGGLE_OFF_DESCRIPTION";
        uaToggle.ShouldToggleDescription = false;   // the description won't change when the toggle switches between on & off states.
        uaToggle.IsInteractable = false; // not interactable
        uaToggle.IsOn = true;   // assuming analytics is vital to our app, make its toggle on by default

        // Append the toggle below the previous toggle.
        dialog.AppendToggle(uaToggle);

        // Append the second paragraph.
        dialog.AppendText("SECOND_PARAGRAPH_TEXT");

        // Build and append the accept button.
        // A consent dialog should always have at least one button!
        ConsentDialog.Button okButton = new ConsentDialog.Button("OK_BUTTON_ID");
        okButton.Title = "OK_BUTTON_TITLE";
        okButton.TitleColor = Color.white;
        okButton.BodyColor = new Color(66 / 255f, 179 / 255f, 1);

        // Append the button to the bottom of the dialog.
        dialog.AppendButton(okButton);

        return dialog;
    }

    void LoadURL(string url)
    {
        Debug.Log("URL: " + url);
        unityAnalyticsOptOutURL = url;
        //Application.OpenURL(url);
    }

    void ErrorMsg(string fail)
    {
        Debug.Log("URL not found!");
        unityAnalyticsOptOutURL = "NOT DEFINED!";
    }
}