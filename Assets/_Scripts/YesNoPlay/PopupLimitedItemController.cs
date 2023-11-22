using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using NaughtyAttributes;
public class PopupLimitedItemController : MonoBehaviour
{
    [SerializeField] CanvasGroup gameobjectCanvasGroup;
    [SerializeField] Button claimButton;
    [SerializeField] Button noThankButton;
    [SerializeField] Image itemImage;
    public int indexOfItem;
    
    void Start()
    {
        
    }

    public void ClickNoThankButton()
    {
        gameObject.SetActive(false);
        YesNoPlayMananger.THIS.ClearOneItemShowPopup();
        YesNoPlayMananger.THIS.RandomSkinInSideBuble();
    }

    public void ClickClaimpButton()
    {
        AdsIronSourceController.THIS.TypeReward = 6;
        AdsIronSourceController.THIS.ShowRewardAds();
        gameObject.SetActive(false);

        //YesNoPlayMananger.THIS.behindCharacterController.MoveLimitedItemToTarget(itemImage.sprite);
        //gameobjectCanvasGroup.DOFade(0, 2f).OnComplete(()=> {
        //    gameObject.SetActive(false);
        //    gameobjectCanvasGroup.alpha = 1f;
        //});
        gameObject.SetActive(false);
        
    }

    [Button("TestLimitItemAni")]
    public void ActionAfterWatchAds()
    {
        YesNoPlayMananger.THIS.behindCharacterController.MoveLimitedItemToTarget(itemImage.sprite);
        gameObject.SetActive(false);
    }

    

    public void UpdateSpriteInItemImage(int index, List<Sprite> listItem)
    {
        indexOfItem = index;
        itemImage.sprite = listItem[index];
    }
}
