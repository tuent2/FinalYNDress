//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using Firebase;
//using Firebase.Analytics;
//public class FireBaseAnalysticsController : MonoBehaviour
//{
//    public static FireBaseAnalysticsController THIS;
//    private bool isFinishedCheck;
//    private void Awake()
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
//    }
//    public void Start()
//    {
//        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
//        {
//            var dependencyStatus = task.Result;
//            if (dependencyStatus == DependencyStatus.Available)
//            {
//                // Create and hold a reference to your FirebaseApp,
//                // where app is a Firebase.FirebaseApp property of your application class.
//                // Crashlytics will use the DefaultInstance, as well;
//                // this ensures that Crashlytics is initialized.
//                FirebaseApp app = FirebaseApp.DefaultInstance;
//                isFinishedCheck = true;
//                // Set a flag here for indicating that your project is ready to use Firebase.
//            }
//            else
//            {
//                isFinishedCheck = false;
//                Debug.LogError(System.String.Format(
//                  "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//                // Firebase Unity SDK is not safe to use here.
//            }
//        });
//        //IronSourceController.THIS.ShowBanner();
//    }

//    public void FireEvent(string log)
//    {

//        if (isFinishedCheck)
//        {
//            log = log.Replace(" ", "_");
//            log = log.Replace(".", "_");
//            FirebaseAnalytics.LogEvent(log);
//        }


//    }
//}
