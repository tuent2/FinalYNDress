using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;
public class ButtonChangeSkeletonNo : MonoBehaviour
{
    public static ButtonChangeSkeletonNo THIS;
   
    public string Type;

    private void Awake()
    {
        THIS = this;
    }

    void Start()
    {

        var button = GetComponent<Button>();
        button.onClick.AddListener(
            
            delegate {
                MonsterModeGameManager.THIS.YesButton.interactable = false;
                MonsterModeGameManager.THIS.NoButton.interactable = false;
                MonsterModeGameManager.THIS.ChangeOptionButton.interactable = false;
                int numberPlay = PlayerPrefs.GetInt("NumberPlay", 1);
                if (numberPlay >= 2)
                {   
                    if (Type == "Eyes")
                    {
                        //IronSouceController.THIS.ShowInterstitialAds();
                    } 
                        
                    //IronSouceController.THIS.ShowInterstitialAds();
                }
                //FireBaseAnalysticsController.THIS.FireEvent("CLICK_NO");
                SoundController.THIS.PlayNoClip();
                
                MainMonterController.THIS.PlayAnimationCharacter(0, "No", true);
                MonsterModeGameManager.THIS.ChangeCurrentSkin(false);


                StartCoroutine(ShowNotificationAfterDelay(1.5f));
                
                
            }
      

       ); ; ;
    }


    IEnumerator ShowNotificationAfterDelay(float delayInSeconds)
    {
        yield return new WaitForSeconds(delayInSeconds);
        MainMonterController.THIS.PlayAnimationCharacterHasDelay(0, "Idle_Ngoi", true, 1f);
        BehindMonsterController.THIS.RemoveSkin(Type);
        yield return new WaitForSeconds(0.5f);
        BehindMonsterController.THIS.PlayAnimationCharacterHasDelay(0, "Idle", true, 0.5f);
        MonsterModeGameManager.THIS.RandomSkinYesNo();

        StartCoroutine(SetButtonCanSelect());
    }

    private IEnumerator SetButtonCanSelect()
    {
        yield return new WaitForSeconds(1.2f);
        MonsterModeGameManager.THIS.YesButton.interactable = true;
        MonsterModeGameManager.THIS.NoButton.interactable = true;
        MonsterModeGameManager.THIS.ChangeOptionButton.interactable = true;
    }
}
