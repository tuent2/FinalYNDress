using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class NotificationPopup : MonoBehaviour
{
    [SerializeField] Button okButton;
    [SerializeField] TextMeshProUGUI valueText;
    UnityAction callbackHided;

    private void Start()
    {
        okButton.onClick.AddListener(() =>
        {
            //AudioManager.Instance.PlayOneShot(AudioManager.Instance.audioClipData.tapButtonAudioClip);
            //GameManager.Instance.ShowInterTapEveryWhere();
            Hide();
        });
    }
    public  void Show()
    {
        //base.Show();
        //GameManager.Instance.giftManager.PauseGiftDrop();
        //if (GameManager.Instance.albumUI.isOpenning)
        //{
        //    GameManager.Instance.albumManager.SetStateControllInAlbum(false);
        //}
        //NativeAdsManager.Instance.ShowBottomNativeAdsPanel();
    }
    public  void Hide()
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
    public void ShowWithTextCallback(string text, UnityAction callbackHided)
    {
        this.callbackHided = callbackHided;
        ShowWithText(text);
    }
    //protected override void CallHided()
    //{
    //    base.CallHided();
    //    callbackHided?.Invoke();
    //    callbackHided = null;
    //}
}
