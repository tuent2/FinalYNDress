using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class StageLimitedPopUpController : MonoBehaviour
{
    [SerializeField] Button CloseButton;
    [SerializeField] Button AddMoreLimit;

    void Start()
    {
        CloseButton.onClick.AddListener(()=> {
            gameObject.SetActive(false);
        });
        AddMoreLimit.onClick.AddListener(() => {
            if ( PlayerPrefs.GetInt(DataGame.countMonsterOnAlbumMax, 3)  >= 15)
            {
                ComonPopUpController.THIS.Container.SetActive(true);
                ComonPopUpController.THIS.inforText.text = "Cannot increase the limit any further";
                gameObject.SetActive(false);
                return;
            }
            AdsIronSourceController.THIS.TypeReward = 5;
            AdsIronSourceController.THIS.ShowRewardAds();
            gameObject.SetActive(false);
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
