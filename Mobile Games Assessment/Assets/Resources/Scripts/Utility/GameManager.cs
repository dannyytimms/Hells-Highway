using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.Advertisements;

///////////////////////////////////////
/////////
////// Author: Daniel Timms (c) 2018.
/////////
/////////////////////////////////////// 

public class GameManager : MonoBehaviour 
{

	//Class references:
	public PlayerScript player; 
	public static GameManager instance = null;

	//Public Statics:
	public static float bulletSpeed = 2;
	public static float bulletDamage = 100.0f;
	public static float zombieSpeed = 0.02f;
	public static float brightness;
	public static float volume;
	public static float minutes;
	public static float seconds;
	public static float hiMins, hiSecs;
	public static int playerStones; 
	public static int hiScoreObject;
	public static int zombieHealth = 100;
	public static int bulletCountL;
	public static int bulletCountR;
	public static int score = 0; 
	public static int coinBalance = 0;
	public static int multiplier = 2;
	public static int gunL;
	public static int gunR;
	public static ulong dailyTimer;
	public static ulong adTimer;
	public static bool useTime;
	public static bool useAds = false;

	//inventory and pricing
	public int riflePrice, pistolPrice, ARPrice;
	public int mechanicBoxPrice, AmmoBoxPrice;
	public static bool pistolUnlock, rifleUnlock, ARUnlock;
	public static int maxMech = 2, maxAmmoBoxes = 2;
	public static int currentMech, currentAmmoBoxes;
	int amount;

	//Public bool:
	public bool spendCheck;
	public bool mute;

	//GameObject References:
	public GameObject checkBox;

	//Private:
	private float time;

	//UI:
	public Toggle muteToggle;
	public AudioListener main;
	public Slider volumeSlider;
	public Slider brightnessSlider;
	Color darkest = Color.black;
	Color lightest = Color.grey;

	void Awake()
	{
		LoadGame ();
		if (PlayerPrefs.GetInt ("muteInt") == 1)
			muteToggle.isOn = true;
		else
			muteToggle.isOn = false;
		mute = muteToggle.isOn;

		volumeSlider.value = PlayerPrefs.GetFloat ("volumeValue");
		brightnessSlider.value = PlayerPrefs.GetFloat ("brightnessValue");


		#if !UNITY_EDITOR
		useAds =  true;
		#endif

		Time.timeScale = 1;
	}
	void Start()
	{
		Scene scene = SceneManager.GetActiveScene ();
		GameObject car = GameObject.Find("Car1");
		GameObject del = GameObject.Find ("Car");

		if (del != null) 
		{
			if (car != null)
//				foreach (Transform child in car.transform) {
//					Destroy (child.gameObject);
//				}
				//car.transform.position = new Vector3 (0, transform.position.y, transform.position.z);
			car.SetActive (false);
		}
	}

	public void StartGame()
	{
		Time.timeScale = 1;
	}

	public void resume()
	{
		if (Time.timeScale == 0)
		{
			Time.timeScale = 1;
		} 
		else if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
		}
	}

	public void pause()
	{
		if (Time.timeScale == 1)
		{
			Time.timeScale = 0;
		}

		else if (Time.timeScale == 0) 
		{
			Time.timeScale = 1;
		}
	}

	public void levelReset()
	{
		if(useAds == true)
		//Advertisement.Show ();
		score = 0;
		SceneManager.LoadScene (0);
	}

	public void LoadScene(int level)
	{
		SceneManager.LoadScene (level);
	}

	public void quitApplication()
	{
		Application.Quit ();
	}

	public static void scoreIncrease()
	{
		score++;
	}
		
	void Update()
	{
		AudioListener.volume = volumeSlider.value;
		volume = volumeSlider.value;
		PlayerPrefs.SetFloat ("volumeValue", volumeSlider.value);

		RenderSettings.ambientLight = Color.Lerp (darkest, lightest, brightnessSlider.value);

		brightness = brightnessSlider.value;
		PlayerPrefs.SetFloat ("brightnessValue", brightnessSlider.value);

		if (muteToggle.isOn == true)
			mute = false;
		else
			mute = true;
		
		if (muteToggle.isOn == true)
			PlayerPrefs.SetInt ("muteInt", 1);
		else
			PlayerPrefs.SetInt ("muteInt", 0);
		main.enabled = mute;

		time += Time.deltaTime;
		minutes = (int)time / 60; //Divide the guiTime by sixty to get the minutes.
		seconds = (int)time % 60;//Use the euclidean division for the seconds.

		if (Time.timeScale == 1) 
		{
			useTime = true;
		} 
		else 
		{
			useTime = false;
		}
	}

	public void ShowAds()
	{
		//if (useAds == true)
		//Advertisement.Show ();
	}
		
	//Gameplay save data
	public void SaveGame()
	{
		if(seconds > hiSecs)
			hiSecs = seconds;

		SerializableConsumables playerConsumables = new SerializableConsumables(coinBalance,hiMins, hiSecs, gunL,gunR,dailyTimer,
			adTimer, useAds, score,pistolUnlock,rifleUnlock, ARUnlock, currentAmmoBoxes,currentMech, playerStones);
		
		GameData gameData = new GameData (playerConsumables);
		BinaryFormatter bf = new BinaryFormatter ();
		File.Delete (Application.persistentDataPath + "/saveGame.txt");
		FileStream file = File.Create (Application.persistentDataPath + "/saveGame.txt");

		bf.Serialize (file, gameData);
		file.Close ();
		Debug.Log ("Saved Successfully");
	}

	public void LoadGame()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Open (Application.persistentDataPath + "/saveGame.txt", FileMode.Open);
		GameData loadedData = (GameData)bf.Deserialize (file);
		file.Close ();

		coinBalance = loadedData.PlayerConsumables.coins;
		hiMins = loadedData.PlayerConsumables.minutes;
		hiSecs = loadedData.PlayerConsumables.seconds;
		gunL = loadedData.PlayerConsumables.gunChoiceL;
		gunR = loadedData.PlayerConsumables.gunChoiceR;
		adTimer = loadedData.PlayerConsumables.advertTimer;
		dailyTimer = loadedData.PlayerConsumables.dailyTimer;
		useAds = loadedData.PlayerConsumables.ads;
		hiScoreObject = loadedData.PlayerConsumables.score;
		pistolUnlock = loadedData.PlayerConsumables.pistol;
		rifleUnlock = loadedData.PlayerConsumables.rifle;
		ARUnlock = loadedData.PlayerConsumables.AR;
		currentAmmoBoxes = loadedData.PlayerConsumables.ammo;
		currentMech = loadedData.PlayerConsumables.mech;
		playerStones = loadedData.PlayerConsumables.stones;

		Debug.Log ("Loaded Successfully!");
	}

