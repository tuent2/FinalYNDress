using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using DG.Tweening;
using Spine.Unity;
using System.IO;
using UnityEngine.SceneManagement;
public class MonsterModeGameManager : MonoBehaviour
{
    [SerializeField] Button noAdsButton;
    
    public static MonsterModeGameManager THIS;

    public SkinToChange currentSkin = SkinToChange.Head;
    public enum SkinToChange
    {
        Head,
        Body,
        Acc,
        Eyes,
        Mouth
    }

    public Camera mainCamera;
    public GameObject targetObject;
    public Animation BlinkEyesAnimation;
    
    private GameObject behindObject;

    public GameObject ChairObject;
    public Canvas RateCanvas;
    public Button YesButton;
    public Button NoButton;
    public Button ChangeOptionButton;

    //private TrackEntry defaultAnimation;
    public float zoomFactor = 1.2f;
    public float scaleAmount = 1.2f;

    public Image fadeImage;


    public Transform HeadMove;
    public Transform BodyMove;
    public Transform AccMove;
    public Transform EyesMove;
    public Transform MouthMove;

    public GameObject IngameCanvas;
    public GameObject SettingPanel;
    public GameObject BgSetting;
    
    public Button TutorialButton;
    public Image ViewSence;

    public GameObject FinalCanvas;
    [Header("ShareCanvas")]
    public GameObject ShareCanvas;
    public Button ShareButtonFinal;
    public Button BackButton;
    public Image FinalScreenShotImage;
    public TextMeshProUGUI FinalLikeTextOfShare;
    public TextMeshProUGUI FinalSeenTextOfShare;
    [Space]
    public TextMeshProUGUI FinalLikeText;
    public TextMeshProUGUI FinalSeenText;
    [Header("ItemCanvas")]
    public Canvas ItemCanvas;

    [Space]

    public Image HeadSelectedImage;
    public Image BodySelectedImage;
    public Image AccSelectedImage;
    public Image EyesSelectedImage;
    public Image MouthSelectedImage;

    public ParticleSystem HeadSelectedEffect;
    public ParticleSystem BodySelectedEffect;
    public ParticleSystem AccSelectedEffect;
    public ParticleSystem EyesSelectedEffect;
    public ParticleSystem MouthSelectedEffect;

    [Space]
    public Image HeadBg;
    public Image BodyBg;
    public Image AccBg;
    public Image EyesBg;
    public Image MouthBg;

    [Space]
    public Sprite ItemSelected;
    //public Sprite ItemNonSelected;
    public Sprite ItemIsSelecting;




    public Image ScreenShotImage;
    [Header("Smash Canvas")]
    public Canvas SmashCanvas;
    public GameObject SmashManager;
    public GameObject SmashCharacter;
    public GameObject StickController;
    public GameObject SawController;
    public GameObject SawObject;
    public GameObject GmanController;
    public Image StickSmashImage;
    public Image SawSmashImage;
    public Image LazeSmashImage;
    public Canvas SmashAskCanvas;
    public Canvas FinisherCanvas;
    public Image khungStick;
    public Image khungSaw;
    public Image khungGman;

    public GameObject adsSaw;
    public GameObject adsGman;
    public Sprite isUsingItem;
    public Sprite notUsingItem;
    private string selectedSmashTool;

    [Header("Hide For Smash Canvas")]
    public Canvas BGCanvas;
    public Canvas Canvas;
    public Canvas ViewCanvas;
    public Canvas TutorialCanvas;
    public Canvas RepondAdsCanvas;
    [Space]
    public GameObject LikeObject;
    public GameObject TabButtonText;
    public GameObject TutorialObject;
    public TextMeshProUGUI LikeNumberText;
    public TextMeshProUGUI SeenNumberText;
    public int LikeNumber;
    public int SeenNumber;
    private Tween LikeReactTween = null;

    private int CountYN = 1;
    public bool isFinalChoice = false;

    public GameObject StickSmash;
    public GameObject SawSmash;

    private int numberPlay;

    public List<GameObject> behindMonster;

    public GameObject[] bongbongBlow;
    public GameObject buiSao;
    public GameObject CompletedEffect;
    public GameObject EmojiEffect;
    private GameObject cloneCompletedEffect;
    private GameObject cloneCompletedEffect1;
    private GameObject cloneCompletedEffect2;


