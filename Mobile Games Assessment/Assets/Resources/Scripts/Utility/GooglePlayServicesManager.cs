using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;

public class GooglePlayServicesManager : MonoBehaviour
{
	
    static GooglePlayServicesManager instance = null;

    Dictionary<string, string> achivementKeys = new Dictionary<string, string>();
    Dictionary<string, string> leaderboardKeys = new Dictionary<string, string>();

    private void Awake()
    {
		PlayGamesPlatform.Activate ();
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        DontDestroyOnLoad(this);
    }

    void Start()
    {
		PlayGamesPlatform.DebugLogEnabled = true;
		PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder ().Build ();
		PlayGamesPlatform.InitializeInstance (config);

        LoadAchievementKeys();
        LoadLeaderboardKeys();
    }

    void LoadAchievementKeys()
    {
		achivementKeys.Add("OpenedTheGame", "CgkI77rysJYNEAIQAQ");
		achivementKeys.Add("Test1", "CgkI77rysJYNEAIQAw");
		achivementKeys.Add("Test2", "CgkI77rysJYNEAIQBA");
		achivementKeys.Add("Test3", "CgkI77rysJYNEAIQBQ");
		achivementKeys.Add("Test4", "CgkI77rysJYNEAIQBg");
    }

    void LoadLeaderboardKeys()
    {
		achivementKeys.Add("Deaths", "CgkI77rysJYNEAIQAg");
    }

    public void SignIn()
    {
		PlayGamesPlatform.Instance.Authenticate (SignedIn);
		SignInAttempt ();
    }

    bool SignInAttempt()
    {
        bool signedInSuccessfully = false;
        PlayGamesPlatform.Instance.Authenticate((bool success)=> { signedInSuccessfully = success; });
		Debug.Log (signedInSuccessfully);
        return signedInSuccessfully;
    }
		
    void SignOut()
    {
        PlayGamesPlatform.Instance.SignOut();
    }

    public void PostHighscore(string leaderboardName, long score)
    {
        string key;
        if (leaderboardKeys.TryGetValue(leaderboardName, out key))
        {
            Social.ReportScore(score, key, HighscorePosted);
        }
    }

    public void ShowAchievementsUI()
    {
		if (Social.localUser.authenticated)
			Social.ShowAchievementsUI ();
		else
			Debug.Log ("not signed in");
    }

    //shows all leaderboards
    public void ShowLeaderboardsUI()
    {
        Social.ShowLeaderboardUI();
    }


    void SignedIn(bool success)
	{
		if (success) 
		{

		}
	}

    void HighscorePosted(bool success)
    {
        if (success)
        {

        }
    }
}
