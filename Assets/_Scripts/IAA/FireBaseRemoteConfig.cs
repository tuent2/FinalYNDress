//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using Firebase.RemoteConfig;
//using System.Threading.Tasks;
//using System;
//using Firebase.Extensions;

//public class FireBaseRemoteConfig : MonoBehaviour
//{
//    public static FireBaseRemoteConfig THIS;
//    public int AdsInterDelay;
//    // Start is called before the first frame update
//    void Awake()
//    {
//        if (THIS != null)

//        {
//            gameObject.SetActive(false);
//            Destroy(gameObject);
//        }
//        else
//        {
//            THIS = this;
//            DontDestroyOnLoad(gameObject);
//        }


//        Dictionary<string, object> defaults = new Dictionary<string, object>();
//        // These are the values that are used if we haven't fetched data from the
//        // server
//        // yet, or if we ask for values that the server doesn't have:
//        defaults.Add("AdsInterDelay", PlayerPrefs.GetInt("AdsInterDelay", 22));

//        FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);
//        FetchDataAsync();
//        FetchDataAsync();
//    }
//    public void CallTest()
//    {
//        FetchDataAsync();
//    }
//    public void CheckParameter()
//    {
//        AdsInterDelay = (int)FirebaseRemoteConfig.DefaultInstance.GetValue("AdsInterDelay").LongValue;
//    }
//    public Task FetchDataAsync()
//    {
//        System.Threading.Tasks.Task fetchTask =
//        FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero);
//        return fetchTask.ContinueWithOnMainThread(FetchComplete);
//    }
//    void FetchComplete(Task fetchTask)
//    {
//        var info = FirebaseRemoteConfig.DefaultInstance.Info;
//        CheckParameter();
//        switch (info.LastFetchStatus)
//        {
//            case LastFetchStatus.Success:
//                FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
//                .ContinueWithOnMainThread(task => {
//                    Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
//                                   info.FetchTime));
//                });

//                break;
//            case LastFetchStatus.Failure:
//                switch (info.LastFetchFailureReason)
//                {
//                    case FetchFailureReason.Error:
//                        Debug.Log("Fetch failed for unknown reason");
//                        break;
//                    case FetchFailureReason.Throttled:
//                        Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
//                        break;
//                }
//                break;
//            case LastFetchStatus.Pending:
//                Debug.Log("Latest Fetch call still pending.");
//                break;
//        }
//    }
//}
