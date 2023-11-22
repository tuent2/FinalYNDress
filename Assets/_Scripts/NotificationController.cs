using System.Collections;
using System.Collections.Generic;
using Unity.Notifications.Android;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using System;
public class NotificationController : MonoBehaviour
{
    public static NotificationController THIS;
    private void Awake()
    {
        //if (THIS != null)
        //{
        //    gameObject.SetActive(false);
        //    Destroy(gameObject);
        //}
        //else
        //{
        THIS = this;
        //    DontDestroyOnLoad(gameObject);
        //}
    }
    private int notificationId = 0;
    private void Start()
    {
        NotifyControl();
    }
    public void NotifyControl()
    {
        System.DateTime today = System.DateTime.Now.Date;
        if (PlayerPrefs.GetInt("FirstNoticeSend", 0) == 0)
        {
            StartNotice("Noti General 1", "Notice 1", "Yes or No: Dress Up", notice1, 10800);
            StartNotice("Noti General 1", "Notice 1", "Yes or No: Dress Up", notice1, today.AddHours(3));
            StartNotice("Noti General 2_1", "Notice 2", "Yes or No: Dress Up", notice2_1, today.AddDays(1).AddHours(12));
            StartNotice("Noti General 2_2", "Notice 2.5", "Yes or No: Dress Up", notice2_2, today.AddDays(1).AddHours(20));
            StartNotice("Noti General 3", "Notice 3", "Yes or No: Dress Up", notice3, today.AddDays(2).AddHours(20));
            StartNotice("Noti General 4", "Notice 4", "Yes or No: Dress Upp", notice4, today.AddDays(3).AddHours(12));
            StartNotice("Noti General 5", "Notice 5", "Yes or No: Dress Up", notice5, today.AddDays(4).AddHours(20));
            StartNotice("Noti General 6", "Notice 6", "Yes or No: Dress Up", notice6, today.AddDays(5).AddHours(12));
            StartNotice("Noti General 7", "Notice 7", "Yes or No: Dress Up", notice7, today.AddDays(6).AddHours(20));
            PlayerPrefs.SetInt("FirstNoticeSend", 1);
        }

        if (!PlayerPrefs.HasKey("FirstDownloadTime"))
        {
            PlayerPrefs.SetString("FirstDownloadTime", System.DateTime.Now.ToString());
        }
        DateTime firstDownloadTime = DateTime.Parse(PlayerPrefs.GetString("FirstDownloadTime"));
        DateTime scheduledTime = firstDownloadTime.AddDays(8);

        if (scheduledTime < DateTime.Now)
        {
            PlayerPrefs.SetString("aftereday", System.DateTime.Now.ToString());
            DateTime aftereday = DateTime.Parse(PlayerPrefs.GetString("aftereday"));
            aftereday = scheduledTime.AddDays(1);
            if (aftereday < DateTime.Now)
            {
                int RandomIndexNotice = UnityEngine.Random.Range(0, 4);
                StartNotice("Noti General 8", "Notice 8", "Yes or No: Dress Up", notice8[RandomIndexNotice], scheduledTime);
            }
        }

    }
    private string notice1 = "Do you want to play now? Yes or Yes? 👌";
    private string notice2_1 = "👗🤣👚Time to style your character! Let's dress up and have fun!🤣👗👚";
    private string notice2_2 = "👕👜New Item for your Model!!! Try them now 🤩🤩";
    private string notice3 = "🎁🎁Many rewards are ready, Play to collect !!!🎁🎁";
    private string notice4 = "🥻🩳Get ready to create your unique character look👜👝";
    private string notice5 = "👚😎Dress up and show off your fashion skills!!! 😂👗";
    private string notice6 = "🤣👚Your character deserves a fresh new outfit!!!🤩👗";
    private string notice7 = "👕👜New Item for your Model!!! Try them now 🤩🤩";
    private string[] notice8 = new string[] { "Do you want to play now? Yes or Yes? 👌", "👗🤣👚Time to style your character! Let's dress up and have fun!🤣👗👚" ,
                            "👕👜New Item for your Model!!! Try them now 🤩🤩"};


    public void StartNotice()
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = "example_channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic Notifications",

        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification();
        notification.Title = "Yes or No: Dress Up";
        notification.Text = "Run fast as fuck";
        notification.LargeIcon = "icon_0";
        notification.SmallIcon = "icon_1";
        notification.ShowTimestamp = true;
        notification.FireTime = System.DateTime.Now.AddSeconds(20);

        var identifier =
            AndroidNotificationCenter.SendNotification(notification, "example_channel_id");

        if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        {
            AndroidNotificationCenter.CancelAllNotifications();
            AndroidNotificationCenter.SendNotification(notification, "example_channel_id");

        }
    }
    public void StartNotice(string idChannel, string name, string title, string text, int fireTimeMinutes)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = idChannel,
            Name = name,
            Importance = Importance.High,
            Description = "Generic Notifications",

        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.LargeIcon = "icon_0";
        notification.SmallIcon = "icon_1";
        notification.ShowTimestamp = true;
        notification.FireTime = System.DateTime.Now.AddMinutes(fireTimeMinutes);

        var identifier =
            AndroidNotificationCenter.SendNotification(notification, idChannel);

        // if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        {
            //AndroidNotificationCenter.CancelAllNotifications();
            // AndroidNotificationCenter.SendNotification(notification, idChannel);
        }
    }
    public void StartNotice(string idChannel, string name, string title, string text, System.DateTime fireTimeMinutes)
    {
        AndroidNotificationChannel notificationChannel = new AndroidNotificationChannel()
        {
            Id = idChannel,
            Name = name,
            Importance = Importance.High,
            Description = "Generic Notifications",

        };

        AndroidNotificationCenter.RegisterNotificationChannel(notificationChannel);

        AndroidNotification notification = new AndroidNotification();
        notification.Title = title;
        notification.Text = text;
        notification.LargeIcon = "icon_0";
        notification.SmallIcon = "icon_1";
        notification.ShowTimestamp = true;
        notification.FireTime = fireTimeMinutes;

        var identifier =
            AndroidNotificationCenter.SendNotification(notification, idChannel);

        //if (AndroidNotificationCenter.CheckScheduledNotificationStatus(identifier) == NotificationStatus.Scheduled)
        {
            //AndroidNotificationCenter.CancelAllNotifications();
            // AndroidNotificationCenter.SendNotification(notification, idChannel);
        }
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            AndroidNotificationIntentData data = AndroidNotificationCenter.GetLastNotificationIntent();
            if (data != null && data.Id == notificationId)
            {
                SceneManager.LoadScene("GameScene");
            }
        }
    }
}
