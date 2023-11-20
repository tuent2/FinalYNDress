using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using UnityEngine.UI;

public class ButtonChangeSkeletonYes : MonoBehaviour
{
    public static ButtonChangeSkeletonYes THIS;
    public int index;
    public string Type;

    //[SpineSkin(dataField: "skeletonDataAsset")] public string itemSkin;
    //public SkinSystems.ItemType itemType;

    private void Awake()
    {
        THIS = this;
    }

    void Start()
    {

        var button = GetComponent<Button>();
        button.onClick.AddListener(
            //delegate { skinsSystem.Equip(itemSkin, itemType); }
            delegate {
                MonsterModeGameManager.THIS.YesButton.interactable = false;
                MonsterModeGameManager.THIS.NoButton.interactable = false;
                MonsterModeGameManager.THIS.ChangeOptionButton.interactable = false;
                int numberPlay = PlayerPrefs.GetInt("NumberPlay", 1);
                if ( numberPlay >= 2)
                {
                    if (Type == "Eyes")
                    {
                        //IronSouceController.THIS.ShowInterstitialAds();
                    }
                }
                //FireBaseAnalysticsController.THIS.FireEvent("CLICK_YES");
                 SoundController.THIS.PlayYesClip();

                MainMonterController.THIS.PlayAnimationCharacter(0, "Yes", true);
                MonsterModeGameManager.THIS.GenImageToTheIcon(Type, index);
                
            }

        
       //delegate { GameManager.THIS.ChangeCurrentSkin(); }

       ); ; ;
    }
}
