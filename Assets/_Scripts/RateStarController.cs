using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RateStarController : MonoBehaviour
{
    
    public void ClickStartToRate(bool isOk)
    {
        gameObject.SetActive(false);
        if (isOk == true)
        {
            string reviewURL = DataGame.appPackageName;
            Application.OpenURL(reviewURL);
            PlayerPrefs.SetInt(DataGame.isRating, 1);
            //IronSouceController.THIS.ResetTimeCount();
            //IronSouceController.THIS.StartCount();
        }
    }

    public void ClickTurnOffRateStart()
    {
       
        gameObject.SetActive(false);
    }

}
