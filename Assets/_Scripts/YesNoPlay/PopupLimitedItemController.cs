using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
        YesNoPlayMananger.THIS.behindCharacterController.MoveLimitedItemToTarget(itemImage.sprite);
        //gameobjectCanvasGroup.DOFade(0, 2f).OnComplete(()=> {
        //    gameObject.SetActive(false);
        //    gameobjectCanvasGroup.alpha = 1f;
        //});
        gameObject.SetActive(false);
        
    }

    

    public void UpdateSpriteInItemImage(int index, List<Sprite> listItem)
    {
        indexOfItem = index;
        itemImage.sprite = listItem[index];
    }
}
