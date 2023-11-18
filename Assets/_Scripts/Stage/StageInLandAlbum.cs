using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum StateStageInLandAlbum
{
    Lock,
    Collect
}
public class StageInLandAlbum : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] int idStage;
    [SerializeField] int countMonsterActiveCollectScore = 3;
    [ConditionalHide] [SerializeField] int coinCollect = 20;
    [ConditionalHide] [SerializeField] int secondCollect = 600;
    [SerializeField] StateStageInLandAlbum stateStageInLandAlbum;
    [Header("Refs")]
    [SerializeField] Button adsUnlockButton;
    [SerializeField] Button collectGoldButton;
    [SerializeField] TextMeshProUGUI timeProcessingText;
    [SerializeField] TextMeshProUGUI countMonsterText;
    [SerializeField] TextMeshProUGUI coinCollectText;
    [SerializeField] GameObject lockObject;
    [SerializeField] GameObject collectObject;
    [SerializeField] GameObject processingObject;
    [SerializeField] GameObject notiObject;
    [SerializeField] Image processingImage;
    [SerializeField] SpriteRenderer imageMaskLockSR;
    [SerializeField] StateCanvasGroup buttonAdsUnlockSCG;

    [SerializeField] BoxCollider2D boxColliderDetect;
    [SerializeField] ContactFilter2D contactFilterDetectMonster;
    List<Collider2D> colliderDetectMonsterList = new List<Collider2D>();

    [ConditionalHide] public bool isMonstersFullStage;
    bool isMonstersFullStageOld;
    TimeSpan timeSpanProcessing;
    int secondProcessing;
    bool isInitData = false;
    private void OnEnable()
    {
        //TimingManager.Instance.callbackNextSecond += NextSecond;
        InvokeRepeating(nameof(DetectMonster), .1f, .1f);
    }

    private void OnDisable()
    {
        //TimingManager.Instance.callbackNextSecond -= NextSecond;
        CancelInvoke(nameof(DetectMonster));
    }
    private void Start()
    {
        //adsUnlockButton.onClick.AddListener(() =>
        //{
        //    //Sound
        //    AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);
        //    FirebasePushEvent.intance.LogEvent(string.Format(DataGame.fbADS_REWARD_CLICK_xxx, DataGame.fb_Position_Album));
        //    AdsIronSourceMediation.Instance.ShowRewardedAd((watched) =>
        //    {
        //        if (watched)
        //        {
        //            FirebasePushEvent.intance.LogEvent(string.Format(DataGame.fbADS_REWARD_COMPLETED_xxx, DataGame.fb_Position_Album));
        //            secondProcessing = 0;
        //            PlayerPrefs.SetInt(string.Format(DataGame.seconDanceGetCoinStageInLandAlbum_id, idStage), secondProcessing);
        //            PlayerPrefs.Save();

        //            PlayerPrefs.SetInt(string.Format(DataGame.isUnlockStageInLandAlbum_id, idStage), 1);
        //            PlayerPrefs.Save();
        //            UpdateData();
        //        }
        //    });

        //});
        //collectGoldButton.onClick.AddListener(() =>
        //{
        //    //Sound
        //    //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);

        //    GameManager.Instance.rewardedPopup.ShowCallback(() =>
        //    {
        //        //GameManager.Instance.AddValueCoin(coinCollect);
        //        GameManager.Instance.albumUI.AddCoinWithShowCoinTotal(coinCollect);
        //    });

        //    stateStageInLandAlbum = StateStageInLandAlbum.Collect;

        //    secondProcessing = 0;
        //    PlayerPrefs.SetInt(string.Format(DataGame.seconDanceGetCoinStageInLandAlbum_id, idStage), secondProcessing);
        //    PlayerPrefs.Save();
        //});
        InitData();
    }

    public void InitData()
    {
        if (gameObject.transform.parent.gameObject.activeSelf == false)
        {
            return;
        }
        if (isInitData)
        {
            return;
        }
        //secondCollect = PlayerPrefs.GetInt(DataGame.secondProcessingDanceCollectCoin_FBRemote, 300);
        //coinCollect = PlayerPrefs.GetInt(DataGame.coinCollectAfterProcessedDance_FBRemote, 20);
        //isMonstersFullStage = PlayerPrefs.GetInt(string.Format(DataGame.isFullMonsterStageInLandAlbum_id, idStage), 0) == 1;
        //isMonstersFullStageOld = isMonstersFullStage;
        //secondProcessing = PlayerPrefs.GetInt(string.Format(DataGame.seconDanceGetCoinStageInLandAlbum_id, idStage), 0);
        UpdateData();
        if (isMonstersFullStage)
        {
            UpdateDataWhenFullMonsterOnStage();
        }
       // TimingManager.Instance.callbackNextSecond += NextSecond;
        isInitData = true;
    }
    void UpdateData()
    {
        coinCollectText.text = "x" + coinCollect.ToString("00");
        switch (stateStageInLandAlbum)
        {
            case StateStageInLandAlbum.Lock:
                lockObject.SetActive(true);
                collectObject.SetActive(false);
                break;
            case StateStageInLandAlbum.Collect:
                lockObject.SetActive(false);
                collectObject.SetActive(true);
                break;
            default:
                break;
        }
        if (stateStageInLandAlbum == StateStageInLandAlbum.Lock)
        {
           //// bool isUnlock = PlayerPrefs.GetInt(string.Format(DataGame.isUnlockStageInLandAlbum_id, idStage), 0) == 1;
           // if (isUnlock)
           // {
           //     stateStageInLandAlbum = StateStageInLandAlbum.Collect;

           //     buttonAdsUnlockSCG.SetState(false);
           //     DOTween.Sequence()
           //         .Append(imageMaskLockSR.transform.DOScale(1.5f, .2f).SetEase(Ease.Linear))
           //         .Join(imageMaskLockSR.DOFade(0f, .2f).SetEase(Ease.Linear))
           //         .AppendCallback(() =>
           //         {
           //             lockObject.SetActive(false);
           //             collectObject.SetActive(true);
           //         });
           // }
        }

    }
    private void NextSecond()
    {
        if (isMonstersFullStage && processingObject.activeSelf)
        {
            secondProcessing++;
            if (secondProcessing > secondCollect)
            {
                secondProcessing = secondCollect;
            }
        }
    }
    void DetectMonster()
    {
        if (collectObject.activeSelf == false)
        {
            return;
        }
        int countMonsterOnStage = Physics2D.OverlapCollider(boxColliderDetect, contactFilterDetectMonster, colliderDetectMonsterList);
        isMonstersFullStage = countMonsterOnStage >= countMonsterActiveCollectScore ? true : false;
        if (isMonstersFullStageOld != isMonstersFullStage)
        {
            //PlayerPrefs.SetInt(string.Format(DataGame.isFullMonsterStageInLandAlbum_id, idStage), isMonstersFullStage ? 1 : 0);
            //PlayerPrefs.Save();
            isMonstersFullStageOld = isMonstersFullStage;
        }
        if (!isMonstersFullStage)
        {
            notiObject.gameObject.SetActive(true);
            collectGoldButton.gameObject.SetActive(false);
            processingObject.gameObject.SetActive(false);
        }
        else
        {
            UpdateDataWhenFullMonsterOnStage();
        }
        countMonsterText.text = countMonsterOnStage + "/" + countMonsterActiveCollectScore;
    }
    void UpdateDataWhenFullMonsterOnStage()
    {
        notiObject.gameObject.SetActive(false);

        if (secondProcessing >= secondCollect)
        {
            collectGoldButton.gameObject.SetActive(true);
            processingObject.gameObject.SetActive(false);
        }
        else
        {
            collectGoldButton.gameObject.SetActive(false);
            processingObject.gameObject.SetActive(true);

            var second = secondCollect - secondProcessing;
            timeSpanProcessing = new TimeSpan(0, 0, second);
            timeProcessingText.text = timeSpanProcessing.ToString(@"mm\:ss");

            float amount = 1 - (secondProcessing * 1f / secondCollect);
            processingImage.fillAmount = amount;
        }
    }
    //public void SaveSecondProcessing()
    //{
    //    if (isInitData)
    //    {
    //        PlayerPrefs.SetInt(string.Format(DataGame.seconDanceGetCoinStageInLandAlbum_id, idStage), secondProcessing);
    //        PlayerPrefs.Save();
    //    }
    //}
}
