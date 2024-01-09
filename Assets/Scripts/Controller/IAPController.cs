using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class IAPController : MonoBehaviour, IStoreListener
{
    public static IAPController inst;
    private static IStoreController storeController;
    private static IExtensionProvider extensionProvider;

    public const string productId_MM_1 = "pack01";
    public const string productId_MM_2 = "pack02";
    public const string productId_MM_3 = "pack03";
    public const string productId_MM_4 = "minipack0";
    public const string productId_MM_5 = "minipack1";
    public const string productId_MM_6 = "minipack2";
    public const string productId_MM_7 = "minipack3";
    public const string productId_MM_9 = "ruby0";
    public const string productId_MM_10 = "ruby1";
    public const string productId_MM_11 = "ruby2";
    public const string productId_MM_12 = "ruby3";
    public const string productId_MM_13 = "ruby4";
    public const string productId_MM_14 = "ruby5";
    public const string productId_MM_15 = "vippass";
    public const string productId_MM_16 = "event";

    private void Awake()
    {
        if (inst == null)
        { inst = this; }
        else
        {
            if (inst != this)
            { Destroy(gameObject); }
        }
        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        if (storeController == null)
        {
            InitializePurchasing();
        }
    }

    private bool IsInitialized()
    {
        return (storeController != null && extensionProvider != null);
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
            return;

        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        //var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());


        builder.AddProduct(productId_MM_1, ProductType.NonConsumable, new IDs { { productId_MM_1, GooglePlay.Name } });
        builder.AddProduct(productId_MM_2, ProductType.NonConsumable, new IDs { { productId_MM_2, GooglePlay.Name } });
        builder.AddProduct(productId_MM_3, ProductType.NonConsumable, new IDs { { productId_MM_3, GooglePlay.Name } });
        builder.AddProduct(productId_MM_4, ProductType.NonConsumable, new IDs { { productId_MM_4, GooglePlay.Name } });
        builder.AddProduct(productId_MM_5, ProductType.NonConsumable, new IDs { { productId_MM_5, GooglePlay.Name } });
        builder.AddProduct(productId_MM_6, ProductType.NonConsumable, new IDs { { productId_MM_6, GooglePlay.Name } });
        builder.AddProduct(productId_MM_7, ProductType.NonConsumable, new IDs { { productId_MM_7, GooglePlay.Name } });

        builder.AddProduct(productId_MM_9, ProductType.Consumable, new IDs { { productId_MM_9, GooglePlay.Name } });
        builder.AddProduct(productId_MM_10, ProductType.Consumable, new IDs { { productId_MM_10, GooglePlay.Name } });
        builder.AddProduct(productId_MM_11, ProductType.Consumable, new IDs { { productId_MM_11, GooglePlay.Name } });
        builder.AddProduct(productId_MM_12, ProductType.Consumable, new IDs { { productId_MM_12, GooglePlay.Name } });
        builder.AddProduct(productId_MM_13, ProductType.Consumable, new IDs { { productId_MM_13, GooglePlay.Name } });
        builder.AddProduct(productId_MM_14, ProductType.Consumable, new IDs { { productId_MM_14, GooglePlay.Name } });
        builder.AddProduct(productId_MM_15, ProductType.NonConsumable, new IDs { { productId_MM_15, GooglePlay.Name } });
        builder.AddProduct(productId_MM_16, ProductType.NonConsumable, new IDs { { productId_MM_16, GooglePlay.Name } });

        UnityPurchasing.Initialize(this, builder);
    }

    public void BuyProduct(int numb)
    {
        if (numb.Equals(0))
        { BuyProductID(productId_MM_1); }
        else if (numb.Equals(1))
        { BuyProductID(productId_MM_2); }
        else if (numb.Equals(2))
        { BuyProductID(productId_MM_3); }
        else if (numb.Equals(3))
        { BuyProductID(productId_MM_4); }
        else if (numb.Equals(4))
        { BuyProductID(productId_MM_5); }
        else if (numb.Equals(5))
        { BuyProductID(productId_MM_6); }
        else if (numb.Equals(6))
        { BuyProductID(productId_MM_7); }

        else if (numb.Equals(8))
        { BuyProductID(productId_MM_9); }
        else if (numb.Equals(9))
        { BuyProductID(productId_MM_10); }
        else if (numb.Equals(10))
        { BuyProductID(productId_MM_11); }
        else if (numb.Equals(11))
        { BuyProductID(productId_MM_12); }
        else if (numb.Equals(12))
        { BuyProductID(productId_MM_13); }
        else if (numb.Equals(13))
        { BuyProductID(productId_MM_14); }
        else if (numb.Equals(14))
        { BuyProductID(productId_MM_15); }
        else if (numb.Equals(15))
        { BuyProductID(productId_MM_16); }
    }

    private void BuyProductID(string productId)
    {
        try
        {
            if (IsInitialized())
            {
                Product p = storeController.products.WithID(productId);

                if (p != null && p.availableToPurchase)
                {
                    Debug.Log(string.Format("Purchasing product asychronously: '{0}'", p.definition.id));
                    storeController.InitiatePurchase(p);
                }
                else
                { Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase"); }
            }
            else
            { Debug.Log("BuyProductID FAIL. Not initialized."); }
        }
        catch (Exception e)
        { Debug.Log("BuyProductID: FAIL. Exception during purchase. " + e); }
    }

    public void RestorePurchase()
    {
        if (!IsInitialized())
        {
            Debug.Log("RestorePurchases FAIL. Not initialized.");
            return;
        }

        if (Application.platform == RuntimePlatform.IPhonePlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            Debug.Log("RestorePurchases started ...");

            var apple = extensionProvider.GetExtension<IAppleExtensions>();

            apple.RestoreTransactions
                (
                    (result) => { Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore."); }
                );
        }
        else
        { Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform); }
    }

    public void OnInitialized(IStoreController sc, IExtensionProvider ep)
    {
        Debug.Log("OnInitialized : PASS");

        storeController = sc;
        extensionProvider = ep;
    }

    public void OnInitializeFailed(InitializationFailureReason reason)
    { Debug.Log("OnInitializeFailed InitializationFailureReason:" + reason); }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        Debug.Log(string.Format("ProcessPurchase: PASS. Product: '{0}'", args.purchasedProduct.definition.id));

        switch (args.purchasedProduct.definition.id)
        {
            case productId_MM_1:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                MainSceneController.inst.UpdateGold(100000);
                MainSceneController.inst.UpdateIron(300);
                MainSceneController.inst.UpdateRuby(200);
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;
            case productId_MM_2:

                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                MainSceneController.inst.UpdateGold(1000000);
                MainSceneController.inst.UpdateIron(1000);
                MainSceneController.inst.UpdateRuby(750);
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_3:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                MainSceneController.inst.UpdateGold(50000000);
                MainSceneController.inst.UpdateIron(3500);
                MainSceneController.inst.UpdateRuby(3000);
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_4:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                DataController.inst.IAPAD = true;
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_5:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                DataController.inst.IAPAt = true;
                DataController.inst.es3.Sync();
                PlayerController.inst.SetAbility();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_6:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                DataController.inst.IAPHp = true;
                DataController.inst.es3.Sync();
                PlayerController.inst.SetAbility();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_7:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                DataController.inst.IAPCri = true;
                DataController.inst.es3.Sync();
                PlayerController.inst.SetAbility();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_9:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                MainSceneController.inst.UpdateRuby(100);
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_10:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                MainSceneController.inst.UpdateRuby(250);
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_11:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                MainSceneController.inst.UpdateRuby(650);
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_12:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                MainSceneController.inst.UpdateRuby(1600);
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_13:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                MainSceneController.inst.UpdateRuby(3000);
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_14:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                MainSceneController.inst.UpdateRuby(6000);
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_15:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                DataController.inst.VipPass = true;
                AwardController.inst.UpdateVip();
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;

            case productId_MM_16:
                SoundController.inst.UISound(2);
                DataController.inst.C_IAP++;
                DataController.inst.IAPEvent = true;
                for (int i = 0; i < 5; i++)
                { ItemSlotController.inst.LockOpen(); }
                DataController.inst.es3.Sync();
                ErrorSign.inst.SimpleSignSet(Language.inst.strArray[55], 50);
                break;
                //}
                //else
                //{
                //    Debug.Log("BuyProductID: FAIL.");
                //}
        }

        return PurchaseProcessingResult.Complete;
    }


    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }
}
