using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;
//using UnityEngine.UI;
//using GoogleMobileAds;
//using GoogleMobileAds.Api;
////using DG.Tweening;
////using com.adjust.sdk;
////using Firebase.Analytics;
public class AdsNativeController : MonoBehaviour
{
//    public string[] adUnitId;


//    private NativeAd nativeAdPlayer;
//    private bool isNativeAdPlayer1 = true;



//    public TextMeshProUGUI Header;
//    public TextMeshProUGUI Discription;
//    public RawImage BigImage;
//    public RawImage LogoImage;
//    public RawImage CloseImage;
//    public Image Star;
//    public TextMeshProUGUI ButtonText;

//    public GameObject loading;
//    public GameObject loaded;

//    private void OnEnable()
//    {
//        if (PlayerPrefs.GetInt("removeAds", 0) == 1)
//        {
//            gameObject.SetActive(false);
//        }


//        loaded.SetActive(false);
//        loading.SetActive(true);


//        MobileAds.Initialize((InitializationStatus initStatus) =>
//        {
//            InvokeRepeating(nameof(RequestNativeAdPauseFromPlayer), 0f, 60f);
//        });
//    }
//    private void RequestNativeAdPauseFromPlayer()
//    {
//        string adUnitId1 = adUnitId[0];
//        if (isNativeAdPlayer1 == true)
//        {
//            adUnitId1 = adUnitId[0];
//            isNativeAdPlayer1 = false;
//        }
//        else
//        {
//            adUnitId1 = adUnitId[1];
//            isNativeAdPlayer1 = true;
//        }
//        AdLoader adLoader = new AdLoader.Builder(adUnitId1)
//            .ForNativeAd()
//            .Build();
//        adLoader.OnNativeAdLoaded += this.HandleNativeAdLoadedPlayer;
//        adLoader.OnAdFailedToLoad += this.HandleAdFailedToLoadPlayer;

//        adLoader.LoadAd(new AdRequest.Builder().Build());
//    }

//    private void HandleAdFailedToLoadPlayer(object sender, AdFailedToLoadEventArgs args)
//    {
//        loaded.SetActive(false);
//        loading.SetActive(true);
//    }

//    private void HandleNativeAdLoadedPlayer(object sender, NativeAdEventArgs args)
//    {

//        this.nativeAdPlayer = args.nativeAd;

//        //set textes and Details;
//        LogoImage.texture = nativeAdPlayer.GetIconTexture();
//        Header.text = nativeAdPlayer.GetHeadlineText();
//        Discription.text = nativeAdPlayer.GetBodyText();
//        BigImage.texture = nativeAdPlayer.GetImageTextures()[0];
//        ButtonText.text = nativeAdPlayer.GetCallToActionText();
//        CloseImage.texture = nativeAdPlayer.GetAdChoicesLogoTexture();
//        var rateStar = nativeAdPlayer.GetStarRating();
//        Star.fillAmount = (float)rateStar / 5f;
//        if (nativeAdPlayer.GetAdChoicesLogoTexture() == null)
//        {
//            CloseImage.gameObject.SetActive(false);
//        }
//        List<GameObject> PauseGameObjects = new List<GameObject>() { BigImage.gameObject };


//        if (!nativeAdPlayer.RegisterIconImageGameObject(LogoImage.gameObject))
//        {
//            LogoImage.gameObject.SetActive(false);
//        }
//        if (!nativeAdPlayer.RegisterHeadlineTextGameObject(Header.gameObject))
//        {
//            Header.gameObject.SetActive(false);
//        }
//        if (!nativeAdPlayer.RegisterBodyTextGameObject(Discription.gameObject))
//        {
//            Discription.gameObject.SetActive(false);
//        }
//        if (nativeAdPlayer.RegisterImageGameObjects(PauseGameObjects) == 0)
//        {

//        }
//        if (!nativeAdPlayer.RegisterCallToActionGameObject(ButtonText.gameObject))
//        {
//            ButtonText.gameObject.SetActive(false);
//        }
//        if (!nativeAdPlayer.RegisterAdChoicesLogoGameObject(CloseImage.gameObject))
//        {
//            CloseImage.gameObject.SetActive(false);
//        }
//        loaded.SetActive(true);
//        loading.SetActive(false);
//        //FireBaseAnalysticsController.THIS.FireEvent("SHOW_NATIVE_ADS_SUCCESS");
//        //nativeAdPlayer.OnPaidEvent += NativeAd_OnPaidEvent;
//    }

//    //private void NativeAd_OnPaidEvent(object sender, AdValueEventArgs e)
//    //{
//    //    SendDataAdjust(e.AdValue);
//    //}

//    //public void SendDataAdjust(AdValue adValue)
//    //{
//    //    // adjust
//    //    AdjustAdRevenue adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAdMob);
//    //    adRevenue.setRevenue(adValue.Value / 1000000f, adValue.CurrencyCode);
//    //    Adjust.trackAdRevenue(adRevenue);


//    //    Firebase.Analytics.Parameter[] LTVParameters = {
//    //      new Firebase.Analytics.Parameter("ad_platform", "adMob"),
//    //      new Firebase.Analytics.Parameter("ad_source", "adMob"),
//    //      new Firebase.Analytics.Parameter("value", adValue.Value / 1000000f),

//    //      new Firebase.Analytics.Parameter("currency",adValue.CurrencyCode),
//    //      new Firebase.Analytics.Parameter("precision",(int)adValue.Precision),
//    //    };
//    //    FirebaseAnalytics.LogEvent("ad_impression", LTVParameters);
//    //}
}
