//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using UnityEngine.UI;
//using UnityEngine.Advertisements;

//public class FreeStuffSpamBlocker : MonoBehaviour
//{

//	public float msToWait;
//	private Text timerText; 

//	private Button TimerButton;
//	private ulong lastRewardVideo;

//	public string placementId = "rewardedVideo";

//	GameManager gameManager = new GameManager();

//	void Awake()
//	{
//		gameManager.LoadGame ();
//		//Debug.Log (GameManager.adTimer);
//	}
//	private void Start()
//	{
//		string gameId = "1706312";

//		TimerButton = GetComponent<Button> ();
//		//lastRewardVideo = GameManager.adTimer;
//		lastRewardVideo = ulong.Parse(PlayerPrefs.GetString ("LastRewardVideo"));
//		timerText = GetComponentInChildren<Text>();
//		if(!isReady())
//			TimerButton.interactable = false;

//		if (Advertisement.isSupported) {
//			Advertisement.Initialize (gameId, true);
//		}
//	}
//	private void Update()
//	{
//		if (!TimerButton.IsInteractable ()) 
//		{
//			if (isReady()) {
//				TimerButton.interactable = true;

//				//return;
//			}

//			//set timer
//			ulong diff = ((ulong)DateTime.Now.Ticks - lastRewardVideo);
//			ulong m = diff / TimeSpan.TicksPerMillisecond;

//			float secondsLeft = (float)(msToWait - m) / 1000.0f;

//			string r = "";

//			// hours 
//			//r += ((int)secondsLeft / 3600).ToString() + "h ";
//		//	secondsLeft -= ((int)secondsLeft / 3600) * 3600;

//			// minutes
//			r += ((int) secondsLeft / 60).ToString("00") + "m ";

//			r += (secondsLeft % 60).ToString ("00") + "s ";

//			timerText.text = r;

//		}
//	}

//	private bool isReady()
//	{
//		ulong diff = ((ulong)DateTime.Now.Ticks - lastRewardVideo);
//		ulong m = diff / TimeSpan.TicksPerMillisecond;

//		float secondsLeft = (float)(msToWait - m) / 1000.0f;

//		if (secondsLeft < 0) {
//			timerText.text = "Watch ads for 50 coins!";
//			return true;
//		}
//		return false;
//	}

//	public void ButtonClick()
//	{
//		if (Advertisement.IsReady (placementId)) {
			
//			lastRewardVideo = (ulong)DateTime.Now.Ticks;
//			PlayerPrefs.SetString ("LastRewardVideo", DateTime.Now.Ticks.ToString ());

//			//GameManager.adTimer = (ulong)DateTime.Now.Ticks;//lastRewardVideo;
//			//gameManager.SaveGame ();
//			TimerButton.interactable = false;
//			Debug.Log (GameManager.adTimer);
//			ShowAd ();
//		} else
//			return;
//	}

//	void ShowAd ()
//	{
//		ShowOptions options = new ShowOptions();
//		options.resultCallback = HandleShowResult;

//		Advertisement.Show(placementId, options);
//	}

//	void HandleShowResult (ShowResult result)
//	{
//		if(result == ShowResult.Finished) {
//			GameManager.coinBalance += 50;
//			gameManager.SaveGame ();

//		}else if(result == ShowResult.Skipped) {
//			Debug.LogWarning("Video was skipped - Do NOT reward the player");

//		}else if(result == ShowResult.Failed) {
//			Debug.LogError("Video failed to show");
//		}
//	}

//}
