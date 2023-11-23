using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using com.adjust.sdk;
//using Firebase.Analytics;
public class AdsIronSourceController : MonoBehaviour
{
    public static AdsIronSourceController THIS;

    public int TypeReward;
    public int timeInterAds = 10;
    private bool isMRKAPK = false;
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
        Debug.unityLogger.logEnabled = true;
    }

    void Start()
    {
#if UNITY_ANDROID
        string appKey = "1b65640f5";
        //        string appKey = "1b65640f5";
        //  string appKey = "85460dcd";
#elif UNITY_IPHONE
        string appKey = "8545d445";
#else
        string appKey = "unexpected_platform";
#endif
        //Call In Once

        IronSource.Agent.validateIntegration();
        IronSource.Agent.init(appKey);

        //LoadInterstitiaAd();
        //IronSource.Agent.loadRewardedVideo();
        //ShowBanner();
        //StartCount();

        IronSourceEvents.onSdkInitializationCompletedEvent += SdkInitializationCompletedEvent;


        IronSourceBannerEvents.onAdLoadedEvent += BannerOnAdLoadedEvent;
        IronSourceBannerEvents.onAdLoadFailedEvent += BannerOnAdLoadFailedEvent;
        IronSourceBannerEvents.onAdClickedEvent += BannerOnAdClickedEvent;
        IronSourceBannerEvents.onAdScreenPresentedEvent += BannerOnAdScreenPresentedEvent;
        IronSourceBannerEvents.onAdScreenDismissedEvent += BannerOnAdScreenDismissedEvent;
        IronSourceBannerEvents.onAdLeftApplicationEvent += BannerOnAdLeftApplicationEvent;


        //Add AdInfo Interstitial Events
        IronSourceInterstitialEvents.onAdReadyEvent += InterstitialOnAdReadyEvent;
        IronSourceInterstitialEvents.onAdLoadFailedEvent += InterstitialOnAdLoadFailed;
        IronSourceInterstitialEvents.onAdOpenedEvent += InterstitialOnAdOpenedEvent;
        IronSourceInterstitialEvents.onAdClickedEvent += InterstitialOnAdClickedEvent;
        IronSourceInterstitialEvents.onAdShowSucceededEvent += InterstitialOnAdShowSucceededEvent;
        IronSourceInterstitialEvents.onAdShowFailedEvent += InterstitialOnAdShowFailedEvent;
        IronSourceInterstitialEvents.onAdClosedEvent += InterstitialOnAdClosedEvent;

        //Add AdInfo Rewarded Video Events
        IronSourceRewardedVideoEvents.onAdOpenedEvent += RewardedVideoOnAdOpenedEvent;
        IronSourceRewardedVideoEvents.onAdClosedEvent += RewardedVideoOnAdClosedEvent;
        IronSourceRewardedVideoEvents.onAdAvailableEvent += RewardedVideoOnAdAvailable;
        IronSourceRewardedVideoEvents.onAdUnavailableEvent += RewardedVideoOnAdUnavailable;
        IronSourceRewardedVideoEvents.onAdShowFailedEvent += RewardedVideoOnAdShowFailedEvent;
        IronSourceRewardedVideoEvents.onAdRewardedEvent += RewardedVideoOnAdRewardedEvent;
        IronSourceRewardedVideoEvents.onAdClickedEvent += RewardedVideoOnAdClickedEvent;


        //IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;
    }

    //private void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
    //{
    //    string allData = impressionData.allData;
    //    string adNetwork = impressionData.adNetwork;
    //    double? revenue = impressionData.revenue;

    //    Debug.Log("unity-script:  ImpressionSuccessEvent impressionData = " + impressionData);
    //    if (impressionData != null)
    //    {

    //        Firebase.Analytics.Parameter[] AdParameters = {
    //             new Firebase.Analytics.Parameter("ad_platform", "ironSource"),
    //              new Firebase.Analytics.Parameter("ad_source", impressionData.adNetwork),
    //              new Firebase.Analytics.Parameter("ad_unit_name", impressionData.adUnit),
    //            new Firebase.Analytics.Parameter("ad_format", impressionData.instanceName),
    //              new Firebase.Analytics.Parameter("currency","USD"),
    //            new Firebase.Analytics.Parameter("value", (double)impressionData.revenue)
    //        };
    //        //FireBaseAnalysticsController.THIS.FireEvent("ad_impression"+ AdParameters);
    //        FirebaseAnalytics.LogEvent("ad_impression", AdParameters);
    //        CallAdjustAdEvent(impressionData);
    //    }
    //}

    //public void CallAdjustAdEvent(IronSourceImpressionData impressionData)
    //{
    //    string allData = impressionData.allData;
    //    string adNetwork = impressionData.adNetwork;
    //    double revenue = (double)impressionData.revenue;

    //    AdjustAdRevenue adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceIronSource);
    //    adRevenue.setRevenue(revenue, "USD");
    //    adRevenue.setAdRevenueNetwork(adNetwork);
    //    adRevenue.setAdRevenueUnit(impressionData.adUnit);
    //    adRevenue.setAdRevenuePlacement(impressionData.placement);
    //    Adjust.trackAdRevenue(adRevenue);
    //}

    #region Check Internet
    bool isConnect;
    public bool CheckInternet()
    {
        isConnect = Application.internetReachability != NetworkReachability.NotReachable;
        return isConnect;

    }
    #endregion


    public void StartCount()
    {
        InvokeRepeating(nameof(TimeCount), 1, 1);
    }
    public void TimeCount()
    {
        timeInterAds--;
    }

    public void ResetTimeCount()
    {
        //timeInterAds = FireBaseRemoteConfig.THIS.AdsInterDelay;
        timeInterAds = 20;
    }

    void OnApplicationPause(bool isPaused)
    {
        IronSource.Agent.onApplicationPause(isPaused);
    }

    public void ShowRewardAds()
    {
        if (isMRKAPK)
        {

        }
        else
        {
            if (IronSource.Agent.isRewardedVideoAvailable())
            {
                IronSource.Agent.showRewardedVideo();
                //FireBaseAnalysticsController.THIS.FireEvent("REWARD_ADS_SHOW");
                ResetTimeCount();
            }
            else
            {

                IronSource.Agent.loadRewardedVideo();
                Debug.Log("Reward Video is Not ready");
                ComonPopUpController.THIS.Container.SetActive(true);
                ComonPopUpController.THIS.inforText.text = "Ads is not ready \n Please try next time";
                if (TypeReward == 6)
                {
                    YesNoPlayMananger.THIS.ClearOneItemShowPopup();
                    YesNoPlayMananger.THIS.RandomSkinInSideBuble();
                }
                //GameManager.THIS.RepondAdsCanvas.gameObject.SetActive(true);
            }
        }

    }

    public void ShowBanner()
    {
        if (isMRKAPK)
        {
            return;
        }
        if (PlayerPrefs.GetInt("removeAds", 0) == 1)
        {
            return;
        }
        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
        //FireBaseAnalysticsController.THIS.FireEvent("BANNER_ADS");
    }

    public void DestroyBanner()
    {
        IronSource.Agent.destroyBanner();
    }

    public void LoadInterstitiaAd()
    {
        IronSource.Agent.loadInterstitial();
    }



    public void ShowInterstitialAds()
    {
        if (isMRKAPK)
        {
            return;
        }
        //Debug.LogError("12222222222222222222222222222222222222222222");
        if (PlayerPrefs.GetInt("removeAds", 0) == 1)
        {
            //Debug.LogError("2111111111111111111111111");
            return;
        }

        if (IronSource.Agent.isInterstitialReady())
        {
            //Debug.LogError("1333333333333333333");
            if (timeInterAds <= 0)
            {
                //Debug.LogError("144444444444");
                IronSource.Agent.showInterstitial();
                Debug.LogError("unity-script: IronSource.Agent.isInterstitialReady - true");
                // FireBaseAnalysticsController.THIS.FireEvent("INTERSTITIAL_ADS");
                ResetTimeCount();
            }

        }
        else
        {
            IronSource.Agent.loadInterstitial();
            Debug.LogError("unity-script: IronSource.Agent.isInterstitialReady - False");

        }


    }

    private void SdkInitializationCompletedEvent()
    {
        LoadInterstitiaAd();
        ShowBanner();
        StartCount();
    }

    //Do BannerCall 

    /************* Banner AdInfo Delegates *************/
    //Invoked once the banner has loaded
    void BannerOnAdLoadedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Invoked when the banner loading process has failed.
    void BannerOnAdLoadFailedEvent(IronSourceError ironSourceError)
    {
    }
    // Invoked when end user clicks on the banner ad
    void BannerOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presentation of a full screen content following user click
    void BannerOnAdScreenPresentedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Notifies the presented screen has been dismissed
    void BannerOnAdScreenDismissedEvent(IronSourceAdInfo adInfo)
    {
    }
    //Invoked when the user leaves the app
    void BannerOnAdLeftApplicationEvent(IronSourceAdInfo adInfo)
    {
    }
    //FullSize ads
    /************* Interstitial AdInfo Delegates *************/
    // Invoked when the interstitial ad was loaded succesfully.
    void InterstitialOnAdReadyEvent(IronSourceAdInfo adInfo)
    {
    }
    // Invoked when the initialization process has failed.
    void InterstitialOnAdLoadFailed(IronSourceError ironSourceError)
    {
    }
    // Invoked when the Interstitial Ad Unit has opened. This is the impression indication. 
    void InterstitialOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
    }
    // Invoked when end user clicked on the interstitial ad
    void InterstitialOnAdClickedEvent(IronSourceAdInfo adInfo)
    {
    }
    // Invoked when the ad failed to show.
    void InterstitialOnAdShowFailedEvent(IronSourceError ironSourceError, IronSourceAdInfo adInfo)
    {
    }
    // Invoked when the interstitial ad closed and the user went back to the application screen.
    void InterstitialOnAdClosedEvent(IronSourceAdInfo adInfo)
    {


    }
    // Invoked before the interstitial ad was opened, and before the InterstitialOnAdOpenedEvent is reported.
    // This callback is not supported by all networks, and we recommend using it only if  
    // it's supported by all networks you included in your build. 
    void InterstitialOnAdShowSucceededEvent(IronSourceAdInfo adInfo)
    {
    }

    //Reward ads


    /************* RewardedVideo AdInfo Delegates *************/
    // Indicates that there’s an available ad.
    // The adInfo object includes information about the ad that was loaded successfully
    // This replaces the RewardedVideoAvailabilityChangedEvent(true) event
    void RewardedVideoOnAdAvailable(IronSourceAdInfo adInfo)
    {
    }
    // Indicates that no ads are available to be displayed
    // This replaces the RewardedVideoAvailabilityChangedEvent(false) event
    void RewardedVideoOnAdUnavailable()
    {
    }
    // The Rewarded Video ad view has opened. Your activity will loose focus.
    void RewardedVideoOnAdOpenedEvent(IronSourceAdInfo adInfo)
    {
    }
    // The Rewarded Video ad view is about to be closed. Your activity will regain its focus.
    void RewardedVideoOnAdClosedEvent(IronSourceAdInfo adInfo)
    {

    }
    // The user completed to watch the video, and should be rewarded.
    // The placement parameter will include the reward data.
    // When using server-to-server callbacks, you may ignore this event and wait for the ironSource server callback.
    void RewardedVideoOnAdRewardedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
        if (TypeReward == 1)
        {
            MonsterModeGameManager.THIS.GetReward();
        }
        //GameManager.THIS.GetReward();
        else if (TypeReward == 2)
        {
            PlayerPrefs.SetInt("SAW_ADSOPEN", 1);
            MonsterModeGameManager.THIS.adsSaw.SetActive(false);
        }
        else if(TypeReward == 3)
        {

            PlayerPrefs.SetInt("GMAN_ADSOPEN", 1);
            MonsterModeGameManager.THIS.adsGman.SetActive(false);
        }
        else if(TypeReward == 4)
        {
            GameManager.THIS.uIYesNoPlay.ChangeStateOfButton(false);
            YesNoPlayMananger.THIS.RandomSkinInSideBuble();
            StartCoroutine(SetButtonCanSelect());
        }
        else if(TypeReward == 5)
        {
            PlayerPrefs.SetInt(DataGame.countMonsterOnAlbumMax, PlayerPrefs.GetInt(DataGame.countMonsterOnAlbumMax, 3) + 2);
            GameManager.THIS.stageManager.UpdateUIOfLimited();
        }
        else if(TypeReward == 6)
        {
            GameManager.THIS.uIYesNoPlay.limitedItemCanvas.ActionAfterWatchAds();
        }

    }

    private IEnumerator SetButtonCanSelect()
    {
        yield return new WaitForSeconds(2.2f);
        GameManager.THIS.uIYesNoPlay.ChangeStateOfButton(true);
    }


    // The rewarded video ad was failed to show.
    void RewardedVideoOnAdShowFailedEvent(IronSourceError error, IronSourceAdInfo adInfo)
    {
        ComonPopUpController.THIS.Container.SetActive(true);
        ComonPopUpController.THIS.inforText.text = "Ads is not ready \n Please try next time";
        if (TypeReward == 6)
        {
            YesNoPlayMananger.THIS.ClearOneItemShowPopup();
            YesNoPlayMananger.THIS.RandomSkinInSideBuble();
        }
        //FireBaseAnalysticsController.THIS.FireEvent("ADS_REWARD_FAIL");
    }
    // Invoked when the video ad was clicked.
    // This callback is not supported by all networks, and we recommend using it only if
    // it’s supported by all networks you included in your build.
    void RewardedVideoOnAdClickedEvent(IronSourcePlacement placement, IronSourceAdInfo adInfo)
    {
    }


    #region RewardedAd callback handlers

    void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
    {
        Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
    }

    void RewardedVideoAdOpenedEvent()
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");
    }

    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
    {
        Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() + " name = " + ssp.getRewardName());

    }

    void RewardedVideoAdClosedEvent()
    {
        Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
    }

    void RewardedVideoAdStartedEvent()
    {
        Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
    }

    void RewardedVideoAdEndedEvent()
    {
        Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
    }

    void RewardedVideoAdShowFailedEvent(IronSourceError error)
    {
        Debug.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
    }

    /************* RewardedVideo DemandOnly Delegates *************/

    void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
    {

        Debug.Log("unity-script: I got RewardedVideoAdLoadedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {

        Debug.Log("unity-script: I got RewardedVideoAdLoadFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdOpenedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdOpenedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdRewardedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdRewardedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdClosedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClosedDemandOnlyEvent for instance: " + instanceId);
    }

    void RewardedVideoAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
    {
        Debug.Log("unity-script: I got RewardedVideoAdShowFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
    }

    void RewardedVideoAdClickedDemandOnlyEvent(string instanceId)
    {
        Debug.Log("unity-script: I got RewardedVideoAdClickedDemandOnlyEvent for instance: " + instanceId);
    }


    #endregion
}
