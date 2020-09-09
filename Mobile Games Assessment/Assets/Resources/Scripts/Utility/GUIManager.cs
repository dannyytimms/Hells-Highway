using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

///////////////////////////////////////
/////////
////// Author: Daniel Timms (c) 2018.
/////////
/////////////////////////////////////// 

public class GUIManager : MonoBehaviour 
{

	public Text scoreT;
	public Text bulletTR;
	public Text bulletT;
	public Text bulletReserves;
	public Text bulletReservesR;
	public Text timerLabel;
	public Text storeCoinBalanceText;
	public Text hiScore;
	public Text hiScoreTime;
	public Text dieMenuTime;
	public Text dieMenuScore;
	public Text consumableAmmoBoxText;
	public Text stonesBalance;
	public Text ammoBoxBalance;
	public Text mechBoxBalance;
	public Text rewardText;
	public Text quantityText;

	#region singleton code
	public static GUIManager instance = null;

	void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}

		Scene scene = SceneManager.GetActiveScene ();

		if (scene.name != "Main") 
		{
			hiScore = null;
			hiScoreTime = null;
			storeCoinBalanceText = null;
			stonesBalance = null;
			mechBoxBalance = null;
			ammoBoxBalance = null;
			rewardText = null;
			quantityText = null;
		} 
		else if (scene.name != "StandardGameMode" || scene.name != "NightmareMode") 
		{
			scoreT = null;
			bulletT = null;
			bulletTR = null;
			bulletReserves = null; 
			bulletReservesR = null;
			timerLabel = null;
			dieMenuTime = null;
			dieMenuScore = null;
			consumableAmmoBoxText = null;

		}

	}
	#endregion

	public void UpdateGUI()
	{
		if(scoreT != null)
		scoreT.text = GameManager.score.ToString();
		if(dieMenuScore != null)
		dieMenuScore.text = GameManager.score.ToString();
		if(bulletT != null)
		bulletT.text = gunFire.magazine.ToString() + "/";
		if(bulletTR != null)
		bulletTR.text = gunFire.magazineR.ToString()+ "/";

		if(bulletReservesR  != null)
		bulletReservesR.text = gunFire.reservesR.ToString ();
		if(bulletReserves != null)
		bulletReserves.text = gunFire.reserves.ToString ();
		if(hiScore != null)
		hiScore.text = GameManager.hiScoreObject.ToString ();
		if(storeCoinBalanceText != null)
		storeCoinBalanceText.text = GameManager.coinBalance.ToString ();
		if(timerLabel != null)
			timerLabel.text = string.Format ("{0:00} : {1:00}", (int)GameManager.minutes, (int)GameManager.seconds);
		if(hiScoreTime != null)
			hiScoreTime.text = string.Format ("{0:00}  {1:00}", (int)GameManager.hiMins, (int)GameManager.hiSecs);
		if(dieMenuTime != null)
			dieMenuTime.text = string.Format ("{0:00}  {1:00}", (int)GameManager.hiMins,(int) GameManager.hiSecs);
		if(consumableAmmoBoxText != null)
			consumableAmmoBoxText.text = GameManager.currentAmmoBoxes.ToString ();
		if(stonesBalance != null)
			stonesBalance.text = GameManager.playerStones.ToString ();
		if(mechBoxBalance != null)
			mechBoxBalance.text = GameManager.currentMech.ToString ();
		if(ammoBoxBalance != null)
			ammoBoxBalance.text = GameManager.currentAmmoBoxes.ToString ();
		if (rewardText != null)
			rewardText.text = GameManager.RandomReward (1);
		if (quantityText != null)
			quantityText.text = GameManager.RandomQuantity ().ToString();
	}

	void Update()
	{
		UpdateGUI ();
	}

}
