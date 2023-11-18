using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestNotificationPopup : MonoBehaviour
{
    [SerializeField] Button okButton;
    [SerializeField] Button closeButton;
    [SerializeField] TextMeshProUGUI valueText;
    UnityAction<bool> callbackHided;
    bool isOk;

    private void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);
            //GameManager.Instance.ShowInterTapEveryWhere();
            isOk = true;
            Hide();
        });
        closeButton.onClick.AddListener(() =>
        {
            //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);
            Hide();
        });
    }
    public void Show()
    {
        //base.Show();
        //GameManager.Instance.giftManager.PauseGiftDrop();
        //if (GameManager.Instance.albumUI.isOpenning)
        //{
        //    GameManager.Instance.albumManager.SetStateControllInAlbum(false);
        //}
        //isOk = false;
        //NativeAdsManager.Instance.ShowBottomNativeAdsPanel();
    }
    public void Hide()
    {
        //base.Hide();
        //GameManager.Instance.giftManager.ResumeGiftDrop();
        //if (GameManager.Instance.albumUI.isOpenning)
        //{
        //    GameManager.Instance.albumManager.SetStateControllInAlbum(true);
        //}
        //NativeAdsManager.Instance.HideBottomNativeAdsPanel();
    }
    public void ShowWithText(string text)
    {
        valueText.text = text;
        Show();
    }
    public void ShowWithTextCallback(string text, UnityAction<bool> callbackHided)
    {
        this.callbackHided = callbackHided;
        ShowWithText(text);
    }
    //protected override void CallHided()
    //{
    //    ////base.CallHided();
    //    ////callbackHided?.Invoke(isOk);
    //    ////callbackHided = null;
    //}
}
