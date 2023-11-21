using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;
using DG.Tweening;
public class UIYesNoPlay : MonoBehaviour
{
    public static UIYesNoPlay THIS;
    [Header("truoc player")]
   
    [SerializeField] Button yesButton;
    [SerializeField] Button noButton;
    [SerializeField] Button changeOptionButton;
    [SerializeField] GameObject FirstScreen;
    public PopupLimitedItemController limitedItemCanvas;
    [SerializeField] GameObject TutorialView;
    [SerializeField] Button TabButton;
    [SerializeField] TextMeshProUGUI YesOrNoText;
    

    public Transform itemInpopUp;

    [Header("sau player")]
    [SerializeField] Image BgInGameImage;
    [SerializeField] GameObject ItemState;
    [SerializeField] GameObject View;

    [SerializeField] TextMeshProUGUI likeNumberText;
    [SerializeField] TextMeshProUGUI seenNumberText;
    [SerializeField] GameObject likeAndSeen;
    [SerializeField] GameObject ChangeSenceFlash;

    [Header("overLapCanvas")]
    [SerializeField] Animator Flash;
    [SerializeField] GameObject overlapButton;

    int numberPlay;
    [SerializeField] List<Sprite> BgInGameSprite;
    [SerializeField] List<Sprite> BgViewSprite;

    private int likeNumber;
    private int seenNumber;
    private void Awake()
    {
        THIS = this;
    }

  

    public void ChangeStateOfButton(bool isShow)
    {
        yesButton.interactable = isShow;
        noButton.interactable = isShow;
        changeOptionButton.interactable = isShow;
    }

   
    public void ActionChangeWhenFinishChoiceItem()
    {
        FirstScreen.SetActive(false);
        //ItemState.SetActive(false);
        ChangeSenceFlash.SetActive(true);
        
        
        StartCoroutine(OpenViewSence());
    }

  
    private IEnumerator OpenViewSence()
    {
        yield return new WaitForSeconds(0.60f);
        Flash.gameObject.SetActive(true);
        Flash.Play("ChangeToView");
        yield return new WaitForSeconds(0.95f);
        ChangeSenceFlash.SetActive(false);
        
        FirstScreen.SetActive(false);
        ItemState.SetActive(false);
        RandomBgView();
        View.SetActive(true);
        likeAndSeen.SetActive(true);
        TutorialView.SetActive(true);
        
    }

    
    void Start()
    {

        
        yesButton.onClick.AddListener(
            //delegate { skinsSystem.Equip(itemSkin, itemType); }
            delegate
            {
                ChangeStateOfButton(false);
               
                //FireBaseAnalysticsController.THIS.FireEvent("CLICK_YES");
                

                YesNoPlayMananger.THIS.mainCharacterController.PlayAnimationCharacter(0, "Yes", true);
                YesNoPlayMananger.THIS.mainCharacterController.PlayAnimationCharacter(0, "Idle", true,0.667f);
                YesNoPlayMananger.THIS.behindCharacterController.PlayAnimationCharacter(0, "Yes", true);
                SoundController.THIS.PlayYesClip();
                YesNoPlayMananger.THIS.ClickYesGenImageToTheIcon();

            });
        noButton.onClick.AddListener(

            delegate
            {
                ChangeStateOfButton(false);
                
                //FireBaseAnalysticsController.THIS.FireEvent("CLICK_NO");
             

                YesNoPlayMananger.THIS.mainCharacterController.PlayAnimationCharacter(0, "No", true);
                YesNoPlayMananger.THIS.mainCharacterController.PlayAnimationCharacter(0, "Idle", true, 0.5f);
                YesNoPlayMananger.THIS.behindCharacterController.PlayAnimationCharacter(0, "No", true, 1f);
                SoundController.THIS.PlayNoClip();
                //GameManager.THIS.ChangeCurrentSkin(false);

                // YesNoPlayMananger.THIS.behindCharacterController.PlayAnimationCharacter(0, "Idle", true, 1f);
                YesNoPlayMananger.THIS.ChangeIndexOfSkinItem(false);
                StartCoroutine(ShowNotificationAfterDelay());
            }
       ); ; ;
        changeOptionButton.onClick.AddListener(

        delegate
        {

            GameManager.THIS.uIYesNoPlay.ChangeStateOfButton(false);
            YesNoPlayMananger.THIS.RandomSkinInSideBuble();
            StartCoroutine(SetButtonCanSelect());
            //FireBaseAnalysticsController.THIS.FireEvent("ADS_REWARD_CLICK");
            //AdsIronSourceController.THIS.TypeReward = 4;
            //AdsIronSourceController.THIS.ShowRewardAds();
        }
        ); ; ;
        TabButton.onClick.AddListener(

        delegate
        {
            ClickTapButton();
            
        }
        ); ; ;


    }

    private IEnumerator SetButtonCanSelect()
    {
        yield return new WaitForSeconds(2.5f);
        GameManager.THIS.uIYesNoPlay.ChangeStateOfButton(true);
    }

    public void SetupForYNPlay()
    {
        RandomBgIngame();
        FirstScreen.SetActive(true);
        Flash.gameObject.SetActive(false);
        if (!ItemState.activeSelf){
            ItemState.SetActive(true);
        }
        ChangeStateOfButton(true);
    }
    IEnumerator DelayWaitToRandom()
    {
        YesNoPlayMananger.THIS.RandomSkinInSideBuble();
        yield return new WaitForSeconds(1.2f);
        ChangeStateOfButton(true);
    }

