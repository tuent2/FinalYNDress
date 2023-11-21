using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmashAskCanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickKeepPunishment()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
        SmashCharacterManager.THIS.ResetTransform();
        AdsIronSourceController.THIS.ShowInterstitialAds();

    }

    public void ClickFinishOff()
    {
        Time.timeScale = 1f;
        SmashController.THIS.HideAllTrap();
        gameObject.SetActive(false);
        AdsIronSourceController.THIS.ShowInterstitialAds();
        SmashCharacterManager.THIS.UltimateShow();
        
    }
}
