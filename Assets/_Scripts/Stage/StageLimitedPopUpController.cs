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
            PlayerPrefs.SetInt(DataGame.countMonsterOnAlbumMax, PlayerPrefs.GetInt(DataGame.countMonsterOnAlbumMax,3) + 2);
            GameManager.THIS.stageManager.UpdateUIOfLimited();
            gameObject.SetActive(false);
        });

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
