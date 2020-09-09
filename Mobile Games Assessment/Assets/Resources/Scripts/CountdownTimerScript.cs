using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
public class CountdownTimerScript : MonoBehaviour
{

	public int msToWait;
	private Text timerText; 

	private Button TimerButton;
	private ulong lastTimerOpen;
	private int freeItemBalance;

	GameManager gameManager = new GameManager();

	void Awake()
	{
		gameManager.LoadGame ();
	}

	private void Start()
	{
		TimerButton = GetComponent<Button> ();
		//lastTimerOpen = GameManager.dailyTimer;

		lastTimerOpen = ulong.Parse(PlayerPrefs.GetString ("LastTimerOpen"));

		timerText = GetComponentInChildren<Text>();
		if(!isReady())
			TimerButton.interactable = false;
	}

	private void Update()
	{
		if (!TimerButton.IsInteractable ()) 
		{
			if (isReady()) 
			{
				TimerButton.interactable = true;
				return;
			}

			//set timer
			ulong diff = ((ulong)DateTime.Now.Ticks - lastTimerOpen);
			ulong m = diff / TimeSpan.TicksPerMillisecond;

			int secondsLeft = (msToWait - (int)m) / 1000;

			string r = "";

			// hours 
			r += ((int)secondsLeft / 3600).ToString() + "h ";
			secondsLeft -= ((int)secondsLeft / 3600) * 3600;
			// minutes
			r += ((int) secondsLeft / 60).ToString("00") + "m ";
			//seconds
			r += (secondsLeft % 60).ToString ("00") + "s ";

			timerText.text = r;

		}
	}

	private bool isReady()
	{
		ulong diff = ((ulong)DateTime.Now.Ticks - lastTimerOpen);
		ulong m = diff / TimeSpan.TicksPerMillisecond;

		int secondsLeft = (msToWait - (int)m) / 1000;
		if (secondsLeft < 0) {
			timerText.text = "Click for random reward!";
			return true;
		}
		return false;
	}

	int randomizer()
	{
		int randomNumber = UnityEngine.Random.Range (0, 4);

		switch (randomNumber) 
		{
		case 0:
			freeItemBalance = 10;
			break;
		case 1:
			freeItemBalance = 20;
			break;
		case 2:
			freeItemBalance = 30;
			break;
		case 3:
			freeItemBalance = 40;
			break;
		case 4:
			freeItemBalance = 50;
			break;
		default: 
			freeItemBalance = 10;
			break;
		}
		return freeItemBalance;
	}

	public void ButtonClick()
	{
		lastTimerOpen = (ulong)DateTime.Now.Ticks;
		PlayerPrefs.SetString("LastTimerOpen",DateTime.Now.Ticks.ToString());
		GameManager.coinBalance += randomizer();
		gameManager.SaveGame();
		TimerButton.interactable = false;
	}
}