    //public Button ShareButton;
    public Image CompletedBGImage;
    public Sprite[] CompletedBGSprite;
    [Space]
    public Image InGameBGImage;
    public Sprite[] InGameBGSprite;
    private void Awake()
    {
        THIS = this;
    }

    void Start()
    {
        if (PlayerPrefs.GetInt("removeAds", 0) == 1)
        {
            noAdsButton.gameObject.SetActive(false);
        }
        //int indexImage = Random.Range(0, CompletedBGSprite.Length);
        //CompletedBGImage.sprite = CompletedBGSprite[indexImage];

        InGameBGImage.sprite = InGameBGSprite[Random.Range(0, InGameBGSprite.Length)];
        
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        LikeNumber = 0;
        //HeadSelectedImage.gameObject.SetActive(false);
        //BodySelectedImage.gameObject.SetActive(false);
        //AccSelectedImage.gameObject.SetActive(false);
        //EyesSelectedImage.gameObject.SetActive(false);
        //MouthSelectedImage.gameObject.SetActive(false);


        int myValue = PlayerPrefs.GetInt("typeOfMode");
        if (myValue == 0)
        {
            StartPlayGameMode0Normal();

        }
        else if (myValue ==1)
        {
            StartPlayGameMode1Smash();
        }
        
    }


    public void ClickSettingButton()
    {
        SoundController.THIS.PlayTabClip();
        SettingPanel.SetActive(true);
        Vector3 originalPosition = BgSetting.transform.position;
        BgSetting.transform.position = new Vector3(originalPosition.x - 30f, originalPosition.y, originalPosition.z);
        BgSetting.transform.DOMove(originalPosition, 0.75f).SetEase(Ease.OutQuad).SetUpdate(true);
        //FireBaseAnalysticsController.THIS.FireEvent("SETTING_CLICK");
    }

    public void ClickOutSettingButton()
    {
        SettingPanel.SetActive(false);
    }


    public void ClickPlayAgain()
    {
        SoundController.THIS.PlayTabClip();
        SceneManager.LoadScene(0);
        SoundController.THIS.PlayInGameBGClip();
    }

    private IEnumerator WaitChangeSence() { 
        yield return new WaitForSeconds(1.5f);
        LoadingController.THIS.gameObject.SetActive(false);
    }

    public void WinGame()
    {
        YesButton.interactable = false;
        NoButton.interactable = false;
        ChangeOptionButton.interactable = false;
        IngameCanvas.SetActive(false);
        ItemCanvas.gameObject.SetActive(false);
        BehindMonsterController.THIS.DisableBongBongAndItem();
        Destroy(behindObject);
        Destroy(ChairObject);
        mainCamera.DOOrthoSize(mainCamera.orthographicSize / zoomFactor, 0.75f);
        targetObject.transform.DOScale(targetObject.transform.localScale * scaleAmount, 0.75f)
            .OnComplete(() =>
            {
                fadeImage.DOFade(1f, 0.75f).OnComplete(() =>
                    {
                        BlinkEyesAnimation.enabled = false;
                        BlinkEyesAnimation.gameObject.transform.localScale = new Vector3(1, 1, 1);
                        int danceRandom = Random.Range(0, 2);
                        if (danceRandom == 0)
                        {
                            MainMonterController.THIS.PlayAnimationCharacter(0, "dance1", true);
                        }
                        else
                        {
                            MainMonterController.THIS.PlayAnimationCharacter(0, "dance2", true);
                        }

                        //StartCoroutine(FadeImageWait());
                        fadeImage.DOFade(0.65f, 0.35f).OnComplete(() =>
                        {
                            
                                UpdateCharacterPosition();
                                fadeImage.DOFade(0.0f, 2f).OnComplete(() => {
                                    
                                });
                            

                        });
                        //UpdateCharacterPosition();
                    });

                // Debug.Log("Zoom and scale completed.");
                // StartCoroutine(ExplodeSequence());
                //UpdateCharacterPosition();
            });
        // mainCamera.transform.DOMove(zoomTarget, zoomDuration);

    }

    public void GenImageToTheIcon(string Type, int index)
    {
        BehindMonsterController.THIS.FadeBongBongImage(Type, index);


    }