    IEnumerator ShowNotificationAfterDelay()
    {
        yield return new WaitForSeconds(0.9f);
        YesNoPlayMananger.THIS.mainCharacterController.PlayAnimationCharacter(0, "Idle", true);
        yield return new WaitForSeconds(0.6f);
        
        YesNoPlayMananger.THIS.RemoveCurrentSkin();
        yield return new WaitForSeconds(0.5f);
        YesNoPlayMananger.THIS.behindCharacterController.PlayAnimationCharacter(0, "Idle", true, 1f);
        YesNoPlayMananger.THIS.RandomSkinInSideBuble();
        yield return new WaitForSeconds(1.2f);
        ChangeStateOfButton(true);

    }   

    public void RandomBgIngame()
    {
        int bgIngameRandomIndex = Random.Range(0, BgInGameSprite.Count);
        BgInGameImage.sprite = BgInGameSprite[bgIngameRandomIndex];
    }

    private void RandomBgView()
    {
        int bgIngameRandomIndex = Random.Range(0, BgViewSprite.Count);
        BgInGameImage.sprite = BgViewSprite[bgIngameRandomIndex];
    }

    public void PlayIEnumPlayTabToGet()
    {
        StartCoroutine(PlayTabToGet()); 
    }

    private int startValue = 0;
    private float duration = 3f;
    private Tween LikeReactTween;
    public IEnumerator PlayTabToGet()
    {
        Flash.gameObject.SetActive(false);
        YesNoPlayMananger.THIS.EmojiEffect.Play();
        likeNumber = Random.Range(1000, 9999);
        //LikeNumberText.text = LikeNumber+"K";
        seenNumber = Random.Range(1000, 9999);
        //SeenNumberText.text = LikeNumber + "K";
        DOTween.To(() => startValue, x => startValue = x, likeNumber, duration)
           .OnUpdate(() => likeNumberText.text = startValue.ToString() + " K")
           .SetEase(Ease.Linear)
          ;
        DOTween.To(() => startValue, x => startValue = x, seenNumber, duration)
           .OnUpdate(() => seenNumberText.text = startValue.ToString() + " K")
           .SetEase(Ease.Linear)
           ;

        yield return new WaitForSeconds(6.6f);
        YesNoPlayMananger.THIS.mainCharacterController.PlayAnimationCharacter(0, "Pose", true);
        TutorialView.SetActive(false);
        likeAndSeen.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        CaptureAndSetScreenshot();

        Flash.gameObject.SetActive(true);
        Flash.Play("ChangeToComplete");
        yield return new WaitForSeconds(1f);
        if (PlayerPrefs.GetInt(DataGame.isDoneFirstTimePlay,0) == 0)
        {
            PlayerPrefs.SetInt(DataGame.isDoneFirstTimePlay, 1);
        }
        GameManager.THIS.TurnOfCurrentPhase();
        GameManager.THIS.ChangePhaseComplete();
    }

    public void CaptureAndSetScreenshot()
    {
        StartCoroutine(CaptureScreenAndSetImage());
    }

    private IEnumerator CaptureScreenAndSetImage()
    {


        // Chụp màn hình
        yield return new WaitForEndOfFrame();
        int screenWidth = Screen.width;
        int screenHeight = Screen.height;

        // Tính toán kích thước ảnh chụp dựa trên tỷ lệ chiều dài được giữ lại (8/10)
        //int captureWidth = Mathf.RoundToInt(screenWidth * 8f / 10f);
        //int captureHeight = screenHeight;
        int captureWidth = screenWidth;
        int captureHeight = Mathf.RoundToInt(screenHeight * 8f / 10f);

        // Tính toán vị trí bắt đầu từ 1/10 đến 9/10 chiều dài màn hình
        int startX = 0;
    int startY = Mathf.RoundToInt(screenHeight / 10f); // Bắt đầu từ đầu chiều dài màn hình

    // Tạo một Texture2D với kích thước mong muốn
    Texture2D screenshotTexture = new Texture2D(captureWidth, captureHeight, TextureFormat.RGB24, false);

    // Chụp màn hình từ vị trí bắt đầu
    screenshotTexture.ReadPixels(new Rect(startX, startY, captureWidth, captureHeight), 0, 0);
    screenshotTexture.Apply();

    // Tạo một Sprite từ Texture2D
    Sprite screenshotSprite = Sprite.Create(screenshotTexture, new Rect(0, 0, captureWidth, captureHeight), new Vector2(0.5f, 0.5f));
        UIComplete.THIS.SetSripteForScreenShot(screenshotSprite);

    }


    public void ClickTapButton()
    {
        
        SoundController.THIS.PlayTabClip();
        int inCreateNumber = Random.Range(1, 5);
        int inCreateNumberSeen = Random.Range(1, 5);
        likeNumber += inCreateNumber;
        likeNumberText.text = likeNumber + "K";
        seenNumber += inCreateNumberSeen;
        seenNumberText.text = seenNumber + "K";

        if (LikeReactTween == null)
        {
            //TabButtonText.transform.DOScale(LikeObject.transform.localScale * 1.2f, 0.1f);
            LikeReactTween = likeAndSeen.transform.DOScale(likeAndSeen.transform.localScale * 1.05f, 0.1f).OnComplete(() =>
            {
                //TabButtonText.transform.DOScale(Vector3.one, 0.1f);
                likeAndSeen.transform.DOScale(Vector3.one, 0.1f).OnComplete(() =>
                {
                    LikeReactTween = null;

                }); ; ;
            }); ;
        }


    }

}
