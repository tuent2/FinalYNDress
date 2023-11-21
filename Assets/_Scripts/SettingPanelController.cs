using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;
public class SettingPanelController : MonoBehaviour
{
    public Button SFXButton;
    public Button BGMButton;
    public Button HomeButton;


    int SetSFX;
    int SetBGM;
    public Sprite bg_OnImage;
    public Sprite bg_OffImage;
    public Sprite sfx_OnImage;
    public Sprite sfx_OffImage;
    public Sprite bgm_OnImage;
    public Sprite bgm_OffImage;
    public Image SFX_Bg;
    public Image SFX_Icon;
    public Image BGM_Bg;
    public Image BGM_Icon;


  

    void Start()
    {
        
        SetSFX = PlayerPrefs.GetInt("SFX", 1);
        SetBGM = PlayerPrefs.GetInt("BGM", 1);
       

    }

    public void ClickHomeButtonFromSence0()
    {
        GameManager.THIS.TurnOfCurrentPhase();
        GameManager.THIS.ChangePhaseStage();
    }
    
    public void ClickHomeButtonFromSence1()
    {
        SceneManager.LoadScene(0);
    }

    public  void UISetting()
    {
        if (GameManager.THIS.phaseGame == PhaseGame.YesNoPlay)
        {
            HomeButton.interactable = true;
        }
        if (GameManager.THIS.phaseGame == PhaseGame.Stage)
        {
            HomeButton.interactable = false;
        }
        if (SetSFX == 1)
        {
            SFX_Bg.sprite = bg_OnImage;
            SFX_Icon.sprite = sfx_OnImage;
        }
        else
        {
            SFX_Bg.sprite = bg_OffImage;
            SFX_Icon.sprite = sfx_OffImage;
        }
        if (SetBGM == 1)
        {
            BGM_Bg.sprite = bg_OnImage;
            BGM_Icon.sprite = bgm_OnImage;
        }
        else
        {
            BGM_Bg.sprite = bg_OffImage;
            BGM_Icon.sprite = bgm_OffImage;
        }
    }


    public void ClickSFXButton()
    {
        //SoundController.THIS.PlayTabClip();
        SFXButton.transform.DOScale(Vector3.one * 0.9f, 0.25f).OnComplete(() =>
        {
            SFXButton.transform.DOScale(Vector3.one, 0.25f);
        });
        if (SetSFX == 1)
        {
            SetSFX = 0;
            PlayerPrefs.SetInt("SFX", 0);
            PlayerPrefs.Save();
            SFX_Bg.sprite = bg_OffImage;
            SFX_Icon.sprite = sfx_OffImage;
            //FireBaseAnalysticsController.THIS.FireEvent("SOUND_OFF");
        }
        else
        {
            SetSFX = 1;
            PlayerPrefs.SetInt("SFX", 1);
            PlayerPrefs.Save();
            SFX_Bg.sprite = bg_OnImage;
            SFX_Icon.sprite = sfx_OnImage;
            //FireBaseAnalysticsController.THIS.FireEvent("SOUND_ON");
        }
    }

    public void ClickBGMButton()
    {
        BGMButton.transform.DOScale(Vector3.one * 0.9f, 0.25f).OnComplete(() =>
        {
            BGMButton.transform.DOScale(Vector3.one, 0.25f);
        });
        if (SetBGM == 1)
        {
            SetBGM = 0;
            PlayerPrefs.SetInt("BGM", 0);
            PlayerPrefs.Save();
            BGM_Bg.sprite = bg_OffImage;
            BGM_Icon.sprite = bgm_OffImage;
            SoundController.THIS.ShutDownTheCurrentBGM();
            //FireBaseAnalysticsController.THIS.FireEvent("MUSIC_OFF");
        }
        else
        {
            SetBGM = 1;
            PlayerPrefs.SetInt("BGM", 1);
            PlayerPrefs.Save();
            BGM_Bg.sprite = bg_OnImage;
            BGM_Icon.sprite = bgm_OnImage;


            SoundController.THIS.PlayInGameBGClip();
            //FireBaseAnalysticsController.THIS.FireEvent("MUSIC_ON");
        }
    }



    public void ClickOutSettingButton()
    {
        SoundController.THIS.PlayTabClip();
        int numberPlay = PlayerPrefs.GetInt("NumberPlay", 1);
        if (numberPlay >= 2)
        {
            AdsIronSourceController.THIS.ShowInterstitialAds();
        }

        gameObject.SetActive(false);

        
    }

   
}