    void UpdateCharacterPosition()
    {
        SoundController.THIS.PlayBackGroundCompletedClip();
        //Destroy(targetObject);
        targetObject.SetActive(true);
        TutorialButton.gameObject.SetActive(true);
        ViewSence.gameObject.SetActive(true);
        float targetY = Screen.height * 1 / 28;

        Vector3 characterScreenPosition = new Vector3(Screen.width / 2, targetY, 0);
        Vector3 characterWorldPosition = Camera.main.ScreenToWorldPoint(characterScreenPosition);

        targetObject.transform.position = characterWorldPosition;
        characterWorldPosition.z = 0;
        targetObject.transform.position = characterWorldPosition;
        mainCamera.DOOrthoSize(mainCamera.orthographicSize * zoomFactor, 1f);
        
        Instantiate(CompletedEffect, characterWorldPosition, Quaternion.identity);
        Instantiate(CompletedEffect, new Vector3(Screen.height / 4, -3, 0), Quaternion.identity);
        Instantiate(CompletedEffect, new Vector3(Screen.height / 2, 3, 0), Quaternion.identity);
        targetObject.transform.DOScale(targetObject.transform.localScale / scaleAmount, 2f)
            .OnComplete(() =>
            {
                cloneCompletedEffect = Instantiate(EmojiEffect, new Vector3(-2, -2, 0), Quaternion.identity);
                cloneCompletedEffect1 = Instantiate(EmojiEffect, new Vector3(2, 2, 0), Quaternion.identity);
                cloneCompletedEffect2 = Instantiate(EmojiEffect, new Vector3(2, -4, 0), Quaternion.identity);
                StartCoroutine(PlayTabToGet());
            });
    }


    private int startValue = 0;
    private float duration = 3f;

    private IEnumerator PlayTabToGet()
    {

        LikeNumber = Random.Range(200, 1000);
        //LikeNumberText.text = LikeNumber+"K";
        SeenNumber = Random.Range(200, 1000);
        //SeenNumberText.text = LikeNumber + "K";
        DOTween.To(() => startValue, x => startValue = x, LikeNumber, duration)
           .OnUpdate(() => LikeNumberText.text = startValue.ToString() + " K")
           .SetEase(Ease.Linear)
          ;
        DOTween.To(() => startValue, x => startValue = x, SeenNumber, duration)
           .OnUpdate(() => SeenNumberText.text = startValue.ToString() + " K")
           .SetEase(Ease.Linear)
           ;


        TutorialButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(6f);
        TutorialButton.gameObject.SetActive(false);
        //StopIncrementing();
        CaptureAndSetScreenshot();

        fadeImage.DOFade(1f, 0.75f).OnComplete(() =>
        {
            fadeImage.DOFade(0f, 0.4f).OnComplete(() =>
            {
                //ViewSence.gameObject.SetActive(false);
                //TabToGetButton.gameObject.SetActive(false);
                LikeObject.SetActive(false);
                targetObject.SetActive(false);

                Instantiate(buiSao, new Vector3(-3, -3, 0), Quaternion.identity);
                Instantiate(buiSao, new Vector3(3, 3, 0), Quaternion.identity);
                // CaptureAndSetScreenshot();
                FinalLikeText.text = LikeNumber + " K";
                FinalSeenText.text = SeenNumber + " K";
                FinalLikeTextOfShare.text = LikeNumber + " K";
                FinalSeenTextOfShare.text = SeenNumber + " K";
                Destroy(cloneCompletedEffect);
                Destroy(cloneCompletedEffect1);
                Destroy(cloneCompletedEffect2);
                FinalCanvas.SetActive(true);
            });
            //UpdateCharacterPosition();
        });
        //yield return new WaitForSeconds(1f);
        // TabToGetCanvas.SetActive(false);
        // targetObject.SetActive(false);
        //// CaptureAndSetScreenshot();
        // FinalCanvas.SetActive(true);


    }

    public void StartPlayGameMode1Smash()
    {   

        SoundController.THIS.PlayInGameBGClip();
        if (PlayerPrefs.GetInt("SAW_ADSOPEN", 0) == 0)
        {
            adsSaw.SetActive(true);
        }
        else
        {
            adsSaw.SetActive(false);
        }

        if (PlayerPrefs.GetInt("GMAN_ADSOPEN", 0) == 0)
        {
            adsGman.SetActive(true);
        }
        else
        {
            adsGman.SetActive(false);
        }

        MainMonterController.THIS.SmashCharacterRandom();
        targetObject.SetActive(false);
        FinalCanvas.SetActive(false);
        HideAllViewForSmash();
        SmashCanvas.gameObject.SetActive(true);
        SmashManager.SetActive(true);
    }

