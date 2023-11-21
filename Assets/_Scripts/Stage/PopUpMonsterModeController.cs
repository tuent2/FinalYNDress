using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PopUpMonsterModeController : MonoBehaviour
{
    [SerializeField] Button exitPopUpButton;
    [SerializeField] Button tryNowButton;
    void Start()
    {
        exitPopUpButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt(DataGame.isOpenMonsterMode, 1);
            AdsIronSourceController.THIS.ShowInterstitialAds();
            gameObject.SetActive(false);
        });

        tryNowButton.onClick.AddListener(() => {
            PlayerPrefs.SetInt(DataGame.isOpenMonsterMode, 1);
            LoadingController.THIS.LoadingAction(() => {
                SceneManager.LoadScene(1);
            });
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
