//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEngine.Analytics;
//using GooglePlayGames;
using UnityEngine.SceneManagement;
using EasyMobile;


public class GameManager : MonoBehaviour
{
    public GameObject scoreObject;
    public GameObject textObject1;
    public GameObject textObject2;
    public GameObject buttons;
    public GameObject textCredits;
    public Camera myCamera;

    public BallsBehaviour ballController;
    public GameObject obstacleSpawner;

    private Text textField1;
    private Text textField2;
    public Text scoreText;

    public float gameSpeed;

    private int gameState;

    private float counter;
    public int score;
    private int currentStage;

    private int highScore;

    private float delayTime = 1f;
    private float startTime;

    private bool isAndroid = false;
    //private string leaderBoardID = "CgkI74KkqIoHEAIQAQ";

    //public BannerAds myBannerAds;

    public Text debugText;

    private void Awake()
    {
        Debug.Log("Consent: " + Advertising.DataPrivacyConsent);
        if (!PlayerPrefs.HasKey("Balltwister_AppConsent"))
        {
            SceneManager.LoadScene("01_Consent");
        }
        else
        {
            Debug.Log("Ad consent granted: " + Advertising.DataPrivacyConsent);
        }

#if UNITY_ANDROID
        isAndroid = true;
        //GPlaySignIn();
        #endif
    }

    // Use this for initialization
    void Start()
    {
        obstacleSpawner.SetActive(false);
        highScore = 0;
        if (PlayerPrefs.HasKey("localHighScore"))
        {
            // Uncomment following line to reset local highscore (for debugging purposes)
            // PlayerPrefs.SetInt ("localHighScore", 0);
            highScore = PlayerPrefs.GetInt("localHighScore");
        }
        initializeGame();
    }

    // Update is called once per frame
    void Update()
    {
        if (ballController.gameOver && gameState == 1)
        {
            obstacleSpawner.SetActive(false);
            textObject1.SetActive(true);
            string endText = "Game Over!";
            if (score > highScore)
            {
                highScore = score;
                PlayerPrefs.SetInt("localHighScore", highScore);
                endText = endText + "\n\nHighscore!";
            }
            if (isAndroid)
            {
                GPlayPostScore(score);
            }
            Debug.Log(endText);
            textField1.text = endText;
            startTime = Time.time;
            gameState = 2;
        }

        if (gameState == 1)
        {
            counter += Time.deltaTime * 10;
            //Debug.Log ("score: " + score);
            if (score > 300 && currentStage < 1)
            {
                currentStage++;
                obstacleSpawner.GetComponent<ObstacleSpawner>().currentStage = currentStage;
            }
            else if (score > 1000 && currentStage < 2)
            {
                currentStage++;
                obstacleSpawner.GetComponent<ObstacleSpawner>().currentStage = currentStage;
            }
            else if (score > 2000 && currentStage < 3)
            {
                currentStage++;
                obstacleSpawner.GetComponent<ObstacleSpawner>().currentStage = currentStage;
            }
            //Debug.Log ("stage: " + currentStage);
        }
        else if (gameState == 2)
        {
            if (Time.time - startTime > delayTime)
            {
                gameState = 3;
                textField2.text = "Tap to continue";
                textObject2.SetActive(true);
                //myBanner.ShowMyBanner();
            }
        }

        score = (int)counter;
        scoreText.text = "Score: " + score;

        // Mouse button actions in each state
        if (Input.GetMouseButtonDown(0))
        {
            switch (gameState)
            {
                case 0:
                    // idle state waiting user interaction
                    // tap will launch game
                    if (!MyIsPointerOverGameObject())
                    {
                        textObject1.SetActive(false);
                        textObject2.SetActive(false);
                        scoreObject.SetActive(true);
                        obstacleSpawner.GetComponent<ObstacleSpawner>().gameSpeed = this.gameSpeed;
                        currentStage = 0;
                        obstacleSpawner.GetComponent<ObstacleSpawner>().currentStage = currentStage;
                        ballController.StartGame();
                        this.gameState = 1;
                        obstacleSpawner.SetActive(true);
                        buttons.SetActive(false);
                        textCredits.SetActive(false);
                        //myBanner.HideMyBanner();
                    }
                    break;
                case 1:
                    // So far no action from tap
                    break;
                case 2:
                    // So far no action from tap
                    break;
                case 3:
                    // This is final state after game over and waiting for user input
                    ballController.gameOver = false;
                    ballController.gameRunning = false;
                    textObject2.SetActive(true);
                    initializeGame();
                    break;
                default:
                    // No need for default, if everythink works as expected
                    Debug.Log("Something is wrong...");
                    break;
            }
            // No idea why this is here..? Should be removed, probably.
            this.textField1.GetComponent<Text>();
        }

        //Debug.Log ("Game State: " + gameState);


    }

    void initializeGame()
    {
        this.gameState = 0;
        this.counter = 0;
        textField1 = this.textObject1.GetComponent<Text>();
        textField2 = this.textObject2.GetComponent<Text>();
        ballController.initializeBalls();
        ballController.gameSpeed = 10f;
        textField1.text = "HighScore:\n" + highScore;
        textField2.text = "Tap and hold\nto control\nballs movement";
        scoreObject.SetActive(false);
        buttons.SetActive(true);
        textCredits.SetActive(true);
    }

    public void CollectCoin()
    {
        Debug.Log("coins collected!");
        counter += 100f;
    }

    void ResetGame()
    {
        if (gameState == 1)
        {
            gameState = 2;
        }
    }

    public void OpenMoreGames()
    {
        Application.OpenURL("https://play.google.com/store/apps/dev?id=5608008400672115508");
    }

    /// <returns>true if mouse or first touch is over any event system object ( usually gui elements )</returns>
    public static bool MyIsPointerOverGameObject()
    {
        //check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == TouchPhase.Began)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }

        return false;
    }

    void GPlaySignIn()
    {
        //if (!PlayGamesPlatform.Instance.IsAuthenticated()) {
        //    debugText.text = "Signing in to gplay";
        //    // Activate Play Games Platform
        //    PlayGamesPlatform.Activate();
        //    // authenticate user:
        //    Social.localUser.Authenticate((bool success) =>
        //    {
        //        if (success)
        //        {
        //            Debug.Log("Login successful!");
        //        }
        //        else
        //        {
        //            Debug.LogWarning("Failed to sign in with Google Play Games.");
        //        }
        //    });
        //}
    }

    void GPlayPostScore(int score)
    {
        Debug.Log("Sending score to leaderboard");
        GameServices.ReportScore(score, EM_GameServicesConstants.Leaderboard_Master_Ball_Twisters);
    }

    public void showLeaderBoard()
    {
        Debug.Log("Showing leaderboard");
        // Check for initialization before showing leaderboard UI
        if (GameServices.IsInitialized())
        {
            GameServices.ShowLeaderboardUI();
        }
        else
        {
            #if UNITY_ANDROID
            GameServices.Init();    // start a new initialization process
            #elif UNITY_IOS
            Debug.Log("Cannot show leaderboard UI: The user is not logged in to Game Center.");
            #endif
        }
    }

    public void SetupConsent() {
        //FindObjectOfType<AdMobAds>().DismissBanner();
        FindObjectOfType<BannerAds>().DismissBanner();
        SceneManager.LoadScene("01_Consent");
    }
}