    public void StartPlayGameMode0Normal()
    {
        SoundController.THIS.PlayInGameBGClip();
        numberPlay = PlayerPrefs.GetInt("NumberPlay", 0);
        numberPlay++;
        if (numberPlay == 2)
        {
           // PlayerPrefs.SetInt("NumberPlay", numberPlay + 1);
            RateCanvas.gameObject.SetActive(true);
        }
        else
        {
            PlayerPrefs.SetInt("NumberPlay", numberPlay + 1);
            int abc = PlayerPrefs.GetInt("isRating", 0);
            numberPlay = PlayerPrefs.GetInt("NumberPlay", 1);
            if (abc == 0 && numberPlay % 15 == 0)
            {
                RateCanvas.gameObject.SetActive(true);
            }
        }
        
        IngameCanvas.SetActive(true);
  
        MainMonterController.THIS.RandomCharacter();
        RandomBehindCharacter();
        RandomSkinYesNo();
        //SoundController.THIS.PlayInGameBGClip();

    }

    private void RandomBehindCharacter()
    {
        int behindIndex = Random.Range(0, behindMonster.Count);
        behindObject = Instantiate(behindMonster[behindIndex]);
    }

    public void ClickChangeOption()
    {
        
        //FireBaseAnalysticsController.THIS.FireEvent("ADS_REWARD_CLICK");
       // IronSouceController.THIS.TypeReward = 1;
        //IronSouceController.THIS.ShowRewardAds();
        // RandomSkinYesNo();
        //GetReward();
    }

    public void GetReward()
    {
        YesButton.interactable = false;
        NoButton.interactable = false;
        ChangeOptionButton.interactable = false;
        string a = "";
        if (currentSkin == SkinToChange.Head)
        {
            a = "Head";
            //FireBaseAnalysticsController.THIS.FireEvent("ADS_REWARD_COMPLETED_HEAD");
        }
        if (currentSkin == SkinToChange.Body)
        {
            a = "Body";
            //FireBaseAnalysticsController.THIS.FireEvent("ADS_REWARD_COMPLETED_BODY");
        }
        if (currentSkin == SkinToChange.Acc)
        {
            a = "Acc";
            //FireBaseAnalysticsController.THIS.FireEvent("ADS_REWARD_COMPLETED_ACC");
        }
        if (currentSkin == SkinToChange.Eyes)
        {
            a = "Eyes";
            //FireBaseAnalysticsController.THIS.FireEvent("ADS_REWARD_COMPLETED_EYES");
        }
        if (currentSkin == SkinToChange.Mouth)
        {
            a = "Mouth";
            //FireBaseAnalysticsController.THIS.FireEvent("ADS_REWARD_COMPLETED_MOUTH");
        }
        BehindMonsterController.THIS.RemoveSkin(a);
        RandomSkinYesNo();
        
        StartCoroutine(SetButtonCanSelect());
    }

    private IEnumerator SetButtonCanSelect()
    {
        yield return new WaitForSeconds(2.5f);
        YesButton.interactable = true;
        NoButton.interactable = true;
        ChangeOptionButton.interactable = true;
    }


   


    public void RandomSkinYesNo()
    {
        BehindMonsterController.THIS.ResetScaleImage();
        switch (currentSkin)
        {
            case SkinToChange.Head:
                {
                    BehindMonsterController.THIS.RandomHeadSkin();
                    //BehindMonsterController.THIS.RemoveSkin("Head");
                    break;
                }
            case SkinToChange.Body:
                {
                    BehindMonsterController.THIS.RandomBodySkin();
                    //BehindMonsterController.THIS.RemoveSkin("Body");
                    break;
                }
            case SkinToChange.Acc:
                {
                    BehindMonsterController.THIS.RandomAccSkin();
                    // BehindMonsterController.THIS.RemoveSkin("Acc");
                    break;
                }
            case SkinToChange.Eyes:
                {
                    BehindMonsterController.THIS.RandomEyesSkin();
                    // BehindMonsterController.THIS.RemoveSkin("Eyes");
                    break;
                }
            case SkinToChange.Mouth:
                {
                    BehindMonsterController.THIS.RandomMouthSkin();
                    // BehindMonsterController.THIS.RemoveSkin("Mouth");
                    break;
                }
        }

    }

