using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;
using GoogleMobileAds.Common;
//using com.adjust.sdk;
//using Firebase.Analytics;
public class AdsOpenAppController : MonoBehaviour
{
    public static AdsOpenAppController THIS;
    private AppOpenAd appOpenAd;
    public string _adUnitId;
    private void Awake()
    {

        if (THIS != null)

        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            THIS = this;
            DontDestroyOnLoad(gameObject);
        }

        //MobileAds.RaiseAdEventsOnUnityMainThread = true;
        AppStateEventNotifier.AppStateChanged += OnAppStateChanged;
        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadAppOpenAd();
        });
    }
    void Start()
    {


    }

    private void OnDestroy()
    {
        AppStateEventNotifier.AppStateChanged -= OnAppStateChanged;
    }

    public void LoadAppOpenAd()
    {
        // Clean up the old ad before loading a new one.
        if (appOpenAd != null)
        {
            appOpenAd.Destroy();
            appOpenAd = null;
        }

        Debug.Log("Loading the app open ad.");


        var adRequest = new AdRequest.Builder().Build();

        // send the request to load the ad.
        AppOpenAd.Load(_adUnitId, ScreenOrientation.Portrait, adRequest,
            (AppOpenAd ad, LoadAdError error) =>
            {
                // if error is not null, the load request failed.
                if (error != null || ad == null)
                {
                    Debug.LogError("app open ad failed to load an ad " +
                                   "with error : " + error);
                    return;
                }

                Debug.Log("App open ad loaded with response : "
                          + ad.GetResponseInfo());

                appOpenAd = ad;
                RegisterEventHandlers(ad);
            });
    }

    private void RegisterEventHandlers(AppOpenAd ad)
    {
        // Raised when the ad is estimated to have earned money.
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(string.Format("App open ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));

            // SendDataAdjust(adValue);
        };
        // Raised when an impression is recorded for an ad.
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("App open ad recorded an impression.");
        };
        // Raised when a click is recorded for an ad.
        ad.OnAdClicked += () =>
        {
            Debug.Log("App open ad was clicked.");
        };
        // Raised when an ad opened full screen content.
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("App open ad full screen content opened.");
        };
        // Raised when the ad closed full screen content.
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("App open ad full screen content closed.");
            //LoadingScreenController.THIS.FinishedSlider();
        };
        // Raised when the ad failed to open full screen content.
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("App open ad failed to open full screen content " +
                           "with error : " + error);
            //LoadAppOpenAd();

        };
    }

    private void OnAppStateChanged(AppState state)
    {
        Debug.Log("App State changed to : " + state);

        // if the app is Foregrounded and the ad is available, show it.
        if (state == AppState.Foreground)
        {
            StartCoroutine(ShowAppOpenAdWithDelay(0.5f));
        }
    }

    public void OnShowAppWithDelay(float time)
    {
        StartCoroutine(ShowAppOpenAdWithDelay(time));
    }
    IEnumerator ShowAppOpenAdWithDelay(float time)
    {
        yield return new WaitForSeconds(time);
        ShowAppOpenAd();
    }

    public void ShowAppOpenAd()
    {
        if (PlayerPrefs.GetInt("removeAds", 0) == 1)
        {
            return;
        }

        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            Debug.Log("Showing app open ad.");
            appOpenAd.Show();
            // FireBaseAnalysticsController.THIS.FireEvent("Open_ADS");
            //FireBaseAnalysticsController.THIS.FireEvent("SHOW_OPEN_ADS_SUCCESS");
        }
        else
        {
            LoadAppOpenAd();
            Debug.Log("App open ad is not ready yet123.");
        }


    }

    //void SendDataAdjust(AdValue adValue)
    //{
    //    // adjust
    //    AdjustAdRevenue adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAdMob);
    //    adRevenue.setRevenue(adValue.Value / 1000000f, adValue.CurrencyCode);
    //    Adjust.trackAdRevenue(adRevenue);


    //    Firebase.Analytics.Parameter[] LTVParameters = {
    //      new Firebase.Analytics.Parameter("ad_platform", "adMob"),
    //      new Firebase.Analytics.Parameter("ad_source", "adMob"),
    //      new Firebase.Analytics.Parameter("value", adValue.Value / 1000000f),

    //      new Firebase.Analytics.Parameter("currency",adValue.CurrencyCode),
    //      new Firebase.Analytics.Parameter("precision",(int)adValue.Precision),
    //    };
    //    FirebaseAnalytics.LogEvent("ad_impression", LTVParameters);

    //}
}
