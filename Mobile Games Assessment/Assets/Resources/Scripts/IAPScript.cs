//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Purchasing;

//// Placing the Purchaser class in the CompleteProject namespace allows it to interact with ScoreManager, 
//// one of the existing Survival Shooter scripts.


//	// Deriving the Purchaser class from IStoreListener enables it to receive messages from Unity Purchasing.
//public class IAPScript : MonoBehaviour, IStoreListener
//{
//GameManager gameManager = new GameManager();

//	public static IAPScript instance{ get; set; }
//	private static IStoreController m_StoreController;          // The Unity Purchasing system.
//	private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

//	public static string COINS_100 = "coins100"; 
//	public static string COINS_250 = "coins250";
//	public static string COINS_500 = "coins500";

//	public static string ADS_REMOVE = "removeads";

//	public static string kProductIDSubscription =  "subscription"; 

//	// Google Play Store-specific product identifier subscription product.
//	private static string kProductNameGooglePlaySubscription =  "com.unity3d.subscription.original"; 

//	void Awake()
//	{
//		instance = this;
//	}

//	void Start()
//	{
//		// If we haven't set up the Unity Purchasing reference
//		if (m_StoreController == null)
//		{
//			// Begin to configure our connection to Purchasing
//			InitializePurchasing();
//		}
//	}

//	public void InitializePurchasing() 
//	{
//		// If we have already connected to Purchasing ...
//		if (IsInitialized())
//		{
//			// ... we are done here.
//			return;
//		}

//		// Create a builder, first passing in a suite of Unity provided stores.
//		var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

//		// Add a product to sell / restore by way of its identifier, associating the general identifier
//		// with its store-specific identifiers.
//		builder.AddProduct(COINS_100, ProductType.Consumable);
//		builder.AddProduct(COINS_250, ProductType.Consumable);
//		builder.AddProduct(COINS_500, ProductType.Consumable);

//		// Continue adding the non-consumable product.
//		builder.AddProduct(ADS_REMOVE, ProductType.NonConsumable);

//		builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs(){
//			{ kProductNameGooglePlaySubscription, GooglePlay.Name },
//		});

//		// Kick off the remainder of the set-up with an asynchrounous call, passing the configuration 
//		// and this class' instance. Expect a response either in OnInitialized or OnInitializeFailed.
//		UnityPurchasing.Initialize(this, builder);
//	}


//	private bool IsInitialized()
//	{
//		// Only say we are initialized if both the Purchasing references are set.
//		return m_StoreController != null && m_StoreExtensionProvider != null;
//	}


//	public void BUY_100_COINS()
//	{
//		BuyProductID(COINS_100);
//	}

//	public void BUY_250_COINS()
//	{
//	BuyProductID(COINS_250);
//	}

//	public void BUY_500_COINS()
//	{
//	BuyProductID(COINS_500);
//	}
	
//	public void BUY_AD_REMOVE()
//	{
//		if(GameManager.useAds == true)
//	BuyProductID(ADS_REMOVE);
//		else
//			Debug.Log ("NO ADS TO REMOVE!");
//	}
		
//	void BuyProductID(string productId)
//	{
//		// If Purchasing has been initialized ...
//		if (IsInitialized())
//		{
//			// ... look up the Product reference with the general product identifier and the Purchasing 
//			// system's products collection.
//			Product product = m_StoreController.products.WithID(productId);

//			// If the look up found a product for this device's store and that product is ready to be sold ... 
//			if (product != null && product.availableToPurchase)
//			{
//				Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
//				// ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
//				// asynchronously.
//				m_StoreController.InitiatePurchase(product);
//			}
//			// Otherwise ...
//			else
//			{
//				// ... report the product look-up failure situation  
//				Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//			}
//		}
//		// Otherwise ...
//		else
//		{
//			// ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
//			// retrying initiailization.
//			Debug.Log("BuyProductID FAIL. Not initialized.");
//		}
//	}




//	public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//	{
//		// Purchasing has succeeded initializing. Collect our Purchasing references.
//		Debug.Log("OnInitialized: PASS");

//		// Overall Purchasing system, configured with products for this application.
//		m_StoreController = controller;
//		// Store specific subsystem, for accessing device-specific store features.
//		m_StoreExtensionProvider = extensions;
//	}


//	public void OnInitializeFailed(InitializationFailureReason error)
//	{
//		// Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
//		Debug.Log("OnInitializeFailed InitializationFailureReason:" + error);
//	}


//	public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args) 
//	{
//		// A consumable product has been purchased by this user.
//	if (String.Equals(args.purchasedProduct.definition.id, COINS_250, StringComparison.Ordinal))
//		{
//		Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//		GameManager.coinBalance += 250;
//		gameManager.SaveGame ();
//		gameManager.LoadGame ();
//		}
//	else if (String.Equals(args.purchasedProduct.definition.id, COINS_500, StringComparison.Ordinal))
//	{
//		Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//		GameManager.coinBalance += 500;
//		gameManager.SaveGame ();
//		gameManager.LoadGame ();
//	}
//	else if (String.Equals(args.purchasedProduct.definition.id, COINS_100, StringComparison.Ordinal))
//	{
//		Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//		GameManager.coinBalance += 100;
//		gameManager.SaveGame ();
//		gameManager.LoadGame ();
//	}
//		// Or ... a non-consumable product has been purchased by this user.
//	else if (String.Equals(args.purchasedProduct.definition.id, ADS_REMOVE, StringComparison.Ordinal))
//	{
//				Debug.Log (string.Format ("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//				GameManager.useAds = false;
//				gameManager.SaveGame ();
//				gameManager.LoadGame ();		
//	}


//		// Or ... a subscription product has been purchased by this user.
//		else if (String.Equals(args.purchasedProduct.definition.id, kProductIDSubscription, StringComparison.Ordinal))
//		{
//			Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));
//			// TODO: The subscription item has been successfully purchased, grant this to the player.
//		}
//		// Or ... an unknown product has been purchased by this user. Fill in additional products here....
//		else 
//		{
//			Debug.Log(string.Format("ProcessPurchase: FAIL. Unrecognized product: '{0}'", args.purchasedProduct.definition.id));
//		}

//		// Return a flag indicating whether this product has completely been received, or if the application needs 
//		// to be reminded of this purchase at next app launch. Use PurchaseProcessingResult.Pending when still 
//		// saving purchased products to the cloud, and when that save is delayed. 
//		return PurchaseProcessingResult.Complete;
//	}


//	public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
//	{
//		// A product purchase attempt did not succeed. Check failureReason for more detail. Consider sharing 
//		// this reason with the user to guide their troubleshooting actions.
//		Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
//	}
//}