    public void ChangeCurrentSkin(bool isYes)
    {
        switch (currentSkin)
        {
            case SkinToChange.Head:
                {
                    if (isYes == true)
                    {
                        HeadSelectedEffect.Stop();
                        HeadSelectedImage.gameObject.SetActive(true);
                        HeadBg.sprite = ItemSelected;
                        currentSkin = SkinToChange.Body;
                        BodySelectedEffect.Play();
                        CountYN = 1;
                        break;
                    }
                    else
                    {
                        StartCoroutine(WaitToBeHindMonsterShow());
                        if (CountYN == 2)
                        {
                            HeadSelectedEffect.Stop();
                            HeadSelectedImage.gameObject.SetActive(true);
                            HeadBg.sprite = ItemSelected;
                            currentSkin = SkinToChange.Body;
                            BodySelectedEffect.Play();
                            CountYN = 1;
                            break;
                        }
                        else
                        {
                            CountYN++;
                            break;
                        }

                    }
                }

            case SkinToChange.Body:
                {
                    if (isYes == true)
                    {
                        BodySelectedEffect.Stop();
                        BodySelectedImage.gameObject.SetActive(true);
                        BodyBg.sprite = ItemSelected;
                        currentSkin = SkinToChange.Acc;
                        AccSelectedEffect.Play();
                        CountYN = 1;
                        break;
                    }
                    else
                    {
                        StartCoroutine(WaitToBeHindMonsterShow());
                        if (CountYN == 2)
                        {
                            BodySelectedEffect.Stop();
                            BodySelectedImage.gameObject.SetActive(true);
                            BodyBg.sprite = ItemSelected;
                            currentSkin = SkinToChange.Acc;
                            AccSelectedEffect.Play();
                            CountYN = 1;
                            break;
                        }
                        else
                        {
                            CountYN++;
                            break;
                        }

                    }
                }

            case SkinToChange.Acc:
                {

                    if (isYes == true)
                    {
                        AccSelectedEffect.Stop();
                        AccSelectedImage.gameObject.SetActive(true);
                        AccBg.sprite = ItemSelected;
                        currentSkin = SkinToChange.Eyes;
                        EyesSelectedEffect.Play();
                        CountYN = 1;
                        break;
                    }
                    else
                    {
                        StartCoroutine(WaitToBeHindMonsterShow());
                        if (CountYN == 2)
                        {
                            AccSelectedEffect.Stop();
                            AccSelectedImage.gameObject.SetActive(true);
                            AccBg.sprite = ItemSelected;
                            currentSkin = SkinToChange.Eyes;
                            EyesSelectedEffect.Play();
                            CountYN = 1;
                            break;
                        }
                        else
                        {
                            CountYN++;
                            break;
                        }

                    }
                }
            case SkinToChange.Eyes:
                {

                    if (isYes == true)
                    {
                        EyesSelectedEffect.Stop();
                        EyesSelectedImage.gameObject.SetActive(true);
                        EyesBg.sprite = ItemSelected;
                        currentSkin = SkinToChange.Mouth;
                        MouthSelectedEffect.Play();
                        CountYN = 1;
                        break;
                    }
                    else
                    {
                        StartCoroutine(WaitToBeHindMonsterShow());
                        if (CountYN == 2)
                        {
                            EyesSelectedEffect.Stop();
                            EyesSelectedImage.gameObject.SetActive(true);
                            EyesBg.sprite = ItemSelected;
                            currentSkin = SkinToChange.Mouth;
                            MouthSelectedEffect.Play();
                            CountYN = 1;
                            break;
                        }
                        else
                        {
                            CountYN++;
                            break;
                        }

                    }
                }
            case SkinToChange.Mouth:
                {

                    if (isYes == true)
                    {
                        MouthSelectedEffect.Stop();
                        MouthSelectedImage.gameObject.SetActive(true);
                        MouthBg.sprite = ItemSelected;
                        isFinalChoice = true;
                        currentSkin = SkinToChange.Head;
                        CountYN = 1;
                        //GameManager.THIS.IngameCanvas.SetActive(false);
                        //GameManager.THIS.WinGame();
                        break;
                    }
                    else
                    {
                        if (CountYN == 2)
                        {
                            MouthSelectedEffect.Stop();
                            MouthSelectedImage.gameObject.SetActive(true);
                            MouthBg.sprite = ItemSelected;
                            isFinalChoice = true;
                            currentSkin = SkinToChange.Head;
                            CountYN = 1;
                            //GameManager.THIS.IngameCanvas.SetActive(false);
                            //WinGame();
                            break;
                        }
                        else
                        {
                            StartCoroutine(WaitToBeHindMonsterShow());
                            CountYN++;
                            break;
                        }

                    }
                }
        }

    }