//Store purchases
	public void pistol()
	{
		amount = pistolPrice;
		if (pistolUnlock == false)
		{
			if (EnoughCoins (amount)) 
				{
				pistolUnlock = true;
				Spend (pistolPrice);
				}
		}
		else 
		{
			Debug.Log ("Already purchased!");
		}
			
	}
	public void Rifle()
	{
		amount = riflePrice;
		if (rifleUnlock == false) 
		{
			if (EnoughCoins (amount)) 
			{
				rifleUnlock = true;
				Spend (riflePrice);
			} 
		}
		else 
		{
			Debug.Log ("Already purchased!");
		}
	}

	public void AR()
	{
		amount = ARPrice;
		if (ARUnlock == false)
		{
			if (EnoughCoins (amount))
			{
				ARUnlock = true;
				Spend (ARPrice);
			}
		}
		else
		{
			Debug.Log ("Already purchased!");
		}
	}

	public void MechBox()
	{
		if (currentMech < maxMech) 
		{
			amount = mechanicBoxPrice;
			if (EnoughCoins (amount)) 
			{
				currentMech++;
				Debug.Log ("Mechanic Box Purchased!");
				Spend (mechanicBoxPrice);
			}
			else
				return;
		}
		else 
		{
			Debug.Log ("max mech boxes!");
		}
	}

	public void AmmoBox()
	{
		if (currentAmmoBoxes < maxAmmoBoxes) 
		{
			amount = AmmoBoxPrice;
			if (EnoughCoins (amount)) 
			{
				currentAmmoBoxes++;
				Debug.Log ("Ammo Box Purchased!");
				Spend (AmmoBoxPrice);
			} 
			else
				return;
		} 
		else
		{
			Debug.Log ("max ammo boxes!");
		}
	}
		
	public bool EnoughCoins(int amount)
	{
		if (coinBalance >= amount) 
		{
			return true;
		} 
		else 
		{
			Debug.Log ("not enough Coins!");
			return false;
		}
	}

	public void Spend(int Ramount)
	{
		coinBalance -= Ramount;
		checkBox.SetActive (false);
		spendCheck = false;
		SaveGame ();
		LoadGame ();
	}

	public void CallRandom()
	{
		int num = Random.Range (0, 2);
		RandomReward (num);
		RandomQuantity ();
	}

	public static string RandomReward(int num)//not yet finished
	{
		switch (num) 
		{
		case 0: 
			return "Coins";
		case 1:
			return "Ammo Box";
		}
		return null;
	}

	public static int RandomQuantity()//not yet finished
	{
		int num= Random.Range (1, 5);
		return 1;
	}

}
	
public enum InteractableType {POWERUP, CUBE, ENEMY_ZOMBIE, ENEMY_OBSTACLE}

public interface IPlayerInteractable
{
	void Highlight(bool highlighted);
	GameObject InteractBegin();
	GameObject InteractEnd();
	Vector3 GetPosition();
	InteractableType GetInteractableType();
}

[System.Serializable]
public class SerializableConsumables
{
	public int coins;
	public float minutes;
	public float seconds;
	public int gunChoiceL;
	public int gunChoiceR;
	public ulong advertTimer;
	public ulong dailyTimer;
	public bool ads;
	public int score;
	public bool pistol, rifle, AR;
	public int ammo;
	public int mech;
	public int stones;

	public SerializableConsumables(int Scoins, float Sminutes, float Sseconds, int SgunChoiceL,int SgunChoiceR, ulong sAdvertT, ulong sDailyT, bool SuseAds, 
		int Sscore, bool Spistol, bool Srifle, bool SAR, int Sammo, int Smech, int Sstones)
	{
		this.coins = Scoins;
		this.minutes = Sminutes;
		this.seconds = Sseconds;
		this.gunChoiceL = SgunChoiceL;
		this.gunChoiceR = SgunChoiceR;
		this.advertTimer = sAdvertT;
		this.dailyTimer = sDailyT;
		this.ads = SuseAds;
		this.score = Sscore;
		this.pistol = Spistol;
		this.rifle = Srifle;
		this.AR = SAR;
		this.ammo = Sammo;
		this.mech = Smech;
		this.stones = Sstones;
	}
}

	[System.Serializable]
	public class GameData
	{
		public SerializableConsumables PlayerConsumables;
		public GameData(SerializableConsumables pConsumables)
		{
			this.PlayerConsumables = pConsumables;
		}

	}


