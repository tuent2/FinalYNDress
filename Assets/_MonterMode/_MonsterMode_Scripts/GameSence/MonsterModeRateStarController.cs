using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonterModeRateStarController : MonoBehaviour
{
    public string appPackageName = "https://play.google.com/store/apps/details?id=com.dressup.makeup.leftright.monstermakeover.monsterdressup.yesorno";
    public void ClickStartToRate(bool isOk)
    {
        gameObject.SetActive(false);
        if (isOk == true)
        {
            string reviewURL = appPackageName;
            Application.OpenURL(reviewURL);
            PlayerPrefs.SetInt("isRating", 1);
            //IronSouceController.THIS.ResetTimeCount();
            //IronSouceController.THIS.StartCount();
        }
    }

    public void ClickTurnOffRateStart()
    {
        gameObject.SetActive(false);
    }

}