    private IEnumerator WaitToBeHindMonsterShow()
    {
        yield return new WaitForSeconds(0.7f);
        BehindMonsterController.THIS.PlayAnimationCharacter(0, "Angry", true);
        SoundController.THIS.PlayNotSelectedClip();
    }

    public void ClickTapButton()
    {
        SoundController.THIS.PlayTabClip();
        int inCreateNumber = Random.Range(1, 5);
        int inCreateNumberSeen = Random.Range(1, 5);
        LikeNumber += inCreateNumber;
        LikeNumberText.text = LikeNumber + "K";
        SeenNumber += inCreateNumberSeen;
        SeenNumberText.text = SeenNumber + "K";

        if (LikeReactTween == null)
        {
            TabButtonText.transform.DOScale(LikeObject.transform.localScale * 1.2f, 0.1f);
            LikeReactTween = LikeObject.transform.DOScale(LikeObject.transform.localScale * 1.05f, 0.1f).OnComplete(() =>
            {
                TabButtonText.transform.DOScale(Vector3.one, 0.1f);
                LikeObject.transform.DOScale(Vector3.one, 0.1f).OnComplete(() =>
                {
                    LikeReactTween = null;

                }); ; ;
            }); ;
        }


    }




    public void CaptureAndSetScreenshot()
    {
        StartCoroutine(CaptureScreenAndSetImage());
    }

    private IEnumerator CaptureScreenAndSetImage()
    {
        

        // Chụp màn hình
        yield return new WaitForEndOfFrame();

        int halfWidth = Screen.width / 2;
        int halfHeight = Screen.height / 2;

        int startX = halfWidth - 380; // Điểm bắt đầu cho phù hợp với màn hình 1020x1920
        int startY = halfHeight - 150; // Điểm bắt đầu cho phù hợp với màn hình 1020x1920

        Texture2D screenshotTexture = new Texture2D(760, 680, TextureFormat.RGB24, false);
        screenshotTexture.ReadPixels(new Rect(startX, startY, 760, 680), 0, 0);
        screenshotTexture.Apply();

        
        // Đặt ảnh vào UI Image
        Sprite screenshotSprite = Sprite.Create(screenshotTexture, new Rect(0, 0, 760, 680), new Vector2(0.5f, 0.5f));
        ScreenShotImage.sprite = screenshotSprite;
        FinalScreenShotImage.sprite = screenshotSprite;
        
    }

    public void ClickShareButton()
    {
        SoundController.THIS.PlayTabClip();
        FinalCanvas.SetActive(false);
        ShareCanvas.gameObject.SetActive(true);

    }

    public void ClickSmashButton()
    {
        if (PlayerPrefs.GetInt("SAW_ADSOPEN", 0) == 0)
        {
            adsSaw.SetActive(true);
        }
        else
        {
            adsSaw.SetActive(false);
        }
        if (PlayerPrefs.GetInt("GMAN_ADSOPEN", 0) == 0)
        {
            adsGman.SetActive(true);
        }
        else
        {
            adsGman.SetActive(false);
        }

        // SoundController.THIS.PlayTabClip();
        targetObject.SetActive(false);
        FinalCanvas.SetActive(false);
        HideAllViewForSmash();
        SmashCanvas.gameObject.SetActive(true);
        SmashManager.SetActive(true);

        // IronSouceController.THIS.ShowInterstitialAds();
    }


    public void HideAllViewForSmash()
    {
        BGCanvas.gameObject.SetActive(false);
        Canvas.gameObject.SetActive(false);
        ItemCanvas.gameObject.SetActive(false);
        ViewCanvas.gameObject.SetActive(false);
        TutorialCanvas.gameObject.SetActive(false);
    }


    public void ClickSmashStickButton()
    {
        //SoundController.THIS.PlayTabClip();
        

        if (selectedSmashTool != "Stick")
        {
            if (selectedSmashTool == "Saw")
            {
                SawController.SetActive(false);
                khungSaw.sprite = notUsingItem;
            }
            else if (selectedSmashTool == "Gman")
            {
                GmanController.SetActive(false);
                khungGman.sprite = notUsingItem;
            }

            Vector2 positon = new Vector2(1.2f, 0f);
            // Instantiate(StickSmash, positon, Quaternion.identity, SmashBox.transform);
            StickController.transform.position = positon;
            StickController.SetActive(true);
            selectedSmashTool = "Stick";
            khungStick.sprite = isUsingItem;
        }
        else
        {
            selectedSmashTool = null;
            StickController.SetActive(false);
            khungStick.sprite = notUsingItem;
        }
    }

    public void ClickSmashSawButton()
    {
        // SoundController.THIS.PlayTabClip();
       

        if (PlayerPrefs.GetInt("SAW_ADSOPEN", 0) == 1)
        {
            if (selectedSmashTool != "Saw")
            {
                if (selectedSmashTool == "Stick")
                {
                    StickController.SetActive(false);
                    khungStick.sprite = notUsingItem;
                }
                else if (selectedSmashTool == "Gman")
                {
                    GmanController.SetActive(false);
                    khungGman.sprite = notUsingItem;
                }

                Vector2 positon = new Vector2(1.2f, 0f);
                //Instantiate(SawSmash, positon, Quaternion.identity, SmashBox.transform);
                SawObject.transform.position = positon;
                SawController.SetActive(true);
                selectedSmashTool = "Saw";
                khungSaw.sprite = isUsingItem;
            }
            else
            {
                selectedSmashTool = null;
                SawController.SetActive(false);
                khungSaw.sprite = notUsingItem;
            }
        }
        else if (PlayerPrefs.GetInt("SAW_ADSOPEN", 0) == 0)
        {
           // IronSouceController.THIS.TypeReward = 2;
           // IronSouceController.THIS.ShowRewardAds();
            
        }
    }

    public void ClickSmashGmanButton()
    {
        //SoundController.THIS.PlayTabClip();
       
        if (PlayerPrefs.GetInt("GMAN_ADSOPEN", 0) == 1)
        {
            if (selectedSmashTool != "Gman")
            {
                if (selectedSmashTool == "Stick")
                {
                    StickController.SetActive(false);
                    khungStick.sprite = notUsingItem;
                }
                else if (selectedSmashTool == "Saw")
                {
                    SawController.SetActive(false);
                    khungSaw.sprite = notUsingItem;
                }
                Vector2 positon = new Vector2(1.2f, 0f);
                //Instantiate(SawSmash, positon, Quaternion.identity, SmashBox.transform);
                GmanController.transform.position = positon;
                GmanController.SetActive(true);
                selectedSmashTool = "Gman";
                khungGman.sprite = isUsingItem;
            }
            else
            {
                selectedSmashTool = null;
                GmanController.SetActive(false);
                khungGman.sprite = notUsingItem;
            }
        }
        else if (PlayerPrefs.GetInt("GMAN_ADSOPEN", 0) == 0)
        {
            //IronSouceController.THIS.TypeReward = 3;
           // IronSouceController.THIS.ShowRewardAds();
            

        }
    }

    public void HideAllTheIconGetWeapon()
    {
        selectedSmashTool = null;
        khungStick.sprite = notUsingItem;
        khungSaw.sprite = notUsingItem;
        khungGman.sprite = notUsingItem;
    }

    public void ShowFinishedCanvas()
    {
        FinisherCanvas.gameObject.SetActive(true);
    }

    public void ClickHomeSmashButton()
    {
        SoundController.THIS.PlayTabClip();
        LoadingController.THIS.gameObject.SetActive(true);

        LoadingController.THIS.LoadingAction(() =>
        {
            
            SceneManager.LoadScene(0);
            
        });
    }


   
}
